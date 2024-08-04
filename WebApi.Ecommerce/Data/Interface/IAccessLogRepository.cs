using Domain.Ecommerce.Model;

namespace WebApi.Ecommerce.Data.Interface
{
    public interface IAccessLogRepository
    {
        Task<IEnumerable<AccessLog>> GetAsync();
        Task PostAsync(AccessLog accessLog);
        Task PutAsync(AccessLog accessLog);
        Task<AccessLog?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
