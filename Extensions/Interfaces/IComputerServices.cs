using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IComputerServices
    {
        Task<int> AddOrUpdateComputerAsync(ComputerModel computerModel);
        Task<ComputerModel> GetComputerByIdAsync(int id);
        Task<List<ComputerModel>> GetComputerAsync();
        Task<int> DeleteComputerAsync(int id);
        Task UpdateComputerModel(ComputerModel computerModel);

        Task<List<ComputerPhotoModel>> GetPhotoByComputerIdAsync(int computerId);
        Task<List<ComputerPhotoModel>> GetPhotosAsync();
    }
}
