using System;
using AutoFixture;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.PlaceTests
{
    public class PlaceTests
    {
        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var place = new Place();

            // Act & Assert
            Assert.Equal(0, place.PlaceId);
            Assert.Equal(string.Empty, place.Licence);
            Assert.Equal(string.Empty, place.OsmType);
            Assert.Equal(0, place.OsmId);
            Assert.Equal(string.Empty, place.Lat);
            Assert.Equal(string.Empty, place.Lon);
            Assert.Equal(string.Empty, place.DisplayName);
            Assert.NotNull(place.Address);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao()
        {
            // Arrange
            var fixture = new Fixture(); // Cria uma instância do AutoFixture
            var address = fixture.Create<Address>();
            var place = fixture.Build<Place>()
                               .With(p => p.Address, address)
                               .Create();

            // Act & Assert
            Assert.NotEqual(0, place.PlaceId);
            Assert.NotEqual(string.Empty, place.Licence);
            Assert.NotEqual(string.Empty, place.OsmType);
            Assert.NotEqual(0, place.OsmId);
            Assert.NotEqual(string.Empty, place.Lat);
            Assert.NotEqual(string.Empty, place.Lon);
            Assert.NotEqual(string.Empty, place.DisplayName);
            Assert.Equal(address, place.Address);
        }

        [Fact]
        public void Propriedades_DevemAceitarEManterValorNulo()
        {
            // Arrange
            var fixture = new Fixture();
            var place = fixture.Build<Place>()
                               .With(p => p.PlaceId, (int?)null)
                               .With(p => p.Licence, (string?)null)
                               .With(p => p.OsmType, (string?)null)
                               .With(p => p.OsmId, (long?)null)
                               .With(p => p.Lat, (string?)null)
                               .With(p => p.Lon, (string?)null)
                               .With(p => p.DisplayName, (string?)null)
                               .With(p => p.Address, (Address?)null)
                               .Create();

            // Act & Assert
            Assert.Null(place.PlaceId);
            Assert.Null(place.Licence);
            Assert.Null(place.OsmType);
            Assert.Null(place.OsmId);
            Assert.Null(place.Lat);
            Assert.Null(place.Lon);
            Assert.Null(place.DisplayName);
            Assert.Null(place.Address);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var fixture = new Fixture();
            var place = fixture.Create<Place>();

            // Act & Assert
            Assert.IsType<int>(place.PlaceId);
            Assert.IsType<string>(place.Licence);
            Assert.IsType<string>(place.OsmType);
            Assert.IsType<long>(place.OsmId);
            Assert.IsType<string>(place.Lat);
            Assert.IsType<string>(place.Lon);
            Assert.IsType<string>(place.DisplayName);
            Assert.IsType<Address>(place.Address);
        }
    }
}
