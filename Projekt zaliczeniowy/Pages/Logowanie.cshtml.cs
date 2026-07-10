using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Security.Claims;

namespace Projekt_zaliczeniowy.Pages
{
    public class LogowanieModel : PageModel
    {
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
            
            if (login == "student" && haslo == "maslo123")
            {
                
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login),
                    new Claim(ClaimTypes.Role, "User")
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                
                var authProperties = new AuthenticationProperties
                {
                   
                    IsPersistent = false
                };


                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                
                return RedirectToPage("/Filmy");
            }

            
            blad_logowania = "Niepoprawny login lub hasło!";
            return Page();
        }
    }

    
}
