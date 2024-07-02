
using AutoMapper;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Model;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Extensions.Services
{
    public class ModiskService : IModiskService
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ModiskService(FMIventoryDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<int> AddOrUpdateModiskAsync(ModiskModel modiskModel)
        {
            if (modiskModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var modisk = _mapper.Map<Modisk>(modiskModel);

            if (modiskModel.Id != 0)
            {
                var findModisk = await _appDbContext.Modisks.FindAsync(modiskModel.Id);
                if (findModisk is null)
                    return (int)System.Net.HttpStatusCode.NotFound;


                findModisk.Id = modiskModel.Id;
                findModisk.ComputerName = modiskModel.ComputerName;
                findModisk.GeneralImage = modiskModel.GeneralImage;
                findModisk.Serial = modiskModel.Serial;
                findModisk.Status = modiskModel.Status;

                await _appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await _appDbContext.ModiskPhotos.Where(_ => _.ComputerName.ToLower().Equals(modiskModel.ComputerName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;


            _appDbContext.Modisks.Add(modisk);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteModiskAsync(int id)
        {
            Modisk modisk = await _appDbContext.Modisks.FirstOrDefaultAsync(c => c.Id == id);
            if (modisk is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            _appDbContext.Modisks.Remove(modisk);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<ModiskModel>> GetModiskAsync()
        {
            var modisks = await _appDbContext.Modisks.ToListAsync();
            if (modisks is null)
                return null!;

            var modiskModelList = modisks.Select(_mapper.Map<ModiskModel>);
            return modiskModelList.ToList();
        }

        public async Task<ModiskModel> GetModiskByIdAsync(int id)
        {
            Modisk modisk = await _appDbContext.Modisks.FirstOrDefaultAsync(c => c.Id == id);
            if (modisk is null) return null!;

            var modiskModel = _mapper.Map<ModiskModel>(modisk);
            return modiskModel;
        }

        public async Task<List<ModiskPhotoModel>> GetPhotoByModiskIdAsync(int modiskId)
        {
            var results = await _appDbContext.ModiskPhotos.Where(_ => _.modiskId == modiskId).Include(_ => _.Modisk).ToListAsync();
            var list = results.Select(_mapper.Map<ModiskPhotoModel>);
            return list.ToList();
        }

        public async Task<List<ModiskPhotoModel>> GetPhotosAsync()
        {
            var modiskhotos = await _appDbContext.ModiskPhotos.ToListAsync();
            if (modiskhotos is null)
                return null!;

            var modiskphotoList = modiskhotos.Select(_mapper.Map<ModiskPhotoModel>);
            return modiskphotoList.ToList();
        }

        public async Task UpdateModiskModel(ModiskModel modiskModel)
        {
            try
            {
                var modiskToUpdate = await _appDbContext.Modisks.FindAsync(modiskModel.Id);

                if (modiskToUpdate != null)
                {
                    modiskToUpdate.Status = modiskModel.Status;
                    modiskToUpdate.ComputerName = modiskModel.ComputerName;
                    modiskToUpdate.Tag = modiskModel.Tag;
                    modiskToUpdate.GeneralImage = modiskModel.GeneralImage;
                    modiskToUpdate.BarCodes = modiskModel.BarCodes;
                    modiskToUpdate.Serial = modiskModel.Serial;
                    modiskToUpdate.Location = modiskModel.Location;
                    modiskToUpdate.Qty = modiskModel.Qty;

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
