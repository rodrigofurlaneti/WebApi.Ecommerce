using Domain.Ecommerce.Model;

namespace WebApi.Ecommerce.Data.Interface
{
    public interface IContactUsRepository
    {
        Task<IEnumerable<ContactUs>> GetAsync();
        Task PostAsync(ContactUs contactUs);
        Task PutAsync(ContactUs contactUs);
        Task<ContactUs> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
