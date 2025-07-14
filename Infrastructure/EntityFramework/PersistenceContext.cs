using Domain.Entities;
using Domain.Entities.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace Infrastructure.EntityFramework
{
    public class PersistenceContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration configuration, IHttpContextAccessor? httpContextAccessor= null) : base(options)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
      
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if(modelBuilder == null)
            {
                return;
            }
            modelBuilder.HasDefaultSchema(_configuration.GetConnectionString("BaseSchema"));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistenceContext).Assembly);

            base.OnModelCreating(modelBuilder);

            //Identity
           

            modelBuilder.Entity<Vehicle>()
                .Property(p => p.Id)
                .UseIdentityColumn();

            modelBuilder.Entity<VehicleType>()
                .Property(p => p.Id)
                .UseIdentityColumn();
                        

            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.VehicleType)
                .WithMany(t => t.Vehicles)
                .HasForeignKey(v => v.VehicleTypeId);

            //Seed          

            modelBuilder.Entity<VehicleType>().HasData(
                new VehicleType { Id = 1, Name = "Sedán", CreatedBy = "seed", CreatedAt = DateTime.UtcNow },
                new VehicleType { Id = 2, Name = "Coupé", CreatedBy = "seed", CreatedAt = DateTime.UtcNow },
                new VehicleType { Id = 3, Name = "SUV", CreatedBy = "seed", CreatedAt = DateTime.UtcNow },
                new VehicleType { Id = 4, Name = "Camioneta", CreatedBy = "seed", CreatedAt = DateTime.UtcNow }
            );

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = 1, PlateNumber = "XXX000", Brand = "Renault", Model = "Logan", Year = 2015, VehicleTypeId = 1,BookingValuePerDay = 85000, CreatedBy = "seed", CreatedAt = DateTime.UtcNow },
                new Vehicle { Id = 2, PlateNumber = "AAA999", Brand = "Mazda", Model = "CX-30", Year = 2022, VehicleTypeId = 3, BookingValuePerDay = 150000, CreatedBy = "seed", CreatedAt = DateTime.UtcNow }
            );

           
         
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries<AuditableEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = GetCurrentUsername(); 
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Property(e => e.CreatedAt).IsModified = false;
                    entry.Property(e => e.CreatedBy).IsModified = false;
                    entry.Entity.ModifiedAt = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = GetCurrentUsername(); 
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private string GetCurrentUsername()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "system";
        }
    }
}
