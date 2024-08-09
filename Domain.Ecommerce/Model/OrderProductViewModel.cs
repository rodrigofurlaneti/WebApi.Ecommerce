namespace Domain.Ecommerce.Model
{
    public class OrderProductViewModel
    {
        public List<OrderProductResponseDto>? Products { get; set; } = new List<OrderProductResponseDto>();
    }
}
