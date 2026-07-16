using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Security.Claims;

namespace Projekt_zaliczeniowy.Pages
{

    public class LogowanieModel : PageModel
    {
        private readonly AppDbContext _context;

        public LogowanieModel(AppDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public string login { get; set; }
        [BindProperty]
        public string haslo { get; set; }

        public string blad_logowania { get; set; }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            var gracz = _context.wyniki.FirstOrDefault(x => x.Nazwa_uzytkownika == login && x.Haslo == haslo);
            if (gracz != null)
            {
                
                var claims = new List<Claim>
                {
                    
                    new Claim(ClaimTypes.Name, gracz.Nazwa_uzytkownika)
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
