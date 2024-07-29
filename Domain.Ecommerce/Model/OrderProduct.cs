using Domain.Ecommerce.Enum;

namespace Domain.Ecommerce.Model
{
    public class OrderProduct
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime DateUpdate { get; set; }
        public Status Status { get; set; }
    }
}
