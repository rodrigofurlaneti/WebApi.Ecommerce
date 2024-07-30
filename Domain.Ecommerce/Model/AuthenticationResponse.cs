using Domain.Ecommerce.Enum;

namespace Domain.Ecommerce.Model
{
    public class AuthenticationResponse
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public bool Status { get; set; }
    }
}
