#nullable enable
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
    public class ComputerService : IComputerService
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ComputerService> _logger;
        private readonly Cloudinary _cloudinary; // Add this line

        public ComputerService(FMIventoryDbContext appDbContext, IMapper mapper, ILogger<ComputerService> logger, IConfiguration configuration) // Update constructor
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

        public async Task<int> AddOrUpdateComputerAsync(ComputerModel? computerModel)
        {
            if (computerModel == null)
            {
                _logger.LogWarning("Computer model is null");
                return (int)HttpStatusCode.BadRequest;
            }

            _logger.LogInformation("AddOrUpdateComputerAsync called with model: {@ComputerModel}", computerModel);

            // Parse properties before mapping
            computerModel.UpdateAllProperties();
            computerModel.UpdateTag();

            var computer = _mapper.Map<Computer>(computerModel);

            if (computerModel.Id != 0)
            {
                _logger.LogInformation("Updating computer with ID {Id}", computerModel.Id);
                var existingComputer = await _appDbContext.Computers.FindAsync(computerModel.Id);
                if (existingComputer == null)
                {
                    _logger.LogWarning("Computer with ID {Id} not found", computerModel.Id);
                    return (int)HttpStatusCode.NotFound;
                }

                _appDbContext.Entry(existingComputer).CurrentValues.SetValues(computer);
                _logger.LogInformation("Computer with ID {Id} updated successfully", computerModel.Id);
            }
            else
            {
                _logger.LogInformation("Adding new computer with name {Name}", computerModel.ComputerName);
                await _appDbContext.Computers.AddAsync(computer);
                _logger.LogInformation("New computer with name {Name} added successfully", computerModel.ComputerName);
            }

            await _appDbContext.SaveChangesAsync();
            return (int)HttpStatusCode.OK;
        }

        public async Task<int> DeleteComputerAsync(int id)
        {
            var computer = await _appDbContext.Computers.FindAsync(id);
            if (computer == null)
            {
                _logger.LogWarning("Computer with ID {Id} not found", id);
                return (int)HttpStatusCode.NotFound;
            }

            _appDbContext.Computers.Remove(computer);
            await _appDbContext.SaveChangesAsync();
            _logger.LogInformation("Computer with ID {Id} deleted successfully", id);
            return (int)HttpStatusCode.OK;
        }

        public async Task<int> DuplicateComputerAsync(int id)
        {
            var existingComputer = await _appDbContext.Computers.FindAsync(id);
            if (existingComputer == null)
            {
                _logger.LogWarning("Computer with ID {Id} not found", id);
                return (int)HttpStatusCode.NotFound;
            }

            // Map the existing computer to a new computer entity
            var duplicatedComputer = _mapper.Map<ComputerModel>(existingComputer);
            duplicatedComputer.Id = 0; // Reset the ID to create a new entry

            // Add the new computer to the database
            var newComputerEntity = _mapper.Map<Computer>(duplicatedComputer);
            await _appDbContext.Computers.AddAsync(newComputerEntity);
            await _appDbContext.SaveChangesAsync();

            // Use the new computer ID for image duplication
            var newComputerId = newComputerEntity.Id;

            // Handle image duplication in Cloudinary
            if (!string.IsNullOrEmpty(existingComputer.Images))
            {
                var imagePaths = existingComputer.Images.Split(',').Where(url => !string.IsNullOrWhiteSpace(url)).ToList();
                var newImagePaths = new List<string>();

                for (int i = 0; i < 3; i++)
                {
                    if (i < imagePaths.Count)
                    {
                        var newImagePath = await DuplicateImageInCloudinaryAsync(imagePaths[i], newComputerId, duplicatedComputer.ComputerName, i + 1);
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

                // Update the new computer's images string
                duplicatedComputer.Images = string.Join(",", newImagePaths);

                // Update the new computer entity with the new images string
                newComputerEntity.Images = duplicatedComputer.Images;
                _appDbContext.Entry(newComputerEntity).Property(i => i.Images).IsModified = true;
                await _appDbContext.SaveChangesAsync();
            }

            _logger.LogInformation("Computer with ID {Id} duplicated successfully", id);
            return (int)HttpStatusCode.OK;
        }

        private async Task<string?> DuplicateImageInCloudinaryAsync(string originalImageUrl, int newComputerId, string newName, int index)
        {
            try
            {
                var uri = new Uri(originalImageUrl);
                var publicId = Path.GetFileNameWithoutExtension(uri.LocalPath);

                var newPublicId = $"ComputerInventory/{newComputerId}_{newName}_{index}_{DateTime.Now:yyyyMMdd_HHmmss}";

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

        public async Task<string?> UploadFileToCloudinaryAsync(Stream fileStream, ComputerModel computer, int index)
        {
            try
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(computer.ComputerName, fileStream),
                    PublicId = $"ComputerInventory/{computer.Id}_{computer.ComputerName}_{index}_{DateTime.Now:yyyyMMdd_HHmmss}"
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
            if (string.IsNullOrEmpty(imageUrl))
            {
                Console.WriteLine("Image URL is null or empty.");
                return false;
            }

            try
            {
                var publicId = GetPublicIdFromUrl(imageUrl);
                if (string.IsNullOrEmpty(publicId))
                {
                    Console.WriteLine("Public ID is null or empty.");
                    return false;
                }

                var deletionParams = new DeletionParams(publicId);
                var result = await _cloudinary.DestroyAsync(deletionParams);

                if (result.Result == "ok")
                {
                    Console.WriteLine($"Image {imageUrl} deleted successfully from Cloudinary.");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Failed to delete image from Cloudinary: {result.Error?.Message}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file from Cloudinary: {ex.Message}");
                return false;
            }
        }

        private string GetPublicIdFromUrl(string imageUrl)
        {
            // Extract the public ID from the URL
            var uri = new Uri(imageUrl);
            var segments = uri.Segments;
            if (segments.Length > 0)
            {
                var publicId = segments.Last().Split('.').First();
                return publicId;
            }
            return null;
        }

        public async Task<List<ComputerModel>> GetComputerAsync()
        {
            var computers = await _appDbContext.Computers.ToListAsync();
            var computerModels = computers.Select(_mapper.Map<ComputerModel>).ToList();

            // Parse properties for each computer model
            foreach (var computerModel in computerModels)
            {
                computerModel.ParseProperties();
            }

            return computerModels;
        }

        public async Task<ComputerModel?> GetComputerByIdAsync(int id)
        {
            var computer = await _appDbContext.Computers.FindAsync(id);
            return computer == null ? null : _mapper.Map<ComputerModel>(computer);
        }

        public async Task<List<ComputerModel>> GetUpdatedComputerAsync()
        {
            var computers = await _appDbContext.Computers.ToListAsync();
            return computers.Select(_mapper.Map<ComputerModel>).Distinct().ToList(); // Ensure no duplicates
        }

        public async Task<List<ComputerModel>> SearchComputerAsync(string searchTerm)
        {
            List<Computer> computers;

            if (string.IsNullOrEmpty(searchTerm))
            {
                computers = await _appDbContext.Computers.ToListAsync();
            }
            else
            {
                var lowerSearchTerm = searchTerm.ToLower();
                bool isInt = int.TryParse(searchTerm, out int id);

                computers = await _appDbContext.Computers
                    .Where(i => (isInt && i.Id == id) ||
                                i.ComputerName.ToLower().Contains(lowerSearchTerm) ||

                                i.Location.ToLower().Contains(lowerSearchTerm) ||
                                i.Serial.ToLower().Contains(lowerSearchTerm) ||
                                i.HardDrive.ToLower().Contains(lowerSearchTerm) ||
                                i.Memory.ToLower().Contains(lowerSearchTerm) ||
                                i.Player.ToLower().Contains(lowerSearchTerm) ||
                                i.Network.ToLower().Contains(lowerSearchTerm) ||
                                i.SCSI.ToLower().Contains(lowerSearchTerm) ||
                                i.MultipleCom.ToLower().Contains(lowerSearchTerm) ||
                                i.Video.ToLower().Contains(lowerSearchTerm) ||
                                i.Images.ToLower().Contains(lowerSearchTerm) ||
                                i.Status.ToLower().Contains(lowerSearchTerm) ||
                                i.Tag.ToLower().Contains(lowerSearchTerm) ||
                                i.Qty.ToLower().Contains(lowerSearchTerm) ||
                                i.BarCodes.ToLower().Contains(lowerSearchTerm) ||
                                i.Pin.ToLower().Contains(lowerSearchTerm) ||
                                i.Fiber.ToLower().Contains(lowerSearchTerm) ||
                                i.IEEE.ToLower().Contains(lowerSearchTerm) ||
                                i.Modisk.ToLower().Contains(lowerSearchTerm) ||
                                i.SASRaid.ToLower().Contains(lowerSearchTerm))
                    .ToListAsync();

                // Filter the results to only include those where the first part of the properties matches the search term
                computers = computers
                    .Where(i => i.ComputerName.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Location.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Serial.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.HardDrive.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Memory.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Player.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Network.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.SCSI.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.MultipleCom.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Video.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Images.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Status.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Tag.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Qty.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.BarCodes.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Pin.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Fiber.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.IEEE.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.Modisk.Split(',')[0].ToLower().Contains(lowerSearchTerm) ||
                                i.SASRaid.Split(',')[0].ToLower().Contains(lowerSearchTerm))
                    .ToList();
            }

            return computers.Select(_mapper.Map<ComputerModel>).ToList();
        }
    }
}