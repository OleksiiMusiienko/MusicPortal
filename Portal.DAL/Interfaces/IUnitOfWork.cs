using Portal.DAL.Entities;

namespace Portal.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryUser Users { get; }
        IRepositorySong Songs { get; } 
        IRepositoryGenre Genres { get; }
        Task Save();
    }
}
