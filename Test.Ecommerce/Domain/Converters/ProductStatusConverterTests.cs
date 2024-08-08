using System;
using System.Text.Json;
using Domain.Ecommerce.Converters;
using Domain.Ecommerce.Enum;
using FluentAssertions;
using Xunit;

namespace Test.Ecommerce.Domain.Converters.ProductStatusConverterTests
{
    public class ProductStatusConverterTests
    {
        private readonly JsonSerializerOptions _options;

        public ProductStatusConverterTests()
        {
            _options = new JsonSerializerOptions
            {
                Converters =
                {
                    new ProductStatusConverter()
                }
            };
        }

        [Fact]
        public void Deve_SerializarStatusDoProduto_ComFormatoCorreto()
        {
            // Arrange
            var status = ProductStatus.Available;
            var expectedJson = "\"Available\"";

            // Act
            var json = JsonSerializer.Serialize(status, _options);

            // Assert
            json.Should().Be(expectedJson);
        }

        [Fact]
        public void Deve_DesserializarStatusDoProduto_ComString()
        {
            // Arrange
            var json = "\"Available\"";
            var expectedStatus = ProductStatus.Available;

            // Act
            var status = JsonSerializer.Deserialize<ProductStatus>(json, _options);

            // Assert
            status.Should().Be(expectedStatus);
        }

        [Fact]
        public void Deve_DesserializarStatusDoProduto_ComNumero()
        {
            // Arrange
            var json = "1"; // Assuming 1 corresponds to ProductStatus.Available
            var expectedStatus = ProductStatus.Available;

            // Act
            var status = JsonSerializer.Deserialize<ProductStatus>(json, _options);

            // Assert
            status.Should().Be(expectedStatus);
        }
    }
}
