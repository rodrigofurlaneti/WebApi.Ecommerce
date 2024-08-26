using Domain.Ecommerce.Model;

namespace Data.Ecommerce.Interface
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> GetAsync();
        Task<int> PostAsync(Profile order);
        Task PutAsync(Profile order);
        Task<Profile> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
