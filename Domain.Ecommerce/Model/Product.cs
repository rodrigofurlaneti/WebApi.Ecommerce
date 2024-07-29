using Domain.Ecommerce.Enun;

namespace Domain.Ecommerce.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        public int Amount { get; set; }
        public string Image { get; set; }
        public double ValueOf { get; set; }
        public double ValueFor { get; set; }
        public double Discount { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime DateUpdate { get; set; }
        public ProductStatus ProductStatus { get; set; }
    }
}
