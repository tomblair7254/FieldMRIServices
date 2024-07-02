using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IMemoryService
    {
        Task<int> AddOrUpdateMemoryAsync(MemoryModel memoryModel);
        Task<MemoryModel> GetMemoryByIdAsync(int id);
        Task<List<MemoryModel>> GetMemoryAsync();
        Task<int> DeleteMemoryAsync(int id);
        Task UpdateMemoryModel(MemoryModel memoryModel);

        Task<List<MemoryPhotoModel>> GetPhotoByMemoryIdAsync(int memoryId);
        Task<List<MemoryPhotoModel>> GetPhotosAsync();
    }
}
