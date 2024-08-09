using AutoFixture;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.OrderRequestTests
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
            var fixture = new Fixture(); // Cria uma instância do AutoFixture
            var orderRequest = fixture.Build<OrderRequest>()
                                      .With(o => o.productId, 123)
                                      .With(o => o.userId, 456)
                                      .With(o => o.amount, 2)
                                      .With(o => o.orderId, 789)
                                      .Create();

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
            var fixture = new Fixture();
            var orderRequest = fixture.Build<OrderRequest>()
                                      .With(o => o.productId, (int?)null)
                                      .With(o => o.userId, (int?)null)
                                      .With(o => o.amount, (int?)null)
                                      .With(o => o.orderId, (int?)null)
                                      .Create();

            // Act & Assert
            Assert.Null(orderRequest.productId);
            Assert.Null(orderRequest.userId);
            Assert.Null(orderRequest.amount);
            Assert.Null(orderRequest.orderId);
        }
    }
}
