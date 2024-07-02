using AutoMapper;
using FieldMRIServices.Data;
using FieldMRIServices.Entities;
using FieldMRIServices.Extensions.Interfaces;
using FieldMRIServices.Model;
using Microsoft.EntityFrameworkCore;


namespace FieldMRIServices.Services
{
    public class ComputerServices : IComputerServices
    {
        private readonly FMIventoryDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ComputerServices()
        {
        }

        public ComputerServices(FMIventoryDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<int> AddOrUpdateComputerAsync(ComputerModel computerModel)
        {
            if (computerModel is null)
                return (int)System.Net.HttpStatusCode.BadRequest;

            var computer = _mapper.Map<Computer>(computerModel);

            if (computerModel.Id != 0)
            {
                var findComputer = await _appDbContext.Computers.FindAsync(computerModel.Id);
                if (findComputer is null)
                    return (int)System.Net.HttpStatusCode.NotFound;


                findComputer.Id = computerModel.Id;
                findComputer.ComputerName = computerModel.ComputerName;
                findComputer.GeneralImage = computerModel.GeneralImage;
                findComputer.Tag = computerModel.Tag;
                findComputer.Network = computerModel.Network;
                findComputer.Memory = computerModel.Memory;
                findComputer.SCSI = computerModel.SCSI;
                findComputer.Video = computerModel.Video;
                findComputer.Location = computerModel.Location;
                findComputer.MultipleCom = computerModel.MultipleCom;
                findComputer.Player = computerModel.Player;
                findComputer.HardDrive = computerModel.HardDrive;
                findComputer.Status = computerModel.Status;
                findComputer.BarCodes = computerModel.BarCodes;
                await _appDbContext.SaveChangesAsync();
                return (int)System.Net.HttpStatusCode.OK;
            }

            var chk = await _appDbContext.ComputerPhotos.Where(_ => _.ComputerName.ToLower().Equals(computerModel.ComputerName.ToLower())).FirstOrDefaultAsync();
            if (chk is not null)
                return (int)System.Net.HttpStatusCode.NotAcceptable;


            _appDbContext.Computers.Add(computer);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.Created;
        }

        public async Task<int> DeleteComputerAsync(int id)
        {
            Computer computer = await _appDbContext.Computers.FirstOrDefaultAsync(c => c.Id == id);
            if (computer is null)
                return (int)System.Net.HttpStatusCode.NotFound;

            _appDbContext.Computers.Remove(computer);
            await _appDbContext.SaveChangesAsync();
            return (int)System.Net.HttpStatusCode.OK;
        }

        public async Task<List<ComputerModel>> GetComputerAsync()
        {

            var computers = await _appDbContext.Computers.ToListAsync();
            if (computers is null)
                return null!;

            var computerModelList = computers.Select(_mapper.Map<ComputerModel>);
            return computerModelList.ToList();
        }

        public async Task<ComputerModel> GetComputerByIdAsync(int id)
        {
            Computer computer = await _appDbContext.Computers.FirstOrDefaultAsync(c => c.Id == id);
            if (computer is null) return null!;

            var computerModel = _mapper.Map<ComputerModel>(computer);
            return computerModel;
        }

        public async Task<List<ComputerPhotoModel>> GetPhotoByComputerIdAsync(int ComputerId)
        {
            var results = await _appDbContext.ComputerPhotos.Where(_ => _.ComputerId == ComputerId).Include(_ => _.Computer).ToListAsync();
            var list = results.Select(_mapper.Map<ComputerPhotoModel>);
            return list.ToList();
        }



        public async Task<List<ComputerPhotoModel>> GetPhotosAsync()
        {
            var computerhotos = await _appDbContext.ComputerPhotos.ToListAsync();
            if (computerhotos is null)
                return null!;

            var computerphotoList = computerhotos.Select(_mapper.Map<ComputerPhotoModel>);
            return computerphotoList.ToList();
        }

        public async Task UpdateComputerModel(ComputerModel computerModel)
        {
            try
            {
                var computerToUpdate = await _appDbContext.Computers.FindAsync(computerModel.Id);

                if (computerToUpdate != null)
                {
                    computerToUpdate.Status = computerModel.Status;
                    computerToUpdate.ComputerName = computerModel.ComputerName;
                    computerToUpdate.Tag = computerModel.Tag;
                    computerToUpdate.GeneralImage = computerModel.GeneralImage;
                    computerToUpdate.BarCodes = computerModel.BarCodes;
                    computerToUpdate.Serial = computerModel.Serial;
                    computerToUpdate.Location = computerModel.Location;
                    computerToUpdate.HardDrive = computerModel.HardDrive;
                    computerToUpdate.Memory = computerModel.Memory;
                    computerToUpdate.Player = computerModel.Player;
                    computerToUpdate.Network = computerModel.Network;
                    computerToUpdate.SCSI = computerModel.SCSI;
                    computerToUpdate.MultipleCom = computerModel.MultipleCom;
                    computerToUpdate.Video = computerModel.Video;
                    computerToUpdate.Qty = computerModel.Qty;

                    await _appDbContext.SaveChangesAsync();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static byte[] GetBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Seek(0, SeekOrigin.Begin);
            stream.ReadAsync(bytes, 0, bytes.Length);
            stream.Dispose();
            return bytes;
        }
    }
}
