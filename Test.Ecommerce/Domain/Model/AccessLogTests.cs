using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.AccessLogTests
{
    public class AccessLogTests
    {
        [Fact]
        public void Propriedades_DevemTerOsTiposCorretos()
        {
            // Arrange
            var accessLog = new AccessLog();

            // Act & Assert
            Assert.IsType<int>(accessLog.Id);
            Assert.IsType<string?>(accessLog.Latitude);
            Assert.IsType<string?>(accessLog.Longitude);
            Assert.IsType<string?>(accessLog.InternetProtocol);
            Assert.IsType<string?>(accessLog.UserAgent);
            Assert.IsType<DateTime>(accessLog.DateInsert);
        }

        [Fact]
        public void Propriedades_DevemAceitarValoresNulos()
        {
            // Arrange
            var accessLog = new AccessLog
            {
                Latitude = null,
                Longitude = null,
                InternetProtocol = null,
                UserAgent = null
            };

            // Act & Assert
            Assert.Null(accessLog.Latitude);
            Assert.Null(accessLog.Longitude);
            Assert.Null(accessLog.InternetProtocol);
            Assert.Null(accessLog.UserAgent);
        }

        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var accessLog = new AccessLog();

            // Act & Assert
            Assert.Equal(string.Empty, accessLog.Latitude);
            Assert.Equal(string.Empty, accessLog.Longitude);
            Assert.Equal(string.Empty, accessLog.InternetProtocol);
            Assert.Equal(string.Empty, accessLog.UserAgent);
            Assert.True(accessLog.DateInsert <= DateTime.Now && accessLog.DateInsert > DateTime.Now.AddSeconds(-5), "DateInsert should be initialized to the current time.");
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao()
        {
            // Arrange
            var accessLog = new AccessLog
            {
                Latitude = "45.0",
                Longitude = "-75.0",
                InternetProtocol = "192.168.1.1",
                UserAgent = "Mozilla/5.0",
                DateInsert = new DateTime(2023, 1, 1)
            };

            // Act & Assert
            Assert.Equal("45.0", accessLog.Latitude);
            Assert.Equal("-75.0", accessLog.Longitude);
            Assert.Equal("192.168.1.1", accessLog.InternetProtocol);
            Assert.Equal("Mozilla/5.0", accessLog.UserAgent);
            Assert.Equal(new DateTime(2023, 1, 1), accessLog.DateInsert);
        }
    }
}
