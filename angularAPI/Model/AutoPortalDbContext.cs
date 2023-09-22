using Microsoft.EntityFrameworkCore;

namespace AutoPortal.Model
{
    public class AutoPortalDbContext : DbContext
    {
        public AutoPortalDbContext(DbContextOptions<AutoPortalDbContext> options) : base(options)
        {
        }
        public DbSet<portaluser> portalusers { get; set; }
        public DbSet<userpolicylist> userpolicylists { get; set; }
    }
}
