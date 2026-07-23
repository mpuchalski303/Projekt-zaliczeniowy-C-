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
        public string nowy_login { get; set; } = string.Empty;

        [BindProperty]
        public string nowe_haslo { get; set; } = string.Empty;

        public string komunikat { get; set; } = string.Empty;

        public List<Uzytkownik> Uczniowie { get; set; } = new();
        public List<StatystykaBledow> statystyki { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (!await czy_zalogowany_admin_async())
            {
                return RedirectToPage("/Index");
            }

            await wczytaj_uczniow_async();
            await WczytajStatystykiAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!await czy_zalogowany_admin_async())
            {
                return RedirectToPage("/Index");
            }

            bool loginZajety = await _context.Uzytkownicy.AnyAsync(u => u.Login == nowy_login);

            if (loginZajety)
            {
                komunikat = "Ten login jest już zajęty.";
            }
            else
            {
                var nowyUzytkownik = new Uzytkownik { Login = nowy_login };
                nowyUzytkownik.HasloHash = _hasher.HashPassword(nowyUzytkownik, nowe_haslo);

                _context.Uzytkownicy.Add(nowyUzytkownik);
                await _context.SaveChangesAsync();

                _context.Wyniki.Add(new Wynik { UzytkownikId = nowyUzytkownik.Id, MaksymalnaSeria = 0 });
                await _context.SaveChangesAsync();

                komunikat = $"Dodano konto ucznia: {nowy_login}";
            }

            await wczytaj_uczniow_async();
            await WczytajStatystykiAsync();
            return Page();
        }

        private async Task wczytaj_uczniow_async()
        {
            Uczniowie = await _context.Uzytkownicy.Include(u => u.Wynik).Where(u => !u.CzyAdmin).ToListAsync();
        }
        private async Task WczytajStatystykiAsync()
        {
            statystyki = await _context.StatystykiBledow.OrderByDescending(s => s.liczba_bledow).ToListAsync();
        }

        private async Task<bool> czy_zalogowany_admin_async()
        {
            string? login = User.Identity?.Name;
            if (login == null) return false;

            var uzytkownik = await _context.Uzytkownicy.FirstOrDefaultAsync(u => u.Login == login);
            return uzytkownik != null && uzytkownik.CzyAdmin;
        }
        public string NazwaZadania(string typ_zadania)
        {
            if (typ_zadania == "ulamki1" || typ_zadania == "ulamki2")
            {
                return "Ułamki";
            }
            else if (typ_zadania == "pierwiastki")
            {
                return "Pierwiastki";
            }
            else if (typ_zadania == "potegi")
            {
                return "Potęgi";
            }
            else if (typ_zadania == "mnozenie_calkowite")
            {
                return "Mnożenie liczb całkowitych";
            }
            else if (typ_zadania == "dodawanie_mnozenie_calkowite")
            {
                return "Dodawanie i mnożenie liczb całkowitych";
            }
            else
            {
                return typ_zadania;
            }
        }
    }
}