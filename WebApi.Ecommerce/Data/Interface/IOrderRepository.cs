using Domain.Ecommerce.Model;

namespace WebApi.Ecommerce.Data.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAsync();
        Task PostAsync(Order contactUs);
        Task PutAsync(Order contactUs);
        Task<Order> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
