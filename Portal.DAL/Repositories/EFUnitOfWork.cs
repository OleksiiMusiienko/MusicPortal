using Portal.DAL.EF;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private PortalContext db;
        private UserRepository userRepository;
        private SongRepository songRepository;
        private GenreRepository genreRepository;
        public EFUnitOfWork(PortalContext context)
        {
            db = context;
        }
        public IRepositoryUser Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }
        public IRepositorySG<Song> Songs
        {
            get
            {
                if (songRepository == null)
                    songRepository = new SongRepository(db);
                return songRepository;
            }
        }
        public IRepositorySG<Genre> Genres
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }
}
