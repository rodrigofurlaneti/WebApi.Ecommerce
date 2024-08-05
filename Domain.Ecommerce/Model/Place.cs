using System.Text.Json.Serialization;

namespace Domain.Ecommerce.Model
{
    public class Place
    {
        [JsonPropertyName("place_id")]
        public int PlaceId { get; set; }
        public string Licence { get; set; }
        [JsonPropertyName("osm_type")]
        public string OsmType { get; set; }
        [JsonPropertyName("osm_id")]
        public long OsmId { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; }
        public Address Address { get; set; }
    }
}
