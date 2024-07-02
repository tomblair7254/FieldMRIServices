using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IHarddriveService
    {
        Task<int> AddOrUpdateHarddriveAsync(HarddriveModel harddriveModel);
        Task<HarddriveModel> GetHarddriveByIdAsync(int id);
        Task<List<HarddriveModel>> GetHarddriveAsync();
        Task<int> DeleteHarddriveAsync(int id);
        Task UpdateHarddirveModel(HarddriveModel harddriveModel);
        Task<List<HarddrivePhotoModel>> GetPhotoByHarddriveIdAsync(int harddriveId);
        Task<List<HarddrivePhotoModel>> GetPhotosAsync();
    }
}
