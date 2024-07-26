using WebApi.Ecommerce.Model;

namespace WebApi.Ecommerce.Data.Interface
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetUser();
        Task<IEnumerable<User>> GetUserAsync();
        void PostUser(User users);
        Task PostUserAsync(User users);
        void PutUser(User users);
        Task PutUserAsync(User users);
        User GetById(int id);
        Task<User> GetByIdAsync(int id);
        void DeleteUser(int id);
        Task DeleteUserAsync(int id);
    }
}
