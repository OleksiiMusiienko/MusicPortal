using Microsoft.EntityFrameworkCore;
using Portal.DAL.EF;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.DAL.Repositories
{
    public class SongRepository: IRepositorySG<Song>
    {
        PortalContext db;
        public SongRepository(PortalContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Song>> GetAll()
        {
            return await db.Songs.ToListAsync();
        }
        public async Task<Song> GetId(int id)
        {
            Song? song = await db.Songs.FindAsync(id);
            return song!;
        }
        public async Task<Song> GetName(string name)
        {
            var songs = await db.Songs.Where(a => a.Name == name).ToListAsync();
            Song? song = songs?.FirstOrDefault();
            return song!;
        }
        public async Task Create(Song song)
        {
            await db.Songs.AddAsync(song);
        }
        public void Update(Song song)
        {
            db.Entry(song).State = EntityState.Modified;
        }
        public async Task Delete(int id)
        {
            Song? song = await db.Songs.FindAsync(id);
            if (song != null)
            {
                db.Songs.Remove(song);
            }
        }
    }
}
