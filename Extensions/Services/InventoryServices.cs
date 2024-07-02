using AutoMapper;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Model;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Extensions.Services
{
    public class InventoryServices : IIventoryServices
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;

        public InventoryServices(FMIventoryDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> AddOrUpdateInventoryAsync(InventoryModel inventoryModel)
        {
            //PhotoModel photoModel = new PhotoModel();
            if (inventoryModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var inventory = _mapper.Map<Inventory>(inventoryModel);
            //var photo = _mapper.Map<Photo>(photoModel);
            //photo.Image = inventoryModel.GeneralImage;
            //photo.InventoryId = inventoryModel.Id;
            //photo.InventoryName = inventoryModel.InventoryName;

            if (inventoryModel.Id != 0)
            {
                var findInventory = await _appDbContext.Inventories.FindAsync(inventoryModel.Id);
                if (findInventory is null)
                    return (int)System.Net.HttpStatusCode.NotFound;

                //photoModel.InventoryName = findInventory.InventoryName;
                //photoModel.Image = findInventory.GeneralImage;
                //photoModel.InventoryId = findInventory.Id;
                //photoModel.InventoryName = findInventory.InventoryName.ToLower();
                //photoModel.Id = findInventory.Id;

                findInventory.InventoryName = inventoryModel.InventoryName;
                findInventory.GeneralImage = inventoryModel.GeneralImage;
                findInventory.Description = inventoryModel.Description;
                findInventory.Pin = inventoryModel.Pin;
                findInventory.Serial = inventoryModel.Serial;
                findInventory.Location = inventoryModel.Location;
                findInventory.DateReturned = inventoryModel.DateReturned;
                findInventory.ReturnedBy = inventoryModel.ReturnedBy;
                findInventory.ReturnedBy = inventoryModel.ReturnedBy;
                findInventory.Status = inventoryModel.Status;
                findInventory.DateReturned = inventoryModel.DateReturned;
                //_appDbContext.Add(photoModel);
                await _appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await _appDbContext.Photos.Where(_ => _.InventoryName.ToLower().Equals(inventoryModel.InventoryName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;

            //_appDbContext.Photos.Add(photo);
            _appDbContext.Inventories.Add(inventory);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteInventoryAsync(int id)
        {
            Inventory inventory = await _appDbContext.Inventories.FirstOrDefaultAsync(c => c.Id == id);
            if (inventory is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            _appDbContext.Inventories.Remove(inventory);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<InventoryModel>> GetInventoryAsync()
        {
            var inventories = await _appDbContext.Inventories.ToListAsync();
            if (inventories is null)
                return null!;

            var inventoryModelList = inventories.Select(_mapper.Map<InventoryModel>);
            return inventoryModelList.ToList();
        }

        public async Task<InventoryModel> GetInventoryByIdAsync(int id)
        {
            Inventory inventory = await _appDbContext.Inventories.FirstOrDefaultAsync(c => c.Id == id);
            if (inventory is null) return null!;

            var inventoryModel = _mapper.Map<InventoryModel>(inventory);
            return inventoryModel;
        }

        public async Task<List<InventoryPhotoModel>> GetPhotoByInventoryIdAsync(int InventoryId)
        {
            var results = await _appDbContext.Photos.Where(_ => _.InventoryId == InventoryId).Include(_ => _.Inventory).ToListAsync();
            var list = results.Select(_mapper.Map<InventoryPhotoModel>);
            return list.ToList();
        }

        public async Task<List<InventoryPhotoModel>> GetInventoryPhotosAsync()
        {
            var photos = await _appDbContext.Photos.ToListAsync();
            if (photos is null)
                return null!;

            var photoModelList = photos.Select(_mapper.Map<InventoryPhotoModel>);
            return photoModelList.ToList();
        }


    }
}
