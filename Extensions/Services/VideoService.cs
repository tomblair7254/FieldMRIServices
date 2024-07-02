using AutoMapper;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Model;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Extensions.Services
{
    public class VideoService : IVideoService
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;

        public VideoService(FMIventoryDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> AddOrUpdateVideoAsync(VideoModel videoModel)
        {
            if (videoModel is null) return (int)System.Net.HttpStatusCode.BadRequest;

            var video = _mapper.Map<Video>(videoModel);

            if (videoModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;


            if (videoModel.Id != 0)
            {
                var findVideo = await _appDbContext.Videos.FindAsync(videoModel.Id);
                if (findVideo is null)
                    return (int)System.Net.HttpStatusCode.NotFound;


                findVideo.Id = videoModel.Id;
                findVideo.ComputerName = videoModel.ComputerName;
                findVideo.GeneralImage = videoModel.GeneralImage;
                videoModel.Model = videoModel.Model;
                videoModel.Status = videoModel.Status;

                await _appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await _appDbContext.VideoPhotos.Where(_ => _.ComputerName.ToLower().Equals(videoModel.ComputerName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;


            _appDbContext.Videos.Add(video);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<List<VideoPhotoModel>> GetPhotoByVideoIdAsync(int videoId)
        {
            var results = await _appDbContext.VideoPhotos.Where(_ => _.VideoId == videoId).Include(_ => _.Video).ToListAsync();
            var list = results.Select(_mapper.Map<VideoPhotoModel>);
            return list.ToList();
        }

        public async Task<int> DeleteVideosAsync(int id)
        {
            Video video = await _appDbContext.Videos.FirstOrDefaultAsync(c => c.Id == id);
            if (video is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            _appDbContext.Videos.Remove(video);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<VideoModel> GetVideoIdAsync(int id)
        {
            Video video = await _appDbContext.Videos.FirstOrDefaultAsync(c => c.Id == id);
            if (video is null) return null!;

            var videoModel = _mapper.Map<VideoModel>(video);
            return videoModel;
        }

        public async Task<List<VideoPhotoModel>> GetVideoPhotoAsync()
        {
            var videophotos = await _appDbContext.VideoPhotos.ToListAsync();
            if (videophotos is null)
                return null!;

            var videophotoList = videophotos.Select(_mapper.Map<VideoPhotoModel>);
            return videophotoList.ToList();
        }

        public async Task<List<VideoModel>> GetVideosAsync()
        {
            var video = await _appDbContext.Videos.ToListAsync();
            if (video is null)
                return null!;

            var videoModelList = video.Select(_mapper.Map<VideoModel>);
            return videoModelList.ToList();
        }

        public async Task UpdateVideoModel(VideoModel videoModel)
        {
            try
            {
                var videoToUpdate = await _appDbContext.Videos.FindAsync(videoModel.Id);

                if (videoToUpdate != null)
                {
                    videoToUpdate.Qty = videoModel.Qty;
                    videoToUpdate.Model = videoModel.Model;
                    videoToUpdate.Status = videoModel.Status;
                    videoToUpdate.ComputerName = videoModel.ComputerName;
                    videoToUpdate.Tag = videoModel.Tag;
                    videoToUpdate.GeneralImage = videoModel.GeneralImage;
                    videoToUpdate.BarCodes = videoModel.BarCodes;
                    videoToUpdate.Serial = videoModel.Serial;

                    await _appDbContext.SaveChangesAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }


}
