using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Ecommerce.Enum;

namespace Domain.Ecommerce.Converters
{
    public class ProductStatusConverter : JsonConverter<ProductStatus>
    {
        public override ProductStatus Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var enumString = reader.GetString();
                if (Enum.ProductStatus.TryParse(enumString, true, out ProductStatus status))
                {
                    return status;
                }
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                var enumValue = reader.GetInt32();
                if (Enum.ProductStatus.IsDefined(typeof(ProductStatus), enumValue))
                {
                    return (ProductStatus)enumValue;
                }
            }
            throw new JsonException($"Unable to convert {reader.GetString()} to {typeof(ProductStatus)}");
        }

        public override void Write(Utf8JsonWriter writer, ProductStatus value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
