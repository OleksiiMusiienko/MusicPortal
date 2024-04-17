using Microsoft.EntityFrameworkCore;
using Portal.DAL.Entities;

namespace Portal.DAL.EF
{
    public class PortalContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public PortalContext(DbContextOptions<PortalContext> options)
            :base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
