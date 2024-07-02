using FieldMRIServices.Entities;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IDVDService
    {
        Task<int> AddOrUpdateDVDAsync(DVDModel dvdModel);
        Task<DVDModel> GetDVDByIdAsync(int id);
        Task<List<DVDModel>> GetDVDAsync();
        Task<int> DeleteDVDAsync(int id);
        Task UpdateDVDAsync(DVDModel dvdModel);

        Task<List<DVDPhotoModel>> GetPhotoByDVDIdAsync(int dvdId);
        Task<List<DVDPhotoModel>> GetPhotosAsync();
    }
}
