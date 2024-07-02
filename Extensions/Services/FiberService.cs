using AutoMapper;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Model;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Extensions.Services
{
    public class FiberService : IFiberService
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;

        public FiberService(FMIventoryDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> AddOrUpdateFiberAsync(FiberModel fiberModel)
        {
            if (fiberModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var fiber = _mapper.Map<Fiber>(fiberModel);

            if (fiber.Id != 0)
            {
                var findFiber = await _appDbContext.Fibers.FindAsync(fiberModel.Id);
                if (findFiber is null)
                    return (int)System.Net.HttpStatusCode.NotFound;


                findFiber.Id = fiberModel.Id;
                findFiber.ComputerName = fiberModel.ComputerName;
                findFiber.GeneralImage = fiberModel.GeneralImage;
                findFiber.Model = fiberModel.Model;
                findFiber.Serial = fiberModel.Serial;
                findFiber.Status = fiberModel.Status;

                await _appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await _appDbContext.FiberPhotos.Where(_ => _.ComputerName.ToLower().Equals(fiberModel.ComputerName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;


            _appDbContext.Fibers.Add(fiber);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteFiberAsync(int id)
        {
            Fiber fiber = await _appDbContext.Fibers.FirstOrDefaultAsync(c => c.Id == id);
            if (fiber is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            _appDbContext.Fibers.Remove(fiber);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<FiberModel>> GetFiberAsync()
        {
            var fibers = await _appDbContext.Fibers.ToListAsync();
            if (fibers is null)
                return null!;

            var fiberModelList = fibers.Select(_mapper.Map<FiberModel>);
            return fiberModelList.ToList(); ;
        }

        public async Task<FiberModel> GetFiberByIdAsync(int id)
        {
            Fiber fiber = await _appDbContext.Fibers.FirstOrDefaultAsync(c => c.Id == id);
            if (fiber is null) return null!;

            var fiberModel = _mapper.Map<FiberModel>(fiber);
            return fiberModel; ;
        }

        public async Task<List<FiberPhotoModel>> GetPhotoByFiberIdAsync(int fiberId)
        {
            var results = await _appDbContext.FiberPhotos.Where(_ => _.FiberId == fiberId).Include(_ => _.Fiber).ToListAsync();
            var list = results.Select(_mapper.Map<FiberPhotoModel>);
            return list.ToList();
        }

        public async Task<List<FiberPhotoModel>> GetPhotosAsync()
        {
            var fiberphoto = await _appDbContext.FiberPhotos.ToListAsync();
            if (fiberphoto is null)
                return null!;

            var computerphotoList = fiberphoto.Select(_mapper.Map<FiberPhotoModel>);
            return computerphotoList.ToList();
        }
    }
}
