using AutoMapper;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Model;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Extensions.Services
{
    public class MonitorService : IMonitorService
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;

        public MonitorService(FMIventoryDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> AddOrUpdateMonitorAsync(MonitorModels monitorModel)
        {
            if (monitorModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var monitor = _mapper.Map<Monitors>(monitorModel);

            if (monitorModel.Id != 0)
            {
                var findMonitor = await _appDbContext.Monitores.FindAsync(monitorModel.Id);

                if (findMonitor is null)
                    return (int)System.Net.HttpStatusCode.NotFound;


                findMonitor.Id = monitorModel.Id;
                findMonitor.ComputerName = monitorModel.ComputerName;
                findMonitor.GeneralImage = monitorModel.GeneralImage;
                findMonitor.Manufactor = monitorModel.Manufactor;
                findMonitor.Location = monitorModel.Location;
                findMonitor.Serial = monitorModel.Serial;
                findMonitor.Resolution = monitorModel.Resolution;
                findMonitor.Size = monitorModel.Size;
                findMonitor.Status = monitorModel.Status;
                await _appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }
            var chk = await _appDbContext.MonitorPhotos.Where(_ => _.ComputerName.ToLower().Equals(monitorModel.ComputerName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;


            _appDbContext.Monitores.Add(monitor);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteMonitorAsync(int id)
        {
            Monitors monitor = await _appDbContext.Monitores.FirstOrDefaultAsync(c => c.Id == id);
            if (monitor is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            _appDbContext.Monitores.Remove(monitor);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<MonitorModels>> GetMonitorAsync()
        {
            var monitors = await _appDbContext.Monitores.ToListAsync();
            if (monitors is null)
                return null!;

            var monitorsModelList = monitors.Select(_mapper.Map<MonitorModels>);
            return monitorsModelList.ToList();
        }

        public async Task<MonitorModels> GetMonitorByIdAsync(int id)
        {
            Monitors monitor = await _appDbContext.Monitores.FirstOrDefaultAsync(c => c.Id == id);
            if (monitor is null) return null!;

            var monitorModel = _mapper.Map<MonitorModels>(monitor);
            return monitorModel;
        }

        public async Task<List<MonitorPhotoModel>> GetPhotoByMonitorIdAsync(int MonitorsId)
        {
            var results = await _appDbContext.MonitorPhotos.Where(_ => _.MonitorsId == MonitorsId).Include(_ => _.Monitors).ToListAsync();
            var list = results.Select(_mapper.Map<MonitorPhotoModel>);
            return list.ToList();
        }

        public async Task<List<MonitorPhotoModel>> GetPhotosAsync()
        {
            var Monitorshotos = await _appDbContext.MonitorPhotos.ToListAsync();
            if (Monitorshotos is null)
                return null!;

            var computerphotoList = Monitorshotos.Select(_mapper.Map<MonitorPhotoModel>);
            return computerphotoList.ToList();
        }

        public async Task UpdateMonitorModels(MonitorModels monitorModel)
        {
            try
            {
                var monitorToUpdate = await _appDbContext.Computers.FindAsync(monitorModel.Id);

                if (monitorToUpdate != null)
                {
                    monitorToUpdate.Status = monitorModel.Status;
                    monitorToUpdate.ComputerName = monitorModel.ComputerName;
                    monitorToUpdate.Tag = monitorModel.Tag;
                    monitorToUpdate.GeneralImage = monitorModel.GeneralImage;
                    monitorToUpdate.BarCodes = monitorModel.BarCodes;
                    monitorToUpdate.Qty = monitorModel.Qty;

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
