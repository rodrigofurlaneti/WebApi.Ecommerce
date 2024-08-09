using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model
{
    public class OrderRequestTests
    {
        [Fact]
        public void Propriedades_DevemTerValoresPadraoCorretos()
        {
            // Arrange
            var orderRequest = new OrderRequest();

            // Act & Assert
            Assert.Equal(0, orderRequest.productId);
            Assert.Equal(0, orderRequest.userId);
            Assert.Equal(0, orderRequest.amount);
            Assert.Equal(0, orderRequest.orderId);
        }

        [Fact]
        public void Propriedades_DevemPermitirAlteracaoDeValoresPadrao()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                productId = 123,
                userId = 456,
                amount = 2,
                orderId = 789
            };

            // Act & Assert
            Assert.Equal(123, orderRequest.productId);
            Assert.Equal(456, orderRequest.userId);
            Assert.Equal(2, orderRequest.amount);
            Assert.Equal(789, orderRequest.orderId);
        }

        [Fact]
        public void Propriedades_DevemAceitarEManterValorNulo()
        {
            // Arrange
            var orderRequest = new OrderRequest
            {
                productId = null,
                userId = null,
                amount = null,
                orderId = null
            };

            // Act & Assert
            Assert.Null(orderRequest.productId);
            Assert.Null(orderRequest.userId);
            Assert.Null(orderRequest.amount);
            Assert.Null(orderRequest.orderId);
        }
    }
}
