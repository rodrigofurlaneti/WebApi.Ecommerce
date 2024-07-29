namespace Domain.Ecommerce.Model
{
    public class User : Base
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string Complement {  get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string CellPhone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
