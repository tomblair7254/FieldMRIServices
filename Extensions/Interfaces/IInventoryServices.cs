using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IInventoryServices
    {
        Task<int> AddOrUpdateInventoryAsync(InventoryModel inventoryModel);
        Task<InventoryModel> GetInventoryByIdAsync(int id);
        Task<List<InventoryModel>> GetInventoryAsync();
        Task<int> DeleteInventoryAsync(int id);
        Task<List<InventoryModel>> GetUpdatedInventoryAsync(); // Existing method
        Task<List<InventoryModel>> SearchInventoryAsync(string searchTerm); // New method
        Task<int> DuplicateInventoryAsync(int id); // New method
        Task<string?> UploadFileToCloudinaryAsync(Stream fileStream, InventoryModel inventory, int index); // New method
        Task<bool> DeleteFileFromCloudinaryAsync(string imageUrl); // New method
    }
}