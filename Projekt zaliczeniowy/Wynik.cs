namespace Projekt_zaliczeniowy
{
    public class Wynik
    {
        public int Id{ get; set; }
        public int UzytkownikId{ get; set; }
        public Uzytkownik Uzytkownik{ get; set; } = null!;
        public int MaksymalnaSeria{ get; set; }
    }
}