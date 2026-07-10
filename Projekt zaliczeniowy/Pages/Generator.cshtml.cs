using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.Design.Serialization;

namespace Projekt_zaliczeniowy.Pages
{
    public class GeneratorModel : PageModel
    {
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

        public string komunikat_zwrotny { get; set; } = string.Empty;



        public void OnGet()
        {
            Losuj_Zadanie();
            seria = 0;

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
            int odp1 = liczba1 * liczba2;
            int odp2 = liczba3;
            //Console.WriteLine(odp1.ToString() + "/" + odp2.ToString());
            if (odpowiedz_uzytkownika == odp1.ToString() + "/" + odp2.ToString())

            {
                komunikat_zwrotny = "Dobrze";
                seria += 1;
            }
            else
            {
                komunikat_zwrotny = "Źle";
                seria = 0;
            }
            odpowiedz_uzytkownika = "";
        }
        private void Zadanie_pierwiastki()
        {

            if (odpowiedz_uzytkownika == liczba4.ToString())
            {
                komunikat_zwrotny = "Dobrze";
                seria += 1;
            }
            else
            {
                komunikat_zwrotny = "Źle";
                seria = 0;

            }
            odpowiedz_uzytkownika = "";
        }
        private void Zadanie_potegi()
        {
            int odp = (int)Math.Pow(liczba1, liczba2);

            if (odpowiedz_uzytkownika == odp.ToString())
            {
                komunikat_zwrotny = "Dobrze";
                seria += 1;
            }
            else
            {
                komunikat_zwrotny = "Źle";
                seria = 0;

            }
            odpowiedz_uzytkownika = "";
        }
    }
}
