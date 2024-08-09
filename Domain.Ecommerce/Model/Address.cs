using System.Text.Json.Serialization;

namespace Domain.Ecommerce.Model
{
    public class Address
    {
        [JsonPropertyName("house_number")]
        public string? HouseNumber { get; set; } = string.Empty;
        public string? Road { get; set; } = string.Empty;
        public string? Suburb { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Municipality { get; set; } = string.Empty;
        public string? County { get; set; } = string.Empty;
        [JsonPropertyName("state_district")]
        public string? StateDistrict { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        [JsonPropertyName("ISO3166-2-lvl4")]
        public string? ISO3166_2_lvl4 { get; set; } = string.Empty;
        public string? Region { get; set; } = string.Empty;
        public string? Postcode { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; } = string.Empty;
    }
}
