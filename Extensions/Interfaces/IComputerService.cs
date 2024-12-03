using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IComputerService
    {
        Task<int> AddOrUpdateComputerAsync(ComputerModel computerModel);
        Task<ComputerModel> GetComputerByIdAsync(int id);
        Task<List<ComputerModel>> GetComputerAsync();
        Task<int> DeleteComputerAsync(int id);
        Task<List<ComputerModel>> SearchComputerAsync(string searchTerm);
        Task<int> DuplicateComputerAsync(int id); // New method
        Task<string?> UploadFileToCloudinaryAsync(Stream fileStream, ComputerModel computer, int index); // New method
        Task<bool> DeleteFileFromCloudinaryAsync(string imageUrl); // New method
    }
}