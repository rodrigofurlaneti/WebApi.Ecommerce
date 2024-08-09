namespace Domain.Ecommerce.Model
{
    public class OrderProduct : Base
    {
        public int Id { get; set; }
        public int IdOrder { get; set; }
        public int IdProduct { get; set; }
    }
}
