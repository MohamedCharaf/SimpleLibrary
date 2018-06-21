using Microsoft.EntityFrameworkCore;

namespace SimpleLibrary.API.Domain
{
    public class SimpleLibraryDbContext : DbContext
    {
        public SimpleLibraryDbContext(DbContextOptions<SimpleLibraryDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
