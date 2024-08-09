using AutoFixture;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.AddressTests
{
    public class AddressTests
    {
        [Fact]
        public void Propriedades_DevemAceitarValoresNulos()
        {
            // Arrange
            var fixture = new Fixture();
            var address = fixture.Build<Address>()
                                 .With(a => a.HouseNumber, (string?)null)
                                 .With(a => a.Road, (string?)null)
                                 .With(a => a.Suburb, (string?)null)
                                 .With(a => a.City, (string?)null)
                                 .With(a => a.Municipality, (string?)null)
                                 .With(a => a.County, (string?)null)
                                 .With(a => a.StateDistrict, (string?)null)
                                 .With(a => a.State, (string?)null)
                                 .With(a => a.ISO3166_2_lvl4, (string?)null)
                                 .With(a => a.Region, (string?)null)
                                 .With(a => a.Postcode, (string?)null)
                                 .With(a => a.Country, (string?)null)
                                 .With(a => a.CountryCode, (string?)null)
                                 .Create();

            // Act & Assert
            Assert.Null(address.HouseNumber);
            Assert.Null(address.Road);
            Assert.Null(address.Suburb);
            Assert.Null(address.City);
            Assert.Null(address.Municipality);
            Assert.Null(address.County);
            Assert.Null(address.StateDistrict);
            Assert.Null(address.State);
            Assert.Null(address.ISO3166_2_lvl4);
            Assert.Null(address.Region);
            Assert.Null(address.Postcode);
            Assert.Null(address.Country);
            Assert.Null(address.CountryCode);
        }

        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var address = new Address();

            // Act & Assert
            Assert.Equal(string.Empty, address.HouseNumber);
            Assert.Equal(string.Empty, address.Road);
            Assert.Equal(string.Empty, address.Suburb);
            Assert.Equal(string.Empty, address.City);
            Assert.Equal(string.Empty, address.Municipality);
            Assert.Equal(string.Empty, address.County);
            Assert.Equal(string.Empty, address.StateDistrict);
            Assert.Equal(string.Empty, address.State);
            Assert.Equal(string.Empty, address.ISO3166_2_lvl4);
            Assert.Equal(string.Empty, address.Region);
            Assert.Equal(string.Empty, address.Postcode);
            Assert.Equal(string.Empty, address.Country);
            Assert.Equal(string.Empty, address.CountryCode);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao()
        {
            // Arrange
            var fixture = new Fixture();
            var addressDinamico = fixture.Build<Address>().Create();

            var addressManual = new Address
            {
                HouseNumber = addressDinamico.HouseNumber,
                Road = addressDinamico.Road,
                Suburb = addressDinamico.Suburb,
                City = addressDinamico.City,
                Municipality = addressDinamico.Municipality,
                County = addressDinamico.County,
                StateDistrict = addressDinamico.StateDistrict,
                State = addressDinamico.State,
                ISO3166_2_lvl4 = addressDinamico.ISO3166_2_lvl4,
                Region = addressDinamico.Region,
                Postcode = addressDinamico.Postcode,
                Country = addressDinamico.Country,
                CountryCode = addressDinamico.CountryCode
            };

            // Act & Assert
            Assert.Equal(addressDinamico.HouseNumber, addressManual.HouseNumber);
            Assert.Equal(addressDinamico.Road, addressManual.Road);
            Assert.Equal(addressDinamico.Suburb, addressManual.Suburb);
            Assert.Equal(addressDinamico.City, addressManual.City);
            Assert.Equal(addressDinamico.Municipality, addressManual.Municipality);
            Assert.Equal(addressDinamico.County, addressManual.County);
            Assert.Equal(addressDinamico.StateDistrict, addressManual.StateDistrict);
            Assert.Equal(addressDinamico.State, addressManual.State);
            Assert.Equal(addressDinamico.ISO3166_2_lvl4, addressManual.ISO3166_2_lvl4);
            Assert.Equal(addressDinamico.Region, addressManual.Region);
            Assert.Equal(addressDinamico.Postcode, addressManual.Postcode);
            Assert.Equal(addressDinamico.Country, addressManual.Country);
            Assert.Equal(addressDinamico.CountryCode, addressManual.CountryCode);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var fixture = new Fixture();
            var address = fixture.Create<Address>();

            // Act & Assert
            Assert.IsType<string>(address.HouseNumber);
            Assert.IsType<string>(address.Road);
            Assert.IsType<string>(address.Suburb);
            Assert.IsType<string>(address.City);
            Assert.IsType<string>(address.Municipality);
            Assert.IsType<string>(address.County);
            Assert.IsType<string>(address.StateDistrict);
            Assert.IsType<string>(address.State);
            Assert.IsType<string>(address.ISO3166_2_lvl4);
            Assert.IsType<string>(address.Region);
            Assert.IsType<string>(address.Postcode);
            Assert.IsType<string>(address.Country);
            Assert.IsType<string>(address.CountryCode);
        }
    }
}
