using Microsoft.EntityFrameworkCore;
using Portal.DAL.EF;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;

namespace Portal.DAL.Repositories
{
    public class SongRepository: IRepositorySong
    {
        PortalContext db;
        public SongRepository(PortalContext context)
        {
            db = context;
        }
        public async Task<IEnumerable<Song>> GetAllSongs()
        {
            return await db.Songs.Include(s=>s.Genre).ToListAsync();
        }
        public async Task<IEnumerable<Song>> GetSongsByGenre(Genre genre)
        {
            var songs = await db.Songs.Where(s=>s.Genre == genre).ToListAsync();
            return songs!;
        }
        public async Task<IEnumerable<Song>> GetSongsByAuthor(string author)
        {
            var songs = await db.Songs.Where(s=>s.Author == author).ToListAsync();
            return songs!;
        }
        public async Task<Song> GetSongById(int id)
        {
            Song? song = await db.Songs.Include(s => s.Genre).FirstOrDefaultAsync(s => s.Id == id);
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

        //public async Task<Song> GetId(int id)
        //{
        //    Song? song = await db.Songs.FindAsync(id);
        //    return song!;
        //}
        //public async Task<Song> GetName(string name)
        //{
        //    var songs = await db.Songs.Where(a => a.Name == name).ToListAsync();
        //    Song? song = songs?.FirstOrDefault();
        //    return song!;
        //}
    }
}
