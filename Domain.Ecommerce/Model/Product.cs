using Domain.Ecommerce.Converters;
using Domain.Ecommerce.Enum;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Domain.Ecommerce.Model
{
    public class Product
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("amount")]
        public int Amount { get; set; }

        [JsonPropertyName("details")]
        public string? Details { get; set; }

        [JsonPropertyName("picture")]
        public string? Picture { get; set; }

        [JsonPropertyName("valueOf")]
        [JsonConverter(typeof(Domain.Ecommerce.Converters.DecimalConverter))]
        public decimal ValueOf { get; set; }

        [JsonPropertyName("valueFor")]
        [JsonConverter(typeof(Domain.Ecommerce.Converters.DecimalConverter))]
        public decimal ValueFor { get; set; }

        [JsonPropertyName("discount")]
        [JsonConverter(typeof(Domain.Ecommerce.Converters.DecimalConverter))]
        public decimal Discount { get; set; }

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
