using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IModiskService
    {
        Task<int> AddOrUpdateModiskAsync(ModiskModel Model);
        Task<ModiskModel> GetModiskByIdAsync(int id);
        Task<List<ModiskModel>> GetModiskAsync();
        Task<int> DeleteModiskAsync(int id);
        Task UpdateModiskModel(ModiskModel modiskModel);

        Task<List<ModiskPhotoModel>> GetPhotoByModiskIdAsync(int modiskId);
        Task<List<ModiskPhotoModel>> GetPhotosAsync();
    }
}
