using Domain.Ecommerce.Model;

namespace Data.Ecommerce.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAsync();
        Task PostAsync(Product product);
        Task PutAsync(Product product);
        Task<Product?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
