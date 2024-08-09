using System.Collections.Generic;
using AutoFixture;
using Domain.Ecommerce.Model;
using Xunit;

namespace Test.Ecommerce.Domain.Model.OrderProductViewModelTests
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
            var fixture = new Fixture(); // Cria uma instância do AutoFixture
            var productsList = fixture.CreateMany<OrderProductResponseDto>(2).ToList();

            var orderProductViewModel = new OrderProductViewModel
            {
                Products = productsList
            };

            // Act & Assert
            Assert.Equal(2, orderProductViewModel.Products.Count);
            Assert.Equal(productsList[0].Id, orderProductViewModel.Products[0].Id);
            Assert.Equal(productsList[0].Name, orderProductViewModel.Products[0].Name);
            Assert.Equal(productsList[1].Id, orderProductViewModel.Products[1].Id);
            Assert.Equal(productsList[1].Name, orderProductViewModel.Products[1].Name);
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
            Assert.IsType<List<OrderProductResponseDto>>(orderProductViewModel.Products);
        }
    }
}
