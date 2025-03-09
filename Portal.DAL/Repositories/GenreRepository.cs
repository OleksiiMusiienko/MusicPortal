using Microsoft.EntityFrameworkCore;
using Portal.DAL.EF;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.DAL.Repositories
{
    public class GenreRepository: IRepositoryGenre
    {
        PortalContext db;
        public GenreRepository(PortalContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Genre>> GetAllGenres()
        {
            return await db.Genres.ToListAsync();
        }
        public async Task<Genre> GetGenreById(int id)
        {
            Genre? gen = await db.Genres.FindAsync(id);
            return gen!;
        }
        public async Task<Genre> GetGenreByName(string name)
        {
            var genres = await db.Genres.Where(a => a.Name == name).ToListAsync();
            Genre? gen = genres?.FirstOrDefault();
            return gen!;
        }
        public async Task Create(Genre gen)
        {
            await db.Genres.AddAsync(gen);
        }       
        public async Task Delete(int id)
        {
            Genre? gen = await db.Genres.FindAsync(id);
            if (gen != null)
                db.Genres.Remove(gen);
        }
    }
}
