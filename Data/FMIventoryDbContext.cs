using FieldMRIServices.Entities;
using FieldMRIServices.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Data
{
    public class FMIventoryDbContext : IdentityDbContext<IdentityUser>
    {
        public FMIventoryDbContext(DbContextOptions<FMIventoryDbContext> options) : base(options)
        {

        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<InventoryPhoto> Photos { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<ComputerPhoto> ComputerPhotos { get; set; }
        public DbSet<Modisk> Modisks { get; set; }
        public DbSet<MosdiskPhoto> ModiskPhotos { get; set; }
        public DbSet<Harddrive> Harddrives { get; set; }
        public DbSet<HarddrivePhoto> HarddrivePhotos { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<VideoPhoto> VideoPhotos { get; set; }
        public DbSet<Memory> Memories { get; set; }
        public DbSet<MemoryPhoto> MemoryPhotos { get; set; }
        public DbSet<Fiber> Fibers { get; set; }
        public DbSet<FiberPhoto> FiberPhotos { get; set; }
        public DbSet<Monitors> Monitores { get; set; }
        public DbSet<MonitorPhoto> MonitorPhotos { get; set; }
        public DbSet<IEEE> IEEEs { get; set; }
        public DbSet<IEEEPhotosModel> IEEEPhotos { get; set; }
        public DbSet<DVDModel> DVDs { get; set; }
        public DbSet<DVDPhotoModel> DVDPhotos { get; set; }
        public DbSet<Other> Others { get; set; }
        public DbSet<OtherPhoto> OtherPhotos { get; set; }
    }
}
