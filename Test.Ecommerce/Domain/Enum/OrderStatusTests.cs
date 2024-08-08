using System;
using Domain.Ecommerce.Enum;
using FluentAssertions;
using Xunit;

namespace Test.Ecommerce.Domain.Enum.OrderStatusTests
{
    public class OrderStatusTests
    {
        [Theory]
        [InlineData(1, OrderStatus.Request)]
        [InlineData(2, OrderStatus.Payment)]
        [InlineData(3, OrderStatus.Approved)]
        [InlineData(4, OrderStatus.Delivery)]
        [InlineData(5, OrderStatus.Done)]
        public void Deve_RetornarNomeCorretoParaValoresValidos(int enumValue, OrderStatus expectedStatus)
        {
            // Act
            var status = (OrderStatus)enumValue;

            // Assert
            status.Should().Be(expectedStatus);
        }
    }
}
