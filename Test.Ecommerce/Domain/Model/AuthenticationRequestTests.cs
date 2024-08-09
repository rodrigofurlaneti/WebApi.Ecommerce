using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model
{
    public class AuthenticationRequestTests
    {
        [Fact]
        public void Propriedades_DevemAceitarValoresNulos()
        {
            // Arrange
            var authRequest = new AuthenticationRequest
            {
                Username = null,
                Password = null
            };

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
            var authRequest = new AuthenticationRequest
            {
                Username = "user123",
                Password = "pass123"
            };

            // Act & Assert
            Assert.Equal("user123", authRequest.Username);
            Assert.Equal("pass123", authRequest.Password);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var authRequest = new AuthenticationRequest();

            // Act & Assert
            Assert.IsType<string>(authRequest.Username);
            Assert.IsType<string>(authRequest.Password);
        }
    }
}
