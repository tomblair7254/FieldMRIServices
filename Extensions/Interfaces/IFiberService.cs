using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IFiberService
    {
        Task<int> AddOrUpdateFiberAsync(FiberModel fiberModel);
        Task<FiberModel> GetFiberByIdAsync(int id);
        Task<List<FiberModel>> GetFiberAsync();
        Task<int> DeleteFiberAsync(int id);

        Task<List<FiberPhotoModel>> GetPhotoByFiberIdAsync(int fiberId);
        Task<List<FiberPhotoModel>> GetPhotosAsync();
    }
}
