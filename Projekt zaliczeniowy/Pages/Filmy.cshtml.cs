using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt_zaliczeniowy.Pages
{
    [Authorize] 
    public class FilmyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}