using Domain.Ecommerce.Model;

namespace Data.Ecommerce.Interface
{
    public interface IContactUsRepository
    {
        Task<IEnumerable<ContactUs>> GetAsync();
        Task PostAsync(ContactUs contactUs);
        Task PutAsync(ContactUs contactUs);
        Task<ContactUs?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
