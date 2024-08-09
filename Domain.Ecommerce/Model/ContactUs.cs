namespace Domain.Ecommerce.Model
{
    public class ContactUs : Base
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? CellPhone { get; set; } = string.Empty;
        public string? Message { get; set; } = string.Empty;
    }
}
