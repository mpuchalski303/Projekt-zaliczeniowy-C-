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
        public static string Mnozenie_odp(int liczba1, int liczba2)
        {
            return (liczba1 * liczba2).ToString();
        }

        public static string Dodawanie_mnozenie_odp(int liczba1, int liczba2, int liczba3, int gdzieZnak)
        {
            int wynik = 0;

            if (gdzieZnak == 1)
            {
                wynik = liczba1 - liczba2 * liczba3;
            }
            else if (gdzieZnak == 2)
            {
                wynik = liczba1 + liczba2 * liczba3;
            }
            else if (gdzieZnak == 3)
            {
                wynik = liczba1 * liczba2 + liczba3;
            }
            else if (gdzieZnak == 4)
            {
                wynik = liczba1 * liczba2 - liczba3;
            }

            return wynik.ToString();
        }
    }
}