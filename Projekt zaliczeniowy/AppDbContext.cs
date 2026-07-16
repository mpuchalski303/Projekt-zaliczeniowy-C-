using Microsoft.EntityFrameworkCore;

namespace Projekt_zaliczeniowy
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Najlepszy_wynik> wyniki { get; set; }
    }
}
