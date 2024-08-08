using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Domain.Ecommerce.Converters;
using FluentAssertions;
using Xunit;

namespace Test.Ecommerce.Domain.Converters.DateTimeConverterTests
{
    public class DateTimeConverterTests
    {
        private readonly JsonSerializerOptions _options;

        public DateTimeConverterTests()
        {
            _options = new JsonSerializerOptions
            {
                Converters =
                {
                    new DateTimeConverter() // Especificando o namespace completo
                }
            };
        }

        [Fact]
        public void Deve_serializar_data_e_hora_com_formato_correto()
        {
            // Arrange
            var dateTime = new DateTime(2024, 8, 7, 14, 30, 45, 123, DateTimeKind.Utc);
            var expectedJson = "\"2024-08-07T14:30:45.123Z\"";

            // Act
            var json = JsonSerializer.Serialize(dateTime, _options);

            // Assert
            json.Should().Be(expectedJson);
        }

        [Fact]
        public void Deve_desserializar_data_e_hora_com_formato_correto()
        {
            // Arrange
            var json = "\"2024-08-07T14:30:45.123Z\"";
            var expectedDateTime = new DateTime(2024, 8, 7, 14, 30, 45, 123, DateTimeKind.Utc);

            // Act
            var dateTime = JsonSerializer.Deserialize<DateTime>(json, _options);

            // Assert
            dateTime.Should().Be(expectedDateTime);
        }

        [Fact]
        public void Deve_desserializar_data_e_hora_com_formato_inválido_lançar_exceção_Json()
        {
            // Arrange
            var json = "\"invalid date format\"";
            Action act = () => JsonSerializer.Deserialize<DateTime>(json, _options);

            // Act & Assert
            act.Should().Throw<JsonException>();
        }
    }
}
