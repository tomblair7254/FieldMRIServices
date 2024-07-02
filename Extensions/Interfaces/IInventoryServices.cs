using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IIventoryServices
    {
        Task<int> AddOrUpdateInventoryAsync(InventoryModel ieeeModel);
        Task<InventoryModel> GetInventoryByIdAsync(int id);
        Task<List<InventoryModel>> GetInventoryAsync();
        Task<int> DeleteInventoryAsync(int id);

        Task<List<InventoryPhotoModel>> GetPhotoByInventoryIdAsync(int InventoryId);
        Task<List<InventoryPhotoModel>> GetInventoryPhotosAsync();
    }
}
