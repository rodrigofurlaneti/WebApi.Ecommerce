using System;
using System.Text.Json;
using Domain.Ecommerce.Converters;
using Domain.Ecommerce.Enum;
using FluentAssertions;
using Xunit;

namespace Test.Ecommerce.Domain.Converters
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

        //[Fact]
        //public void Deve_DesserializarStatusDoProduto_ComStringInvalida_LancarJsonException()
        //{
        //    // Arrange
        //    var json = "\"NotAvailable\"";
        //    Action act = () => JsonSerializer.Deserialize<ProductStatus>(json, _options);

        //    // Act & Assert
        //    act.Should().Throw<JsonException>().WithMessage("Unable to convert \"NotAvailable\" to Domain.Ecommerce.Enum.ProductStatus");
        //}

        //[Fact]
        //public void Deve_DesserializarStatusDoProduto_ComNumeroInvalido_LancarJsonException()
        //{
        //    // Arrange
        //    var json = "999"; // Assuming 999 is not a valid ProductStatus value
        //    Action act = () => JsonSerializer.Deserialize<ProductStatus>(json, _options);

        //    // Act & Assert
        //    act.Should().Throw<JsonException>().WithMessage("Unable to convert \"999\" to Domain.Ecommerce.Enum.ProductStatus");
        //}
    }
}
