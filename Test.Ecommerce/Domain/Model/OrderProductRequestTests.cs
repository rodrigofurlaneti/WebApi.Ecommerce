using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model
{
    public class OrderProductRequestTests
    {
        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var orderProductRequest = new OrderProductRequest();

            // Act & Assert
            Assert.Equal(0, orderProductRequest.IdOrder);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao()
        {
            // Arrange
            var orderProductRequest = new OrderProductRequest
            {
                IdOrder = 456
            };

            // Act & Assert
            Assert.Equal(456, orderProductRequest.IdOrder);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var orderProductRequest = new OrderProductRequest();

            // Act & Assert
            Assert.IsType<int>(orderProductRequest.IdOrder);
        }
    }
}
