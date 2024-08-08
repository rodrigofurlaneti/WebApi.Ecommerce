using Domain.Ecommerce.Enum;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Test.Ecommerce.Domain.Enum.StatusTests
{
    public class StatusTests
    {
        [Theory]
        [InlineData(0, Status.Disabled)]
        [InlineData(1, Status.Active)]
        public void Deve_RetornarNomeCorretoParaValoresValidos(int enumValue, Status expectedStatus)
        {
            // Act
            var status = (Status)enumValue;

            // Assert
            status.Should().Be(expectedStatus);
        }
    }
}