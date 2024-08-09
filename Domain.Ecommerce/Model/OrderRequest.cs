namespace Domain.Ecommerce.Model
{
    public class OrderRequest
    {
        public int? productId { get; set; } = 0;
        public int? userId { get; set; } = 0;
        public int? amount { get; set; } = 0;
        public int? orderId { get; set; } = 0;
    }
}
