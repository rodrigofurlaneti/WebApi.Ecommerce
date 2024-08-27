namespace Domain.Ecommerce.Model
{
    public class AuthenticationResponse
    {
        public int Id { get; set; }
        public string? Username { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public int IdProfile { get; set; } = 0;
        public string? Name { get; set; } = string.Empty;
        public bool Status { get; set; } = false;
    }
}
