namespace Domain.Ecommerce.Model
{
    public class OrderProductResponseDto
    {
        public int? Id { get; set; } = 0;
        public string? Name { get; set; } = string.Empty;
        public int? Amount { get; set; } = 0;
        public string? Details { get; set; } = string.Empty;
        public string? Picture { get; set; } = string.Empty;
        public decimal? ValueFor { get; set; } = 0;
    }
}
