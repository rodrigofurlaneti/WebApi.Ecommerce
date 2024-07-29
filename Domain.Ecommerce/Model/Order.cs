using Domain.Ecommerce.Enum;

namespace Domain.Ecommerce.Model
{
    public class Order
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public List<Product>? Products { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime DateUpdate { get; set; }
        public OrderStatus OrderStatus { get; set; }

    }
}
