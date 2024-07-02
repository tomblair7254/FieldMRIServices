using FieldMRIServices.Entities;
using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IOtherService
    {
        Task<int> AddOrUpdateOtherAsync(OtherModel otherData);
        Task<List<OtherModel>> GetOthers();
        Task<Other> AddOther(OtherModel otherModel);
        Task UdateOtherModel(OtherModel otherModel);
        Task DeleteOther(int id);


    }
}
