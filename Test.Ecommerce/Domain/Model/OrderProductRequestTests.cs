using AutoFixture;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.OrderProductRequestTests
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
            var fixture = new Fixture(); // Cria uma instância do AutoFixture
            var orderProductRequest = fixture.Build<OrderProductRequest>()
                                             .With(opr => opr.IdOrder, 456)
                                             .Create();

            // Act & Assert
            Assert.Equal(456, orderProductRequest.IdOrder);
        }

        [Fact]
        public void Propriedades_DevemTerTiposCorretos()
        {
            // Arrange
            var fixture = new Fixture();
            var orderProductRequest = fixture.Create<OrderProductRequest>();

            // Act & Assert
            Assert.IsType<int>(orderProductRequest.IdOrder);
        }
    }
}
