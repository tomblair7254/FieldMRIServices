using FieldMRIServices.Entities;
using FieldMRIServices.Model;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Data
{
    public class FMIventoryDbContext : IdentityDbContext<IdentityUser, ApplicationRoles, string>
    {
        private readonly IConfiguration _configuration;

        public FMIventoryDbContext(DbContextOptions<FMIventoryDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlite(connectionString)
                              .EnableSensitiveDataLogging(); // Enable detailed error logging
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationRoles>()
                .Property(r => r.PermissionsString)
                .HasColumnName("Permissions");

            // Additional configurations for the Test entity can be added here if needed
        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<UserLogs> UserLogs { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    }
}
