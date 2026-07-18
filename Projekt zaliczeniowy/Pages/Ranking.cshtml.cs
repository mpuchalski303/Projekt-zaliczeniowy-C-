using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Projekt_zaliczeniowy.Pages
{
    public class RankingModel : PageModel
    {
        private readonly AppDbContext _context;

        public RankingModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Wynik> ListaGraczy { get; set; } = new List<Wynik>();

        public async Task OnGetAsync()
        {
            ListaGraczy = await _context.Wyniki
                .Include(w => w.Uzytkownik)
                .OrderByDescending(w => w.MaksymalnaSeria)
                .ToListAsync();
        }
    }
}