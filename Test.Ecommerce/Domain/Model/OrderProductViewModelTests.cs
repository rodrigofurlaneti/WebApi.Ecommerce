using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model
{
    public class OrderProductViewModelTests
    {
        [Fact]
        public void Propriedade_Products_DeveTerValorPadraoCorreto()
        {
            // Arrange
            var orderProductViewModel = new OrderProductViewModel();

            // Act & Assert
            Assert.NotNull(orderProductViewModel.Products);
            Assert.Empty(orderProductViewModel.Products);
        }

        [Fact]
        public void Propriedade_Products_DevePermitirAlteracaoDeValorPadrao()
        {
            // Arrange
            var orderProductViewModel = new OrderProductViewModel();
            var productsList = new List<OrderProductResponseDTO>
            {
                new OrderProductResponseDTO { Id = 1, Name = "Product 1" },
                new OrderProductResponseDTO { Id = 2, Name = "Product 2" }
            };

            // Act
            orderProductViewModel.Products = productsList;

            // Assert
            Assert.Equal(2, orderProductViewModel.Products.Count);
            Assert.Equal(1, orderProductViewModel.Products[0].Id);
            Assert.Equal("Product 1", orderProductViewModel.Products[0].Name);
            Assert.Equal(2, orderProductViewModel.Products[1].Id);
            Assert.Equal("Product 2", orderProductViewModel.Products[1].Name);
        }

        [Fact]
        public void Propriedade_Products_DeveAceitarEManterValorNulo()
        {
            // Arrange
            var orderProductViewModel = new OrderProductViewModel();

            // Act
            orderProductViewModel.Products = null;

            // Assert
            Assert.Null(orderProductViewModel.Products);
        }

        [Fact]
        public void Propriedade_Products_DeveTerTipoCorreto()
        {
            // Arrange
            var orderProductViewModel = new OrderProductViewModel();

            // Act & Assert
            Assert.IsType<List<OrderProductResponseDTO>>(orderProductViewModel.Products);
        }
    }
}
