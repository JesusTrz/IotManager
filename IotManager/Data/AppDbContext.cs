using IotManager.Models;
using Microsoft.EntityFrameworkCore;

namespace IotManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceConfigHistory> DeviceConfigHistory { get; set; }
    }
}
