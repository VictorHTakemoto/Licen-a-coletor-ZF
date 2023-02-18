using DeviceLicense.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace DeviceLicense.Model
{
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions<DeviceDbContext> options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }
    }
}
