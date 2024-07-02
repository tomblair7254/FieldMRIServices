using AutoMapper;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Model;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Extensions.Services
{
    public class HarddriveService : IHarddriveService
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;

        public HarddriveService(FMIventoryDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<int> AddOrUpdateHarddriveAsync(HarddriveModel harddriveModel)
        {
            if (harddriveModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var harddrive = _mapper.Map<Harddrive>(harddriveModel);

            if (harddriveModel.Id != 0)
            {
                var findHarddrive = await _appDbContext.Harddrives.FindAsync(harddriveModel.Id);
                if (findHarddrive is null)
                    return (int)System.Net.HttpStatusCode.NotFound;


                findHarddrive.Id = harddriveModel.Id;
                findHarddrive.ComputerName = harddriveModel.ComputerName;
                findHarddrive.GeneralImage = harddriveModel.GeneralImage;
                findHarddrive.Serial = harddriveModel.Serial;
                findHarddrive.Status = harddriveModel.Status;
                findHarddrive.Tag = harddriveModel.Tag;
                findHarddrive.Qty = harddriveModel.Qty;
                findHarddrive.Harddrives = harddriveModel.Harddrives;

                await _appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await _appDbContext.HarddrivePhotos.Where(_ => _.ComputerName.ToLower().Equals(harddriveModel.ComputerName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;


            _appDbContext.Harddrives.Add(harddrive);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteHarddriveAsync(int id)
        {
            Harddrive harddirve = await _appDbContext.Harddrives.FirstOrDefaultAsync(c => c.Id == id);
            if (harddirve is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            _appDbContext.Harddrives.Remove(harddirve);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<HarddriveModel>> GetHarddriveAsync()
        {
            var harddirve = await _appDbContext.Harddrives.ToListAsync();
            if (harddirve is null)
                return null!;

            var harddriveModelList = harddirve.Select(_mapper.Map<HarddriveModel>);
            return harddriveModelList.ToList();
        }

        public async Task<HarddriveModel> GetHarddriveByIdAsync(int id)
        {
            Harddrive harddirve = await _appDbContext.Harddrives.FirstOrDefaultAsync(c => c.Id == id);
            if (harddirve is null) return null!;

            var harddriveModel = _mapper.Map<HarddriveModel>(harddirve);
            return harddriveModel;
        }

        public async Task<List<HarddrivePhotoModel>> GetPhotoByHarddriveIdAsync(int harddriveId)
        {
            var results = await _appDbContext.HarddrivePhotos.Where(_ => _.harddriveId == harddriveId).Include(_ => _.Harddrive).ToListAsync();
            var list = results.Select(_mapper.Map<HarddrivePhotoModel>);
            return list.ToList();
        }

        public async Task<List<HarddrivePhotoModel>> GetPhotoByHarddriveIdAsync()
        {
            var harddrivehotos = await _appDbContext.HarddrivePhotos.ToListAsync();
            if (harddrivehotos is null)
                return null!;

            var harddrivephotoList = harddrivehotos.Select(_mapper.Map<HarddrivePhotoModel>);
            return harddrivephotoList.ToList();
        }



        public async Task<List<HarddrivePhotoModel>> GetPhotosAsync()
        {
            var harddrivephotos = await _appDbContext.HarddrivePhotos.ToListAsync();
            if (harddrivephotos is null)
                return null!;

            var harddrivephotoList = harddrivephotos.Select(_mapper.Map<HarddrivePhotoModel>);
            return harddrivephotoList.ToList();
        }

        public async Task UpdateHarddirveModel(HarddriveModel harddriveModel)
        {
            try
            {
                var hardToUpdate = await _appDbContext.Harddrives.FindAsync(harddriveModel.Id);

                if (hardToUpdate != null)
                {
                    hardToUpdate.Status = harddriveModel.Status;
                    hardToUpdate.ComputerName = harddriveModel.ComputerName;
                    hardToUpdate.Tag = harddriveModel.Tag;
                    hardToUpdate.GeneralImage = harddriveModel.GeneralImage;
                    hardToUpdate.BarCodes = harddriveModel.BarCodes;
                    hardToUpdate.Serial = harddriveModel.Serial;
                    hardToUpdate.Location = harddriveModel.Location;
                    hardToUpdate.Qty = harddriveModel.Qty;

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

