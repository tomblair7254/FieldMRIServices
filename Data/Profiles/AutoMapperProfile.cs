using AutoMapper;
using FieldMRIServices.Entities;
using FieldMRIServices.Model;
namespace FieldMRIServices.Data.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //from databse - user
            CreateMap<Inventory, InventoryModel>();
            CreateMap<InventoryPhoto, PhotoModel>();

            //from user -database
            CreateMap<InventoryModel, Inventory>();
            CreateMap<PhotoModel, InventoryPhoto>();

            CreateMap<Computer, ComputerModel>();
            CreateMap<ComputerModel, Computer>();

            CreateMap<ComputerPhoto, ComputerPhotoModel>();
            CreateMap<ComputerPhotoModel, ComputerPhoto>();

            CreateMap<Modisk, ModiskModel>();
            CreateMap<ModiskModel, Modisk>();

            CreateMap<Harddrive, HarddriveModel>();
            CreateMap<HarddriveModel, Harddrive>();

            CreateMap<HarddrivePhotoModel, HarddrivePhoto>();
            CreateMap<HarddrivePhoto, HarddrivePhotoModel>();

            CreateMap<VideoModel, Video>();
            CreateMap<Video, VideoModel>();

            CreateMap<MemoryModel, Memory>();
            CreateMap<Memory, MemoryModel>();

            CreateMap<Fiber, FiberModel>();
            CreateMap<FiberModel, Fiber>();

            CreateMap<Monitors, MonitorModels>();
            CreateMap<MonitorModels, Monitors>();

            CreateMap<IEEE, IEEEModel>();
            CreateMap<IEEEModel, IEEE>();

            CreateMap<DVD, DVDModel>();
            CreateMap<DVDModel, DVD>();
            CreateMap<DVDPhoto, DVDPhotoModel>();
            CreateMap<DVDPhotoModel, DVDPhoto>();


        }
    }
}
