using Portal.DAL.Entities;

namespace Portal.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryUser Users { get; }
        IRepositorySG<Song> Songs { get; } 
        IRepositorySG<Genre> Genres { get; }
        Task Save();
    }
}
