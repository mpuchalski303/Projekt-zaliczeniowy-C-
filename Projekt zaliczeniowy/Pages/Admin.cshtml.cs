using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Projekt_zaliczeniowy.Pages
{
    [Authorize]
    public class AdminModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Uzytkownik> _hasher = new();

        public AdminModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string NowyLogin { get; set; } = string.Empty;

        [BindProperty]
        public string NoweHaslo { get; set; } = string.Empty;

        public string Komunikat { get; set; } = string.Empty;

        public List<Uzytkownik> Uczniowie { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!await CzyZalogowanyJestAdminemAsync())
            {
                return RedirectToPage("/Index");
            }

            await WczytajUczniowAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!await CzyZalogowanyJestAdminemAsync())
            {
                return RedirectToPage("/Index");
            }

            bool loginZajety = await _context.Uzytkownicy.AnyAsync(u => u.Login == NowyLogin);

            if (loginZajety)
            {
                Komunikat = "Ten login jest już zajęty.";
            }
            else
            {
                var nowyUzytkownik = new Uzytkownik { Login = NowyLogin };
                nowyUzytkownik.HasloHash = _hasher.HashPassword(nowyUzytkownik, NoweHaslo);

                _context.Uzytkownicy.Add(nowyUzytkownik);
                await _context.SaveChangesAsync();

                _context.Wyniki.Add(new Wynik { UzytkownikId = nowyUzytkownik.Id, MaksymalnaSeria = 0 });
                await _context.SaveChangesAsync();

                Komunikat = $"Dodano konto ucznia: {NowyLogin}";
            }

            await WczytajUczniowAsync();
            return Page();
        }

        private async Task WczytajUczniowAsync()
        {
            Uczniowie = await _context.Uzytkownicy.Where(u => !u.CzyAdmin).ToListAsync();
        }

        private async Task<bool> CzyZalogowanyJestAdminemAsync()
        {
            string? login = User.Identity?.Name;
            if (login == null) return false;

            var uzytkownik = await _context.Uzytkownicy.FirstOrDefaultAsync(u => u.Login == login);
            return uzytkownik != null && uzytkownik.CzyAdmin;
        }
    }
}