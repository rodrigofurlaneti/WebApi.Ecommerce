using AutoFixture;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.AuthenticationResponseTests
{
    public class AuthenticationResponseTests
    {
        [Fact]
        public void Propriedades_DevemAceitarValoresNulos()
        {
            // Arrange
            var fixture = new Fixture();
            var authResponse = fixture.Build<AuthenticationResponse>()
                                      .With(ar => ar.Username, (string?)null)
                                      .With(ar => ar.Password, (string?)null)
                                      .With(ar => ar.Name, (string?)null)
                                      .Create();

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
            var fixture = new Fixture();
            var authResponseDinamico = fixture.Build<AuthenticationResponse>()
                                              .With(ar => ar.Status, true) // Define o Status manualmente
                                              .Create();

            var authResponseManual = new AuthenticationResponse
            {
                Id = authResponseDinamico.Id,
                Username = authResponseDinamico.Username,
                Password = authResponseDinamico.Password,
                Name = authResponseDinamico.Name,
                Status = authResponseDinamico.Status
            };

            // Act & Assert
            Assert.Equal(authResponseDinamico.Id, authResponseManual.Id);
            Assert.Equal(authResponseDinamico.Username, authResponseManual.Username);
            Assert.Equal(authResponseDinamico.Password, authResponseManual.Password);
            Assert.Equal(authResponseDinamico.Name, authResponseManual.Name);
            Assert.Equal(authResponseDinamico.Status, authResponseManual.Status);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var fixture = new Fixture();
            var authResponse = fixture.Create<AuthenticationResponse>();

            // Act & Assert
            Assert.IsType<int>(authResponse.Id);
            Assert.IsType<string>(authResponse.Username);
            Assert.IsType<string>(authResponse.Password);
            Assert.IsType<string>(authResponse.Name);
            Assert.IsType<bool>(authResponse.Status);
        }
    }
}
