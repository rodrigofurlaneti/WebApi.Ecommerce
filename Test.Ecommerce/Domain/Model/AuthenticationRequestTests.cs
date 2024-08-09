using AutoFixture;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.AuthenticationRequestTests
{
    public class AuthenticationRequestTests
    {
        [Fact]
        public void Propriedades_DevemAceitarValoresNulos()
        {
            // Arrange
            var fixture = new Fixture();
            var authRequest = fixture.Build<AuthenticationRequest>()
                                     .With(ar => ar.Username, (string?)null)
                                     .With(ar => ar.Password, (string?)null)
                                     .Create();

            // Act & Assert
            Assert.Null(authRequest.Username);
            Assert.Null(authRequest.Password);
        }

        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var authRequest = new AuthenticationRequest();

            // Act & Assert
            Assert.Equal(string.Empty, authRequest.Username);
            Assert.Equal(string.Empty, authRequest.Password);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao()
        {
            // Arrange
            var fixture = new Fixture();
            var authRequestDinamico = fixture.Build<AuthenticationRequest>()
                                             .Create();

            var authRequestManual = new AuthenticationRequest
            {
                Username = authRequestDinamico.Username,
                Password = authRequestDinamico.Password
            };

            // Act & Assert
            Assert.Equal(authRequestDinamico.Username, authRequestManual.Username);
            Assert.Equal(authRequestDinamico.Password, authRequestManual.Password);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var fixture = new Fixture();
            var authRequest = fixture.Create<AuthenticationRequest>();

            // Act & Assert
            Assert.IsType<string>(authRequest.Username);
            Assert.IsType<string>(authRequest.Password);
        }
    }
}
