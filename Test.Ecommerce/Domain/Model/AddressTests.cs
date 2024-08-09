using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model
{
    public class AddressTests
    {
        [Fact]
        public void Propriedades_DevemAceitarValoresNulos()
        {
            // Arrange
            var address = new Address
            {
                HouseNumber = null,
                Road = null,
                Suburb = null,
                City = null,
                Municipality = null,
                County = null,
                StateDistrict = null,
                State = null,
                ISO3166_2_lvl4 = null,
                Region = null,
                Postcode = null,
                Country = null,
                CountryCode = null
            };

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
            var address = new Address
            {
                HouseNumber = "123",
                Road = "Main St",
                Suburb = "Central",
                City = "Springfield",
                Municipality = "Springfield Municipality",
                County = "Clark",
                StateDistrict = "District 9",
                State = "IL",
                ISO3166_2_lvl4 = "US-IL",
                Region = "Midwest",
                Postcode = "62701",
                Country = "United States",
                CountryCode = "US"
            };

            // Act & Assert
            Assert.Equal("123", address.HouseNumber);
            Assert.Equal("Main St", address.Road);
            Assert.Equal("Central", address.Suburb);
            Assert.Equal("Springfield", address.City);
            Assert.Equal("Springfield Municipality", address.Municipality);
            Assert.Equal("Clark", address.County);
            Assert.Equal("District 9", address.StateDistrict);
            Assert.Equal("IL", address.State);
            Assert.Equal("US-IL", address.ISO3166_2_lvl4);
            Assert.Equal("Midwest", address.Region);
            Assert.Equal("62701", address.Postcode);
            Assert.Equal("United States", address.Country);
            Assert.Equal("US", address.CountryCode);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var address = new Address();

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
