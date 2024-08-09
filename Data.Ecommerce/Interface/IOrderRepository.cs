using Domain.Ecommerce.Model;

namespace Data.Ecommerce.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAsync();
        Task<int> PostAsync(Order order);
        Task PutAsync(Order order);
        Task<Order> GetByIdAsync(int id);
        Task<int> GetProductCountByOrderIdAsync(int orderId);
        Task DeleteAsync(int id);
    }
}
