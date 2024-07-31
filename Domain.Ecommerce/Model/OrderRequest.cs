namespace Domain.Ecommerce.Model
{
    public class OrderRequest
    {
        public int productId { get; set; }
        public int userId { get; set; }
        public int amount { get; set; }
        public int orderId { get; set; }
    }
}
