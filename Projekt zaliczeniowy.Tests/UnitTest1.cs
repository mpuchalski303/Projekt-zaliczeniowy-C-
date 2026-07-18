using Xunit;
using Projekt_zaliczeniowy;

namespace Projekt_zaliczeniowy.Tests
{
    public class SprawdzanieOdpowiedziTests
    {
        [Fact]
        public void WyliczOdpowiedzUlamki_ZwracaPoprawnyString()
        {
            string wynik = SprawdzanieOdpowiedzi.WyliczOdpowiedzUlamki(3, 4, 5);
            Assert.Equal("12/5", wynik);
        }

        [Fact]
        public void WyliczOdpowiedzPierwiastki_ZwracaLiczbe4JakoString()
        {
            string wynik = SprawdzanieOdpowiedzi.WyliczOdpowiedzPierwiastki(7);
            Assert.Equal("7", wynik);
        }

        [Fact]
        public void WyliczOdpowiedzPotegi_ObliczaPoprawnyWynik()
        {
            string wynik = SprawdzanieOdpowiedzi.WyliczOdpowiedzPotegi(2, 3);
            Assert.Equal("8", wynik);
        }
    }
}