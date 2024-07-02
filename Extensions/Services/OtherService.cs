using AutoMapper;
using BlazorApp1.Extensions;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Extensions.Services;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Extensions.Services
{
    public class OtherService : IOtherService
    {
        private readonly FMIventoryDbContext _appcontext;
        private readonly IMapper _mapper;

        public OtherService(FMIventoryDbContext appcontext, IMapper mapper)
        {
            _appcontext = appcontext;
            _mapper = mapper;
        }

        public async Task<int> AddOrUpdateOtherAsync(OtherModel otherData)
        {
            if (otherData is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var other = _mapper.Map<Other>(otherData);

            if (otherData.Id != 0)
            {
                var findOther = await _appcontext.Others.FindAsync(otherData.Id);
                if (findOther is null)
                    return (int)System.Net.HttpStatusCode.NotFound;


                findOther.Id = otherData.Id;
                findOther.ComputerName = otherData.ComputerName;
                findOther.GeneralImage = otherData.GeneralImage;
                findOther.Tag = otherData.Tag;
                findOther.Type = otherData.Type;
                findOther.Location = otherData.Location;
                findOther.BarCodes = otherData.BarCodes;
                findOther.Status = otherData.Status;
                findOther.Qty = otherData.Qty;
                findOther.Serial = otherData.Serial;
                await _appcontext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await _appcontext.OtherPhotos.Where(_ => _.ComputerName.ToLower().Equals(otherData.ComputerName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;


            _appcontext.Others.Add(other);
            await _appcontext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<Other> AddOther(OtherModel otherModel)
        {
            try
            {
                Other otherToAdd = otherModel.Convert();
                var chk = await _appcontext.OtherPhotos.Where(_ => _.ComputerName.ToLower().Equals(otherModel.ComputerName.ToLower())).FirstOrDefaultAsync();
                var result = await _appcontext.Others
                                .AddAsync(otherToAdd);

                await _appcontext.SaveChangesAsync();

                return result.Entity;

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task DeleteOther(int id)
        {
            try
            {
                var other = await _appcontext.Others.FindAsync(id);
                if (other != null)
                {
                    _appcontext.Others.Remove(other);
                    await _appcontext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<OtherModel>> GetOthers()
        {

            try
            {
                return await _appcontext.Others.Convert();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UdateOtherModel(OtherModel otherModel)
        {
            try
            {
                var othersToUpdate = await _appcontext.Others.FindAsync(otherModel.Id);

                if (othersToUpdate != null)
                {
                    othersToUpdate.Qty = otherModel.Qty;
                    othersToUpdate.Model = otherModel.Model;
                    othersToUpdate.Type = otherModel.Type;
                    othersToUpdate.Status = otherModel.Status;
                    othersToUpdate.ComputerName = otherModel.ComputerName;
                    othersToUpdate.Tag = otherModel.Tag;
                    othersToUpdate.GeneralImage = otherModel.GeneralImage;
                    othersToUpdate.BarCodes = otherModel.BarCodes;
                    othersToUpdate.Serial = otherModel.Serial;

                    await _appcontext.SaveChangesAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
