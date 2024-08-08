using System;
using Domain.Ecommerce.Enum;
using FluentAssertions;
using Xunit;

namespace Test.Ecommerce.Domain.Enum.ProductStatusTests
{
    public class ProductStatusTests
    {
        [Theory]
        [InlineData(1, ProductStatus.Available)]
        [InlineData(2, ProductStatus.NotAvailable)]
        public void Deve_RetornarNomeCorretoParaValoresValidos(int enumValue, ProductStatus expectedStatus)
        {
            // Act
            var status = (ProductStatus)enumValue;

            // Assert
            status.Should().Be(expectedStatus);
        }
    }
}
