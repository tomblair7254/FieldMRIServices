using AutoMapper;
using FieldMRIServices.Entities;
using FieldMRIServices.Model;

namespace FieldMRIServices.Data.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Existing mappings
            CreateMap<Inventory, InventoryModel>();
            CreateMap<InventoryModel, Inventory>();

            // Add missing mappings for ComputerModel and Computer
            CreateMap<ComputerModel, Computer>();
            CreateMap<Computer, ComputerModel>();

            // Add mappings for UserLogs and UserLogsModel
            CreateMap<UserLogsModel, UserLogs>();
            CreateMap<UserLogs, UserLogsModel>();
        }
    }
}