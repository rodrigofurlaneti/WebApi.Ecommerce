using System.Text.Json.Serialization;

namespace Domain.Ecommerce.Model
{
    public class Address
    {
        [JsonPropertyName("house_number")]
        public string HouseNumber { get; set; }
        public string Road { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string Municipality { get; set; }
        public string County { get; set; }
        [JsonPropertyName("state_district")]
        public string StateDistrict { get; set; }
        public string State { get; set; }
        [JsonPropertyName("ISO3166-2-lvl4")]
        public string ISO3166_2_lvl4 { get; set; }
        public string Region { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
    }
}
