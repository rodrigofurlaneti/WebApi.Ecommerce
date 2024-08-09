namespace Domain.Ecommerce.Model
{
    public class AccessLog
    {
        public int Id { get; set; }
        public string? Latitude { get; set; } = string.Empty;
        public string? Longitude { get; set; } = string.Empty;
        public string? InternetProtocol { get; set; } = string.Empty;
        public string? UserAgent { get; set; } = string.Empty;
        public DateTime DateInsert { get; set; } = DateTime.Now;
    }
}
