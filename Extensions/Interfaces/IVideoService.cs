using FieldMRIServices.Model;

namespace FieldMRIServices.Extensions.Interfaces
{
    public interface IVideoService
    {
        Task<int> AddOrUpdateVideoAsync(VideoModel videoModel);
        Task<VideoModel> GetVideoIdAsync(int id);
        Task<List<VideoModel>> GetVideosAsync();
        Task<int> DeleteVideosAsync(int id);
        Task UpdateVideoModel(VideoModel videoModel);


        Task<List<VideoPhotoModel>> GetPhotoByVideoIdAsync(int videoId);
        Task<List<VideoPhotoModel>> GetVideoPhotoAsync();
    }
}
