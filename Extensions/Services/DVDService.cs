using AutoMapper;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Extensions.Services
{
    public class DVDService : IDVDService
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;

        public DVDService(FMIventoryDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> AddOrUpdateDVDAsync(DVDModel dvdModel)
        {
            if (dvdModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var dvd = _mapper.Map<DVDModel>(dvdModel);

            if (dvdModel.Id != 0)
            {
                var findDvd = await _appDbContext.DVDs.FindAsync(dvdModel.Id);
                if (findDvd is null)
                    return (int)System.Net.HttpStatusCode.NotFound;


                findDvd.Id = dvdModel.Id;
                findDvd.ComputerName = dvdModel.ComputerName;
                findDvd.GeneralImage = dvdModel.GeneralImage;
                findDvd.Tag = dvdModel.Tag;
                findDvd.Type = dvdModel.Type;
                findDvd.Location = dvdModel.Location;
                findDvd.BarCodes = dvdModel.BarCodes;
                findDvd.Status = dvdModel.Status;
                findDvd.Qunity = dvdModel.Qunity;
                findDvd.Serial = dvdModel.Serial;
                await _appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await _appDbContext.DVDPhotos.Where(_ => _.ComputerName.ToLower().Equals(dvdModel.ComputerName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;


            _appDbContext.DVDs.Add(dvd);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteDVDAsync(int id)
        {
            DVDModel dvd = await _appDbContext.DVDs.FirstOrDefaultAsync(c => c.Id == id);
            if (dvd is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            _appDbContext.DVDs.Remove(dvd);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<DVDModel> GetDVDByIdAsync(int id)
        {
            DVDModel dvd = await _appDbContext.DVDs.FirstOrDefaultAsync(c => c.Id == id);
            if (dvd is null) return null!;

            var dvdModel = _mapper.Map<DVDModel>(dvd);
            return dvdModel;
        }

        public async Task<List<DVDModel>> GetDVDAsync()
        {
            var dvd = await _appDbContext.DVDs.ToListAsync();
            if (dvd is null)
                return null!;

            var dvdModelList = dvd.Select(_mapper.Map<DVDModel>);
            return dvdModelList.ToList();
        }

        public async Task<List<DVDPhotoModel>> GetPhotoByDVDIdAsync(int dvdId)
        {
            var results = await _appDbContext.DVDPhotos.Where(_ => _.DVDId == dvdId).Include(_ => _.DVD).ToListAsync();
            var list = results.Select(_mapper.Map<DVDPhotoModel>);
            return list.ToList();
        }

        public async Task<List<DVDPhotoModel>> GetPhotosAsync()
        {
            var dvdphotos = await _appDbContext.DVDPhotos.ToListAsync();
            if (dvdphotos is null)
                return null!;

            var dvdphotoList = dvdphotos.Select(_mapper.Map<DVDPhotoModel>);
            return dvdphotoList.ToList();
        }

        public async Task UpdateDVDAsync(DVDModel dvdModel)
        {
            try
            {
                var dvdToUpdate = await _appDbContext.DVDs.FindAsync(dvdModel.Id);

                if (dvdToUpdate != null)
                {
                    dvdToUpdate.Status = dvdModel.Status;
                    dvdToUpdate.ComputerName = dvdModel.ComputerName;
                    dvdToUpdate.Tag = dvdModel.Tag;
                    dvdToUpdate.GeneralImage = dvdModel.GeneralImage;
                    dvdToUpdate.BarCodes = dvdModel.BarCodes;
                    dvdToUpdate.Qunity = dvdModel.Qunity;

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
