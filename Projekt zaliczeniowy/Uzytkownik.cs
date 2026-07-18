namespace Projekt_zaliczeniowy
{
    public class Uzytkownik
    {
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string HasloHash { get; set; } = string.Empty;
        public bool CzyAdmin { get; set; }

        public Wynik? Wynik { get; set; }
    }
}