using AutoMapper;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Model;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Extensions.Services
{
    public class IEEEServive : IIEEEService
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;

        public IEEEServive(FMIventoryDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> AddOrUpdateIEEEAsync(IEEEModel ieeeModel)
        {

            if (ieeeModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var ieee = _mapper.Map<IEEE>(ieeeModel);

            if (ieeeModel.Id != 0)
            {
                var findComputer = await _appDbContext.IEEEs.FindAsync(ieeeModel.Id);
                if (findComputer is null)
                    return (int)System.Net.HttpStatusCode.NotFound;


                findComputer.Id = ieee.Id;
                findComputer.ComputerName = ieee.ComputerName;
                findComputer.GeneralImage = ieee.GeneralImage;
                findComputer.Tag = ieee.Tag;
                findComputer.Qunity = ieee.Qunity;
                findComputer.Tag = ieee.Tag;
                findComputer.Location = ieee.Location;
                findComputer.Status = ieee.Status;
                await _appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await _appDbContext.IEEEPhotos.Where(_ => _.ComputerName.ToLower().Equals(ieeeModel.ComputerName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;


            _appDbContext.IEEEs.Add(ieee);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteIEEEAsync(int id)
        {
            IEEE ieee = await _appDbContext.IEEEs.FirstOrDefaultAsync(c => c.Id == id);
            if (ieee is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            _appDbContext.IEEEs.Remove(ieee);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<IEEEModel>> GetIEEEAsync()
        {
            var ieee = await _appDbContext.IEEEs.ToListAsync();
            if (ieee is null)
                return null!;

            var ieeeModelList = ieee.Select(_mapper.Map<IEEEModel>);
            return ieeeModelList.ToList();
        }

        public async Task<IEEEModel> GetIEEEByIdAsync(int id)
        {
            IEEE ieee = await _appDbContext.IEEEs.FirstOrDefaultAsync(c => c.Id == id);
            if (ieee is null) return null!;

            var ieeeModel = _mapper.Map<IEEEModel>(ieee);
            return ieeeModel;
        }

        public async Task<List<IEEEPhotosModel>> GetPhotoByIEEEIdAsync(int ieeeModelId)
        {
            var results = await _appDbContext.IEEEPhotos.Where(_ => _.IEEEId == ieeeModelId).Include(_ => _.IEEE).ToListAsync();
            var list = results.Select(_mapper.Map<IEEEPhotosModel>);
            return list.ToList();
        }

        async Task<List<IEEEPhotosModel>> IIEEEService.GetPhotosAsync()
        {
            var ieeephotos = await _appDbContext.IEEEPhotos.ToListAsync();
            if (ieeephotos is null)
                return null!;

            var ieeephotoList = ieeephotos.Select(_mapper.Map<IEEEPhotosModel>);
            return ieeephotoList.ToList();
        }

        public async Task UpdateIEEEModelAsync(IEEEModel ieeeModel)
        {

            try
            {
                var ieeeToUpdate = await _appDbContext.IEEEs.FindAsync(ieeeModel.Id);

                if (ieeeToUpdate != null)
                {
                    ieeeToUpdate.Status = ieeeModel.Status;
                    ieeeToUpdate.ComputerName = ieeeModel.ComputerName;
                    ieeeToUpdate.Tag = ieeeModel.Tag;
                    ieeeToUpdate.GeneralImage = ieeeModel.GeneralImage;
                    ieeeToUpdate.BarCodes = ieeeModel.BarCodes;
                    ieeeToUpdate.Qunity = ieeeModel.Qunity;

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
