using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RigMonitorAPI.Entities;
using RigMonitorAPI.Models;

namespace RigMonitorAPI.Authentication
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DeviceStats>()
                .HasKey(c => new { c.Timestamp, c.DeviceId });
            modelBuilder.Entity<WalletAddress>()
                .HasKey(c => new { c.Address, c.UserId });
        }
        public DbSet<Rig> Rig { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<DeviceStats> DeviceStats { get; set; }
        public DbSet<WalletAddress> WalletAddress { get; set; }
        public DbSet<DeviceGroup> DeviceGroup { get; set; }
        public DbSet<RigGroup> RigGroup { get; set; }

    }
}
