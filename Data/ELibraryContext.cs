using electronic_library_6.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace electronic_library_6.Data
{
    public class ELibraryContext :DbContext
    {
        public ELibraryContext (DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Categories> Voting  { get; set; }
        public DbSet<PollingStations> PollingStations { get; set; }
      
    }
}
