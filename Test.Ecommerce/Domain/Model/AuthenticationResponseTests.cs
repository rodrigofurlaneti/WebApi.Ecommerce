using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model
{
    public class AuthenticationResponseTests
    {
        [Fact]
        public void Propriedades_DevemAceitarValoresNulos()
        {
            // Arrange
            var authResponse = new AuthenticationResponse
            {
                Username = null,
                Password = null,
                Name = null
            };

            // Act & Assert
            Assert.Null(authResponse.Username);
            Assert.Null(authResponse.Password);
            Assert.Null(authResponse.Name);
        }

        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var authResponse = new AuthenticationResponse();

            // Act & Assert
            Assert.Equal(string.Empty, authResponse.Username);
            Assert.Equal(string.Empty, authResponse.Password);
            Assert.Equal(string.Empty, authResponse.Name);
            Assert.False(authResponse.Status);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao()
        {
            // Arrange
            var authResponse = new AuthenticationResponse
            {
                Id = 123,
                Username = "user123",
                Password = "pass123",
                Name = "John Doe",
                Status = true
            };

            // Act & Assert
            Assert.Equal(123, authResponse.Id);
            Assert.Equal("user123", authResponse.Username);
            Assert.Equal("pass123", authResponse.Password);
            Assert.Equal("John Doe", authResponse.Name);
            Assert.True(authResponse.Status);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var authResponse = new AuthenticationResponse();

            // Act & Assert
            Assert.IsType<int>(authResponse.Id);
            Assert.IsType<string>(authResponse.Username);
            Assert.IsType<string>(authResponse.Password);
            Assert.IsType<string>(authResponse.Name);
            Assert.IsType<bool>(authResponse.Status);
        }
    }
}
