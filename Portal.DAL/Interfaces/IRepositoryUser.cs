using Portal.DAL.Entities;

namespace Portal.DAL.Interfaces
{
    public interface IRepositoryUser
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserId(int id);
        Task<User> GetUserLog(string name);
        Task<bool> GetAdmin();
        Task Create(User item);
        void Update(User item);
        Task Delete(int id);
        void RegisterUser(User item);
        Task<IEnumerable<User>> GetUsersRegister();
    }
}
