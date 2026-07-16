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

        public List<Najlepszy_wynik> ListaGraczy { get; set; } = new List<Najlepszy_wynik>();

        public async Task OnGetAsync()
        {
            ListaGraczy = await _context.wyniki
                .OrderByDescending(x => x.Maksymalna_seria)
                .ToListAsync();
        }
    }
}