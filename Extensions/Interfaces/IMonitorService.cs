using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IMonitorService
    {
        Task<int> AddOrUpdateMonitorAsync(MonitorModels monitorModel);
        Task<MonitorModels> GetMonitorByIdAsync(int id);
        Task<List<MonitorModels>> GetMonitorAsync();
        Task<int> DeleteMonitorAsync(int id);
        Task UpdateMonitorModels(MonitorModels monitorModel);
        Task<List<MonitorPhotoModel>> GetPhotoByMonitorIdAsync(int monitorId);
        Task<List<MonitorPhotoModel>> GetPhotosAsync();
    }
}
