using Domain.Ecommerce.Model;

namespace WebApi.Ecommerce.Data.Interface
{
    public interface IOrderProductRepository
    {
        Task<IEnumerable<OrderProduct>> GetAsync();
        Task PostAsync(OrderProduct orderProduct);
        Task PutAsync(OrderProduct orderProduct);
        Task<OrderProduct?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
