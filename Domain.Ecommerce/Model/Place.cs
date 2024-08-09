using System.Text.Json.Serialization;

namespace Domain.Ecommerce.Model
{
    public class Place
    {
        [JsonPropertyName("place_id")]
        public int? PlaceId { get; set; } = 0;
        public string? Licence { get; set; } = string.Empty;
        [JsonPropertyName("osm_type")]
        public string? OsmType { get; set; } = string.Empty;
        [JsonPropertyName("osm_id")]
        public long? OsmId { get; set; } = 0;
        public string? Lat { get; set; } = string.Empty;
        public string? Lon { get; set; } = string.Empty;
        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; } = string.Empty;
        public Address? Address { get; set; } = new Address();
    }
}
