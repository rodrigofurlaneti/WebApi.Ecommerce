using Domain.Ecommerce.Enum;
using System.Text.Json.Serialization;

namespace Domain.Ecommerce.Model
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; } = 0;

        [JsonPropertyName("name")]
        public string? Name { get; set; } = string.Empty;

        [JsonPropertyName("amount")]
        public int? Amount { get; set; } = 0;

        [JsonPropertyName("details")]
        public string? Details { get; set; } = string.Empty;

        [JsonPropertyName("picture")]
        public string? Picture { get; set; } = string.Empty;

        [JsonPropertyName("valueOf")]
        [JsonConverter(typeof(Domain.Ecommerce.Converters.DecimalConverter))]
        public decimal? ValueOf { get; set; } = 0;

        [JsonPropertyName("valueFor")]
        [JsonConverter(typeof(Domain.Ecommerce.Converters.DecimalConverter))]
        public decimal? ValueFor { get; set; } = 0;

        [JsonPropertyName("discount")]
        [JsonConverter(typeof(Domain.Ecommerce.Converters.DecimalConverter))]
        public decimal? Discount { get; set; } = 0;

        [JsonPropertyName("dateInsert")]
        [JsonConverter(typeof(Domain.Ecommerce.Converters.DateTimeConverter))]
        public DateTime DateInsert { get; set; }

        [JsonPropertyName("dateUpdate")]
        [JsonConverter(typeof(Domain.Ecommerce.Converters.DateTimeConverter))]
        public DateTime DateUpdate { get; set; }

        [JsonPropertyName("productStatus")]
        [JsonConverter(typeof(Domain.Ecommerce.Converters.ProductStatusConverter))]
        public ProductStatus ProductStatus { get; set; }

    }
}
