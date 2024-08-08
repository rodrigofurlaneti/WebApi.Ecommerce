using Domain.Ecommerce.Converters;
using FluentAssertions;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Test.Ecommerce.Domain.Converters.DecimalConverterTests
{
    public class DecimalConverterTests
    {
        private readonly JsonSerializerOptions _options;

        public DecimalConverterTests()
        {
            _options = new JsonSerializerOptions
            {
                Converters =
                {
                    new DecimalConverter()
                }
            };
        }

        [Fact]
        public void Deve_serializar_decimal_com_formato_correto()
        {
            // Arrange
            var decimalValue = 12345.6789m;
            var expectedJson = "\"12345.6789\"";

            // Act
            var json = JsonSerializer.Serialize(decimalValue, _options);

            // Assert
            json.Should().Be(expectedJson);
        }

        [Fact]
        public void Deve_desserializar_decimal_com_formato_correto()
        {
            // Arrange
            var json = "\"12345.6789\"";
            var expectedDecimalValue = 12345.6789m;

            // Act
            var decimalValue = JsonSerializer.Deserialize<decimal>(json, _options);

            // Assert
            decimalValue.Should().Be(expectedDecimalValue);
        }

        [Fact]
        public void Deve_desserializar_decimal_com_formato_inválido_lançar_exceção_Json()
        {
            // Arrange
            var json = "\"invalid decimal format\"";
            Action act = () => JsonSerializer.Deserialize<decimal>(json, _options);

            // Act & Assert
            act.Should().Throw<JsonException>();
        }
    }
}