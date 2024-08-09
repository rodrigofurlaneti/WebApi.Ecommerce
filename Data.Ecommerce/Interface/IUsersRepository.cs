using Domain.Ecommerce.Model;

namespace Data.Ecommerce.Interface
{
    public interface IUsersRepository
    {
        Task<IEnumerable<User>> GetAsync();
        Task PostAsync(User users);
        Task PutAsync(User users);
        Task<User> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
