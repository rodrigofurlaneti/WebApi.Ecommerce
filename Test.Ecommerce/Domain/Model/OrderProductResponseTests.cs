using AutoFixture;
using Domain.Ecommerce.Model;
using Xunit;
using System.Collections.Generic;

namespace Test.Ecommerce.Domain.Model.OrderProductResponseTests
{
    public class OrderProductResponseTests
    {
        [Fact]
        public void Propriedade_Product_DeveTerValorPadraoCorreto()
        {
            // Arrange
            var orderProductResponse = new OrderProductResponse();

            // Act & Assert
            Assert.NotNull(orderProductResponse.Product);
            Assert.Empty(orderProductResponse.Product);
        }

        [Fact]
        public void Propriedade_Product_DevePermitirAlteracaoDeValorPadrao_GerandoValoresDinamicos()
        {
            // Arrange
            var fixture = new Fixture();
            var productListDinamico = fixture.Create<List<Product>>();

            var orderProductResponse = new OrderProductResponse
            {
                Product = productListDinamico
            };

            // Act & Assert
            Assert.Equal(productListDinamico, orderProductResponse.Product);
            Assert.Equal(productListDinamico.Count, orderProductResponse.Product.Count);
        }

        [Fact]
        public void Propriedade_Product_DeveAceitarEManterValorNulo()
        {
            // Arrange
            var orderProductResponse = new OrderProductResponse
            {
                Product = null
            };

            // Act & Assert
            Assert.Null(orderProductResponse.Product);
        }

        [Fact]
        public void Propriedade_Product_DeveTerTipoCorreto()
        {
            // Arrange
            var fixture = new Fixture();
            var orderProductResponse = fixture.Create<OrderProductResponse>();

            // Act & Assert
            Assert.IsType<List<Product>>(orderProductResponse.Product);
        }
    }
}
