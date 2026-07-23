namespace Projekt_zaliczeniowy
{
    public static class SprawdzanieOdpowiedzi
    {
        public static string Ulamki_odp(int liczba1, int liczba2, int liczba3)
        {
            return $"{liczba1 * liczba2}/{liczba3}";
        }

        public static string Pierwiastki_odp(int liczba4)
        {
            return liczba4.ToString();
        }

        public static string Potegi_odp(int liczba1, int liczba2)
        {
            return ((int)Math.Pow(liczba1, liczba2)).ToString();
        }
    }
}