namespace Domain.Ecommerce.Model
{
    public class AccessLog
    {
        public int Id { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string InternetProtocol { get; set; }
        public string UserAgent { get; set; }
        public DateTime DateInsert { get; set; }
    }
}
