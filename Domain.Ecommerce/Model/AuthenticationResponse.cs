using Domain.Ecommerce.Enum;

namespace Domain.Ecommerce.Model
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }
    }
}
