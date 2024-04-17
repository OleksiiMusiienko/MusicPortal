using Portal.DAL.Entities;

namespace Portal.DAL.Interfaces
{
    public interface IRepositoryUser
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUserId(int id);
        Task<User> GetUserName(string name);
        Task<bool> GetUserLog(string log);
        Task Create(User item);
        void Update(User item);
        Task Delete(int id);
        void RegisterUser(User item);
    }
}
