using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Projekt_zaliczeniowy.Pages
{
    [Authorize]
    public class GeneratorModel : PageModel
    {
        private readonly AppDbContext _context;

        public GeneratorModel(AppDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public int liczba1 { get; set; }

        [BindProperty]
        public int liczba2 { get; set; } //licznik

        [BindProperty]
        public int liczba3 { get; set; } // mianownik
        [BindProperty]
        public int liczba4 { get; set; }

        [BindProperty]
        public string odpowiedz_uzytkownika { get; set; } = string.Empty; //zeby nie bylo ostrzezen 
        [BindProperty]
        public int seria { get; set; }
        [BindProperty]
        public string typ_zadania { get; set; } = string.Empty;
        [BindProperty]
        public int najlepszy_wynik_uzytkownika { get; set; }
        public string komunikat_zwrotny { get; set; } = string.Empty;

        


        public void OnGet()
        {
            Losuj_Zadanie();
            seria = 0;

            WczytajNajlepszyWynik();
        }
        public void OnPost()
        {
            if (typ_zadania == "ulamki2" || typ_zadania == "ulamki1")
            {
                Zadanie_ulamki();
            }
            else if (typ_zadania == "pierwiastki")
            {
                Zadanie_pierwiastki();
            }
            else if (typ_zadania == "potegi")
            {
                Zadanie_potegi();
            }
            


            Losuj_Zadanie();
            ModelState.Clear();//zeby zapomniec o 5 2 3

            WczytajNajlepszyWynik();
        }
        private void Losuj_Zadanie()
        {

            int los = Random.Shared.Next(4);
            if (los == 0)
            {
                liczba1 = Random.Shared.Next(2, 10);
                liczba2 = Random.Shared.Next(1, 10);
                liczba3 = Random.Shared.Next(2, 10);
                liczba4 = Random.Shared.Next(1, 20);
                typ_zadania = "ulamki2";
                while (liczba2 == liczba3)
                {
                    liczba2 = Random.Shared.Next(1, 10);
                }
            }
            else if (los == 1)
            {
                liczba1 = Random.Shared.Next(2, 10);
                liczba2 = Random.Shared.Next(1, 10);
                liczba3 = Random.Shared.Next(2, 10);
                liczba4 = Random.Shared.Next(1, 20);
                typ_zadania = "ulamki1";
                while (liczba2 == liczba3)
                {
                    liczba2 = Random.Shared.Next(1, 10);
                }
            }
            else if (los == 2)
            {
                liczba4 = Random.Shared.Next(1, 20);
                typ_zadania = "pierwiastki";

                liczba1 = liczba4 * liczba4;
            }
            else if (los == 3)
            {
                liczba1 = Random.Shared.Next(2, 5);
                liczba2 = Random.Shared.Next(2, 4);
                typ_zadania = "potegi";


            }


        }
        private void Zadanie_ulamki()
        {
            string poprawna_odpowiedz = SprawdzanieOdpowiedzi.WyliczOdpowiedzUlamki(liczba1, liczba2, liczba3);
            //Console.WriteLine(odp1.ToString() + "/" + odp2.ToString());
            if (odpowiedz_uzytkownika == poprawna_odpowiedz)
            {
                komunikat_zwrotny = "Dobrze";
                seria += 1;
            }
            else
            {
                komunikat_zwrotny = "Źle";
                SprawdzIZapiszRekord();
                seria = 0;

            }
            odpowiedz_uzytkownika = "";
        }
        private void Zadanie_pierwiastki()
        {

            string poprawna_odpowiedz = SprawdzanieOdpowiedzi.WyliczOdpowiedzPierwiastki(liczba4);
            if (odpowiedz_uzytkownika == poprawna_odpowiedz)
            {

            
                komunikat_zwrotny = "Dobrze";
                seria += 1;
            }
            else
            {
                komunikat_zwrotny = "Źle";
                SprawdzIZapiszRekord();
                seria = 0;
                
            }
            odpowiedz_uzytkownika = "";
        }
        private void Zadanie_potegi()
        {
            string poprawna_odpowiedz = SprawdzanieOdpowiedzi.WyliczOdpowiedzPotegi(liczba1, liczba2);

            if (odpowiedz_uzytkownika == poprawna_odpowiedz)
            {
                komunikat_zwrotny = "Dobrze";
                seria += 1;
            }
            else
            {
                komunikat_zwrotny = "Źle";
                SprawdzIZapiszRekord();
                seria = 0;
                
            }
            odpowiedz_uzytkownika = "";
        }
        private void WczytajNajlepszyWynik()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                string nazwa_uzytkownika = User.FindFirstValue(ClaimTypes.Name) ?? "Gosc";

                var rekord = _context.Wyniki.Include(w => w.Uzytkownik).FirstOrDefault(w => w.Uzytkownik.Login == nazwa_uzytkownika);

                if (rekord != null)
                {
                    najlepszy_wynik_uzytkownika = rekord.MaksymalnaSeria;
                }
            }
        }


        private void SprawdzIZapiszRekord()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                string nazwa_uzytkownika = User.FindFirstValue(ClaimTypes.Name) ?? "Gosc";

                var rekord_w_bazie = _context.Wyniki
                    .Include(w => w.Uzytkownik)
                    .FirstOrDefault(w => w.Uzytkownik.Login == nazwa_uzytkownika);

                if (rekord_w_bazie != null)
                {
                    if (seria > rekord_w_bazie.MaksymalnaSeria)
                    {
                        rekord_w_bazie.MaksymalnaSeria = seria;
                    }
                    _context.SaveChanges();
                }
            }
        }

    }

}
