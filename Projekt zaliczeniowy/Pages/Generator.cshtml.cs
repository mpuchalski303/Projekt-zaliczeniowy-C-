using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt_zaliczeniowy.Pages
{
    public class GeneratorModel : PageModel
    {
        [BindProperty]
        public int liczba { get; set; }

        [BindProperty]
        public int licznik { get; set; }

        [BindProperty]
        public int mianownik { get; set; }

        [BindProperty]//wtedy zapamietuje te zmienne 
        public string odpowiedz_uzytkownika { get; set; }
        public string komunikat_zwrotny { get; set; }


        public void OnGet()
        {
            liczba = 5;
            licznik = 2;
            mianownik = 3;
            

        }
        public void OnPost()
        {
            

            int odp1 = liczba * licznik;
            int odp2 = mianownik;
            Console.WriteLine(odp1.ToString() + "/" + odp2.ToString());
            if (odpowiedz_uzytkownika == odp1.ToString() + "/" + odp2.ToString())
                
            {
                komunikat_zwrotny = "Dobrze";
            }
            else
            {
                komunikat_zwrotny = "Źle";
            }
            liczba = 7;
            licznik = 8;
            mianownik = 3;

            ModelState.Clear();
        }



    }
}
