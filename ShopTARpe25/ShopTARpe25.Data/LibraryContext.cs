using Microsoft.EntityFrameworkCore;
using ShopTARpe25.Core.Domain;


namespace ShopTARpe25.Data
{
    //teha sellest classist DbContext, et saaks andmebaasi kasutada
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Loan> Loans { get; set; }
    }
}
