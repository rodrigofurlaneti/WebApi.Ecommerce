using Domain.Ecommerce.Model;

namespace WebApi.Ecommerce.Data.Interface
{
    public interface IOrderProductRepository
    {
        Task<IEnumerable<OrderProduct>> GetAsync();
        Task PostAsync(OrderProduct contactUs);
        Task PutAsync(OrderProduct contactUs);
        Task<OrderProduct> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
