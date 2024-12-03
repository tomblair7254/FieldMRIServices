using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Model;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FieldMRIServices.Extensions.Services
{
    public class InventoryServices : IInventoryServices
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<InventoryServices> _logger;
        private readonly Cloudinary _cloudinary;

        public InventoryServices(FMIventoryDbContext appDbContext, IMapper mapper, ILogger<InventoryServices> logger, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _logger = logger;

            var cloudinarySettings = configuration.GetSection("CloudinarySettings");
            var account = new Account(
                cloudinarySettings["CloudName"],
                cloudinarySettings["ApiKey"],
                cloudinarySettings["ApiSecret"]
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<int> AddOrUpdateInventoryAsync(InventoryModel? inventoryModel)
        {
            if (inventoryModel == null)
            {
                _logger.LogWarning("Inventory model is null");
                return (int)HttpStatusCode.BadRequest;
            }

            _logger.LogInformation("AddOrUpdateInventoryAsync called with model: {@InventoryModel}", inventoryModel);

            // Parse properties before mapping
            inventoryModel.UpdateAllProperties();
            inventoryModel.UpdateTag();

            var inventory = _mapper.Map<Inventory>(inventoryModel);

            if (inventoryModel.Id != 0)
            {
                _logger.LogInformation("Updating inventory with ID {Id}", inventoryModel.Id);
                var existingInventory = await _appDbContext.Inventories.FindAsync(inventoryModel.Id);
                if (existingInventory == null)
                {
                    _logger.LogWarning("Inventory with ID {Id} not found", inventoryModel.Id);
                    return (int)HttpStatusCode.NotFound;
                }

                _appDbContext.Entry(existingInventory).CurrentValues.SetValues(inventory);
                _logger.LogInformation("Inventory with ID {Id} updated successfully", inventoryModel.Id);
            }
            else
            {
                _logger.LogInformation("Adding new inventory with name {Name}", inventoryModel.Name);
                await _appDbContext.Inventories.AddAsync(inventory);
                _logger.LogInformation("New inventory with name {Name} added successfully", inventoryModel.Name);
            }

            await _appDbContext.SaveChangesAsync();
            return (int)HttpStatusCode.OK;
        }

        public async Task<int> DeleteInventoryAsync(int id)
        {
            var inventory = await _appDbContext.Inventories.FindAsync(id);
            if (inventory == null)
            {
                _logger.LogWarning("Inventory with ID {Id} not found", id);
                return (int)HttpStatusCode.NotFound;
            }

            _appDbContext.Inventories.Remove(inventory);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Inventory with ID {Id} deleted successfully", id);
            return (int)HttpStatusCode.OK;
        }

        public async Task<int> DuplicateInventoryAsync(int id)
        {
            var existingInventory = await _appDbContext.Inventories.FindAsync(id);
            if (existingInventory == null)
            {
                _logger.LogWarning("Inventory with ID {Id} not found", id);
                return (int)HttpStatusCode.NotFound;
            }

            // Map the existing inventory to a new inventory entity
            var duplicatedInventory = _mapper.Map<InventoryModel>(existingInventory);
            duplicatedInventory.Id = 0; // Reset the ID to create a new entry

            // Add the new inventory to the database
            var newInventoryEntity = _mapper.Map<Inventory>(duplicatedInventory);
            await _appDbContext.Inventories.AddAsync(newInventoryEntity);
            await _appDbContext.SaveChangesAsync();

            // Use the new inventory ID for image duplication
            var newInventoryId = newInventoryEntity.Id;

            // Handle image duplication in Cloudinary
            if (!string.IsNullOrEmpty(existingInventory.Images))
            {
                var imagePaths = existingInventory.Images.Split(',').Where(url => !string.IsNullOrWhiteSpace(url)).ToList();
                var newImagePaths = new List<string>();

                for (int i = 0; i < 3; i++)
                {
                    if (i < imagePaths.Count)
                    {
                        var newImagePath = await DuplicateImageInCloudinaryAsync(imagePaths[i], newInventoryId, duplicatedInventory.Name, i + 1);
                        if (newImagePath != null)
                        {
                            newImagePaths.Add(newImagePath);
                        }
                    }
                }

                // Ensure the new image paths list has exactly three elements
                while (newImagePaths.Count < 3)
                {
                    newImagePaths.Add("");
                }

                // Update the new inventory's images string
                duplicatedInventory.Images = string.Join(",", newImagePaths);

                // Update the new inventory entity with the new images string
                newInventoryEntity.Images = duplicatedInventory.Images;
                _appDbContext.Entry(newInventoryEntity).Property(i => i.Images).IsModified = true;
                await _appDbContext.SaveChangesAsync();
            }

            _logger.LogInformation("Inventory with ID {Id} duplicated successfully", id);
            return (int)HttpStatusCode.OK;
        }

        private async Task<string?> DuplicateImageInCloudinaryAsync(string originalImageUrl, int newInventoryId, string newName, int index)
        {
            try
            {
                var uri = new Uri(originalImageUrl);
                var publicId = Path.GetFileNameWithoutExtension(uri.LocalPath);

                var newPublicId = $"MedicalInventory/{newInventoryId}_{newName}_{index}_{DateTime.Now:yyyyMMdd_HHmmss}";

                var copyParams = new ImageUploadParams
                {
                    PublicId = newPublicId,
                    File = new FileDescription(originalImageUrl)
                };

                var copyResult = await _cloudinary.UploadAsync(copyParams);

                if (copyResult.StatusCode == HttpStatusCode.OK)
                {
                    return copyResult.SecureUrl.ToString();
                }
                else
                {
                    _logger.LogError("Failed to duplicate image in Cloudinary: {StatusCode}", copyResult.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error duplicating image in Cloudinary");
                return null;
            }
        }

        public async Task<List<InventoryModel>> GetInventoryAsync()
        {
            var inventories = await _appDbContext.Inventories.ToListAsync();
            var inventoryModels = inventories.Select(_mapper.Map<InventoryModel>).ToList();

            // Parse properties for each inventory model
            foreach (var inventoryModel in inventoryModels)
            {
                inventoryModel.ParseProperties();
            }

            return inventoryModels;
        }

        public async Task<InventoryModel?> GetInventoryByIdAsync(int id)
        {
            var inventory = await _appDbContext.Inventories.FindAsync(id);
            return inventory == null ? null : _mapper.Map<InventoryModel>(inventory);
        }

        public async Task<List<InventoryModel>> GetUpdatedInventoryAsync()
        {
            var inventories = await _appDbContext.Inventories.ToListAsync();
            return inventories.Select(_mapper.Map<InventoryModel>).Distinct().ToList(); // Ensure no duplicates
        }

        public async Task<List<InventoryModel>> SearchInventoryAsync(string searchTerm)
        {
            List<Inventory> inventories;

            if (string.IsNullOrEmpty(searchTerm))
            {
                inventories = await _appDbContext.Inventories.ToListAsync();
            }
            else
            {
                var lowerSearchTerm = searchTerm.ToLower();
                bool isInt = int.TryParse(searchTerm, out int id);

                inventories = await _appDbContext.Inventories
                    .Where(i => (isInt && i.Id == id) ||
                                i.Name.ToLower().Contains(lowerSearchTerm) ||
                                i.InventoryNumber.ToLower().Contains(lowerSearchTerm) ||
                                i.Qty.ToLower().Contains(lowerSearchTerm) ||
                                i.Pin.ToLower().Contains(lowerSearchTerm) ||
                                i.Tag.ToLower().Contains(lowerSearchTerm) ||
                                i.Location.ToLower().Contains(lowerSearchTerm) ||
                                i.BarCode.ToLower().Contains(lowerSearchTerm) ||
                                i.Status.ToLower().Contains(lowerSearchTerm) ||
                                i.MRITS.ToLower().Contains(lowerSearchTerm))
                    .ToListAsync();

                // Filter the results to only include those where the first part of the Name matches the search term
                inventories = inventories
                    .Where(i => i.Name.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.InventoryNumber.ToLower().Contains(lowerSearchTerm) ||
                                i.Qty.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Pin.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Tag.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Location.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.BarCode.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Status.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.MRITS.Split(',')[0].ToLower().Contains(lowerSearchTerm))
                    .ToList();
            }

            return inventories.Select(_mapper.Map<InventoryModel>).ToList();
        }

        public async Task<string?> UploadFileToCloudinaryAsync(Stream fileStream, InventoryModel inventory, int index)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(inventory.Name, fileStream),
                    PublicId = $"MedicalInventory/{inventory.Id}_{inventory.Name}_{index}_{DateTime.Now:yyyyMMdd_HHmmss}"
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == HttpStatusCode.OK)
                {
                    return uploadResult.SecureUrl.ToString();
                }
                else
                {
                    _logger.LogError("Failed to upload file to Cloudinary: {StatusCode}", uploadResult.StatusCode);
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error uploading file to Cloudinary");
                return null;
            }
        }

        public async Task<bool> DeleteFileFromCloudinaryAsync(string imageUrl)
        {
            try
            {
                _logger.LogInformation("Deleting image from Cloudinary: {ImageUrl}", imageUrl);
                // Extract the public ID from the URL
                var uri = new Uri(imageUrl);
                var publicId = Path.GetFileNameWithoutExtension(uri.LocalPath);

                // Delete the image from Cloudinary
                var deletionParams = new DeletionParams(publicId);
                var deletionResult = await _cloudinary.DestroyAsync(deletionParams);

                if (deletionResult.Result == "ok")
                {
                    _logger.LogInformation("Image deleted successfully from Cloudinary: {ImageUrl}", imageUrl);
                    return true;
                }
                else
                {
                    _logger.LogError("Failed to delete image from Cloudinary: {Error}", deletionResult.Error.Message);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting file from Cloudinary");
                return false;
            }
        }
    }
}