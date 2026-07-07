using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt_zaliczeniowy.Pages
{
    public class IndexModel : PageModel
    {
        public string Imie { get; set;}
        public string Opis { get; set; }

        public void OnGet()
        {
            Imie = "Miłosz";
            Opis = "Specjalizuje się w pomaganiu najbardziej wymagającym uczniom z dużymi problemami z matematyką. " +
                "Prowadzę lekcje jak najbardziej rzeczowo się da i staram się nie marnować ani minuty czasu pracy, " +
                "ponieważ moim priorytetem jest jak największa pomoc naukowa. Zajęcia prowadzę od 1 do 2 godzin (po dłuższym czasie lekcja staje się mniej efektywna)." +
                " Lekcje są skierowane dla uczniów klas podstawowych oraz liceów. Przygotowuję również czysto pod maturę z matematyki, z której uzyskałem 98% i chętnie " +
                "pomagam uzyskać podobny wynik moim uczniom. Zachęcam do kontaktu.";
        }
    }
}
