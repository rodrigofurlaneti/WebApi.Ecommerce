namespace WebApi.Ecommerce.Model
{
    public class ContactUs : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string Message { get; set; }
    }
}
