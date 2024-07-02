using AutoMapper;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Model;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Extensions.Services
{
    public class MemoryService : IMemoryService
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;

        public MemoryService(FMIventoryDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> AddOrUpdateMemoryAsync(MemoryModel memoryModel)
        {
            if (memoryModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var memory = _mapper.Map<Memory>(memoryModel);


            if (memoryModel.Id != 0)
            {
                var findMemroy = await _appDbContext.Memories.FindAsync(memoryModel.Id);
                if (findMemroy is null)
                    return (int)System.Net.HttpStatusCode.NotFound;

                findMemroy.Id = memoryModel.Id;
                findMemroy.Qunity = memoryModel.Qunity;
                findMemroy.GeneralImage = memoryModel.GeneralImage;
                findMemroy.Location = memoryModel.Location;
                findMemroy.Model = memoryModel.Model;
                findMemroy.Type = memoryModel.Type;
                findMemroy.Size = memoryModel.Size;
                findMemroy.Status = memoryModel.Status;
                findMemroy.Tag = memoryModel.Tag;
                findMemroy.ComputerName = memoryModel.ComputerName;
                await _appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }
            var chk = await _appDbContext.MemoryPhotos.Where(_ => _.ComputerName.ToLower().Equals(memoryModel.ComputerName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;


            _appDbContext.Memories.Add(memory);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteMemoryAsync(int id)
        {
            Memory memory = await _appDbContext.Memories.FirstOrDefaultAsync(c => c.Id == id);
            if (memory is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            _appDbContext.Memories.Remove(memory);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<MemoryModel>> GetMemoryAsync()
        {
            var memories = await _appDbContext.Memories.ToListAsync();
            if (memories is null)
                return null!;

            var memoryModelList = memories.Select(_mapper.Map<MemoryModel>);
            return memoryModelList.ToList();
        }

        public async Task<MemoryModel> GetMemoryByIdAsync(int id)
        {
            Memory memory = await _appDbContext.Memories.FirstOrDefaultAsync(c => c.Id == id);
            if (memory is null) return null!;

            var memoryModel = _mapper.Map<MemoryModel>(memory);
            return memoryModel;
        }

        public async Task<List<MemoryPhotoModel>> GetPhotoByMemoryIdAsync(int memoryId)
        {
            var results = await _appDbContext.MemoryPhotos.Where(_ => _.MemoryId == memoryId).Include(_ => _.Memory).ToListAsync();
            var list = results.Select(_mapper.Map<MemoryPhotoModel>);
            return list.ToList();
        }

        public async Task<List<MemoryPhotoModel>> GetPhotosAsync()
        {
            var photos = await _appDbContext.MemoryPhotos.ToListAsync();
            if (photos is null)
                return null!;

            var memoryModelList = photos.Select(_mapper.Map<MemoryPhotoModel>);
            return memoryModelList.ToList();
        }

        public async Task UpdateMemoryModel(MemoryModel memoryModel)
        {
            try
            {
                var memoryToUpdate = await _appDbContext.Computers.FindAsync(memoryModel.Id);

                if (memoryToUpdate != null)
                {
                    memoryToUpdate.Status = memoryModel.Status;
                    memoryToUpdate.ComputerName = memoryModel.ComputerName;
                    memoryToUpdate.Tag = memoryModel.Tag;
                    memoryToUpdate.GeneralImage = memoryModel.GeneralImage;
                    memoryToUpdate.BarCodes = memoryModel.BarCodes;
                    memoryToUpdate.Qty = memoryModel.Qunity;

                    await _appDbContext.SaveChangesAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
