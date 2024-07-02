using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IIEEEService
    {
        Task<int> AddOrUpdateIEEEAsync(IEEEModel ieeeModel);
        Task<IEEEModel> GetIEEEByIdAsync(int id);
        Task<List<IEEEModel>> GetIEEEAsync();
        Task<int> DeleteIEEEAsync(int id);
        Task UpdateIEEEModelAsync(IEEEModel ieeeModel);
        Task<List<IEEEPhotosModel>> GetPhotoByIEEEIdAsync(int ieeeModelId);
        Task<List<IEEEPhotosModel>> GetPhotosAsync();
    }
}
