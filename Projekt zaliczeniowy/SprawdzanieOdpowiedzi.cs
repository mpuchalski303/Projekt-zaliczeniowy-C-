namespace Projekt_zaliczeniowy
{
    public static class SprawdzanieOdpowiedzi
    {
        public static string WyliczOdpowiedzUlamki(int liczba1, int liczba2, int liczba3)
        {
            return $"{liczba1 * liczba2}/{liczba3}";
        }

        public static string WyliczOdpowiedzPierwiastki(int liczba4)
        {
            return liczba4.ToString();
        }

        public static string WyliczOdpowiedzPotegi(int liczba1, int liczba2)
        {
            return ((int)Math.Pow(liczba1, liczba2)).ToString();
        }
    }
}