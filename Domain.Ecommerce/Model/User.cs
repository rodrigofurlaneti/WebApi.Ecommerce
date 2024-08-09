namespace Domain.Ecommerce.Model
{
    public class User : Base
    {
        public int? Id { get; set; } = 0; 
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string? Number { get; set; } = string.Empty;
        public string? Complement {  get; set; } = string.Empty;
        public string? Neighborhood { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? ZipCode { get; set; } = string.Empty;
        public string? CellPhone { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
    }
}
