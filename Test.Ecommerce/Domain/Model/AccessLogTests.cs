using AutoFixture;
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
            var fixture = new Fixture();
            var accessLog = fixture.Create<AccessLog>();

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
            var fixture = new Fixture();
            var accessLog = fixture.Build<AccessLog>()
                                   .With(al => al.Latitude, (string?)null)
                                   .With(al => al.Longitude, (string?)null)
                                   .With(al => al.InternetProtocol, (string?)null)
                                   .With(al => al.UserAgent, (string?)null)
                                   .Create();

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
            var fixture = new Fixture();
            var accessLogDinamico = fixture.Build<AccessLog>().Create();

            var accessLogManual = new AccessLog
            {
                Latitude = accessLogDinamico.Latitude,
                Longitude = accessLogDinamico.Longitude,
                InternetProtocol = accessLogDinamico.InternetProtocol,
                UserAgent = accessLogDinamico.UserAgent,
                DateInsert = accessLogDinamico.DateInsert
            };

            // Act & Assert
            Assert.Equal(accessLogDinamico.Latitude, accessLogManual.Latitude);
            Assert.Equal(accessLogDinamico.Longitude, accessLogManual.Longitude);
            Assert.Equal(accessLogDinamico.InternetProtocol, accessLogManual.InternetProtocol);
            Assert.Equal(accessLogDinamico.UserAgent, accessLogManual.UserAgent);
            Assert.Equal(accessLogDinamico.DateInsert, accessLogManual.DateInsert);
        }
    }
}
