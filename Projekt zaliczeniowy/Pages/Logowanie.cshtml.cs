using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Projekt_zaliczeniowy.Pages
{

    public class LogowanieModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly PasswordHasher<Uzytkownik> _hasher = new();

        public LogowanieModel(AppDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string Login { get; set; } = string.Empty;
        [BindProperty]
        public string Haslo { get; set; } = string.Empty;

        public string blad_logowania { get; set; } = string.Empty;

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var uzytkownik = await _context.Uzytkownicy.FirstOrDefaultAsync(u => u.Login == Login);

            bool poprawneHaslo = uzytkownik != null &&
                _hasher.VerifyHashedPassword(uzytkownik, uzytkownik.HasloHash, Haslo) == PasswordVerificationResult.Success;

            if (uzytkownik != null && poprawneHaslo)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, uzytkownik.Login)
            };  

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

               
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                
                return RedirectToPage("/Generator");
            }



            blad_logowania = "Niepoprawny login lub hasło!";
            return Page();
        }

    }

    
}
