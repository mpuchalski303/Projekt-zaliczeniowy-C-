using Microsoft.EntityFrameworkCore;

namespace Projekt_zaliczeniowy
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<Wynik> Wyniki { get; set; }
        public DbSet<StatystykaBledow> StatystykiBledow { get; set; }
    }
}
