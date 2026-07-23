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
     
        public int najlepszy_wynik_uzytkownika { get; set; }
        [BindProperty]
        public int liczba_calkowita1 { get; set; }
        [BindProperty]
        public int liczba_calkowita2 { get; set; }
        [BindProperty]
        public int liczba_calkowita3 { get; set; }
        [BindProperty]
        public int gdzie_znak { get; set; }
        public string komunikat_zwrotny { get; set; } = string.Empty;

        


        public void OnGet()
        {
            Losuj_Zadanie();
            seria = 0;

            Wczytaj_Najlepszy_Wynik();
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
            else if (typ_zadania == "mnozenie_calkowite")
            {
                Zadanie_mnozenie_calkowite();
            }
            else if (typ_zadania == "dodawanie_mnozenie_calkowite")
            {
                Zadanie_dodawanie_mnozenie_calkowite();
            }


            Losuj_Zadanie();
            ModelState.Clear();//zeby zapomniec o 5 2 3

            Wczytaj_Najlepszy_Wynik();
        }
        private void Losuj_Zadanie()
        {

            int los = Random.Shared.Next(6);
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
            else if (los == 4)
            {
                typ_zadania = "mnozenie_calkowite";
                liczba_calkowita1 = Random.Shared.Next(-10, 10);
                liczba_calkowita2 = Random.Shared.Next(-10, 10);

                if (liczba_calkowita1 == 0) liczba_calkowita1 = 1;
                if (liczba_calkowita2 == 0) liczba_calkowita2 = 1;
            }
            else if(los == 5)
            {
                typ_zadania = "dodawanie_mnozenie_calkowite";
                liczba_calkowita1 = Random.Shared.Next(-8, 8);
                liczba_calkowita2 = Random.Shared.Next(-8, 8);
                liczba_calkowita3 = Random.Shared.Next(-8, 8);
                gdzie_znak = Random.Shared.Next(1, 5);

                if (liczba_calkowita1 == 0) liczba_calkowita1 = 1;
                if (liczba_calkowita2 == 0) liczba_calkowita2 = 1;
                if (liczba_calkowita3 == 0) liczba_calkowita3 = 1;
            }


        }
        private void Zadanie_ulamki()
        {
            string poprawna_odpowiedz = SprawdzanieOdpowiedzi.Ulamki_odp(liczba1, liczba2, liczba3);
            //Console.WriteLine(odp1.ToString() + "/" + odp2.ToString());
            if (odpowiedz_uzytkownika == poprawna_odpowiedz)
            {
                komunikat_zwrotny = "Dobrze";
                seria += 1;
                Zapisz_Probe(true);
            }
            else
            {
                komunikat_zwrotny = "Źle";
                Zapisz_Rekord();
                Zapisz_Blad(typ_zadania);
                Zapisz_Probe(false);
                seria = 0;

            }
            odpowiedz_uzytkownika = "";
        }
        private void Zadanie_pierwiastki()
        {

            string poprawna_odpowiedz = SprawdzanieOdpowiedzi.Pierwiastki_odp(liczba4);
            if (odpowiedz_uzytkownika == poprawna_odpowiedz)
            {

            
                komunikat_zwrotny = "Dobrze";
                seria += 1;
                Zapisz_Probe(true);
            }
            else
            {
                komunikat_zwrotny = "Źle";
                Zapisz_Rekord();
                Zapisz_Blad(typ_zadania);
                Zapisz_Probe(false);
                seria = 0;
                
            }
            odpowiedz_uzytkownika = "";
        }
        private void Zadanie_potegi()
        {
            string poprawna_odpowiedz = SprawdzanieOdpowiedzi.Potegi_odp(liczba1, liczba2);

            if (odpowiedz_uzytkownika == poprawna_odpowiedz)
            {
                komunikat_zwrotny = "Dobrze";
                seria += 1;
                Zapisz_Probe(true);
            }
            else
            {
                komunikat_zwrotny = "Źle";
                Zapisz_Rekord();
                Zapisz_Blad(typ_zadania);
                Zapisz_Probe(false);
                seria = 0;
                
            }
            odpowiedz_uzytkownika = "";
        }
        private void Zadanie_mnozenie_calkowite()
        {
            string poprawna_odpowiedz = SprawdzanieOdpowiedzi.Mnozenie_odp(liczba_calkowita1, liczba_calkowita2);

            if (odpowiedz_uzytkownika == poprawna_odpowiedz)
            {
                komunikat_zwrotny = "Dobrze";
                seria += 1;
                Zapisz_Probe(true);
            }
            else
            {
                komunikat_zwrotny = "Źle";
                Zapisz_Rekord();
                Zapisz_Blad(typ_zadania);
                Zapisz_Probe(false);
                seria = 0;
            }
            odpowiedz_uzytkownika = "";
        }

        private void Zadanie_dodawanie_mnozenie_calkowite()
        {
            string poprawna_odpowiedz = SprawdzanieOdpowiedzi.Dodawanie_mnozenie_odp(
                liczba_calkowita1, liczba_calkowita2, liczba_calkowita3, gdzie_znak);

            if (odpowiedz_uzytkownika == poprawna_odpowiedz)
            {
                komunikat_zwrotny = "Dobrze";
                seria += 1;
                Zapisz_Probe(true);
            }
            else
            {
                komunikat_zwrotny = "Źle";
                Zapisz_Rekord();
                Zapisz_Blad(typ_zadania);
                Zapisz_Probe(false);
                seria = 0;
            }
            odpowiedz_uzytkownika = "";
        }
        private void Wczytaj_Najlepszy_Wynik()
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


        private void Zapisz_Rekord()
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
        private void Zapisz_Blad(string typ_zadania)
        {
            var statystyka = _context.StatystykiBledow.FirstOrDefault(s => s.typ_zadania == typ_zadania);

            if (statystyka == null)
            {
                statystyka = new StatystykaBledow { typ_zadania = typ_zadania, liczba_bledow = 0 };
                _context.StatystykiBledow.Add(statystyka);
            }

            statystyka.liczba_bledow += 1;
            _context.SaveChanges();
        }

        private void Zapisz_Probe(bool poprawna)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                string nazwa_uzytkownika = User.FindFirstValue(ClaimTypes.Name) ?? "Gosc";

                var rekord = _context.Wyniki
                    .Include(w => w.Uzytkownik)
                    .FirstOrDefault(w => w.Uzytkownik.Login == nazwa_uzytkownika);

                if (rekord != null)
                {
                    rekord.liczba_prob += 1;

                    if (poprawna)
                    {
                        rekord.liczba_poprawnych += 1;
                    }

                    _context.SaveChanges();
                }
            }
        }

    }

}
