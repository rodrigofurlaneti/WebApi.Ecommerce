namespace Domain.Ecommerce.Model
{
    public class OrderProductResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Details { get; set; }
        public string Picture { get; set; }
        public decimal ValueFor { get; set; }
    }
}
