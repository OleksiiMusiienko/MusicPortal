using Microsoft.EntityFrameworkCore;
using Portal.DAL.EF;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.DAL.Repositories
{
    public class GenreRepository: IRepositorySG<Genre>
    {
        PortalContext db;
        public GenreRepository(PortalContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await db.Genres.ToListAsync();
        }
        public async Task<Genre> GetId(int id)
        {
            Genre? gen = await db.Genres.FindAsync(id);
            return gen!;
        }
        public async Task<Genre> GetName(string name)
        {
            var genres = await db.Genres.Where(a => a.Name == name).ToListAsync();
            Genre? gen = genres?.FirstOrDefault();
            return gen!;
        }
        public async Task Create(Genre gen)
        {
            await db.Genres.AddAsync(gen);
        }
        public void Update(Genre gen)
        {
            db.Entry(gen).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            Genre? gen = await db.Genres.FindAsync(id);
            if (gen != null)
                db.Genres.Remove(gen);
        }
    }
}
