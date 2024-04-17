using Portal.DAL.Entities;
namespace Portal.DAL.Interfaces
{
    public interface IRepositorySG<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetId(int id);
        Task<T> GetName(string name);
        Task Create(T item);
        void Update(T item);
        Task Delete(int id);
    }
   
}
