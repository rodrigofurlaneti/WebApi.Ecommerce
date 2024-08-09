namespace Domain.Ecommerce.Model
{
    public class OrderProductViewModel
    {
        public List<OrderProductResponseDTO>? Products { get; set; } = new List<OrderProductResponseDTO>();
    }
}
