using Xunit;
using Projekt_zaliczeniowy;

namespace Projekt_zaliczeniowy.Tests
{
    public class SprawdzanieOdpowiedziTests
    {
        [Fact]
        public void Ulamki_test()
        {
            string wynik = SprawdzanieOdpowiedzi.Ulamki_odp(3, 4, 5);
            Assert.Equal("12/5", wynik);
        }

        [Fact]
        public void Pierwiastki_test()
        {
            string wynik = SprawdzanieOdpowiedzi.Pierwiastki_odp(7);
            Assert.Equal("7", wynik);
        }

        [Fact]
        public void Potegi_test()
        {
            string wynik = SprawdzanieOdpowiedzi.Potegi_odp(2, 3);
            Assert.Equal("8", wynik);
        }
        [Fact]
        public void Mnozenie_test()
        {
            string wynik = SprawdzanieOdpowiedzi.Mnozenie_odp(-3, 4);
            Assert.Equal("-12", wynik);
        }

        [Fact]
        public void Mnozenie_dodawanie_test()
        {
            
            string wynik = SprawdzanieOdpowiedzi.Dodawanie_mnozenie_odp(2, 3, 4, 3);
            Assert.Equal("10", wynik);
        }
    }
}