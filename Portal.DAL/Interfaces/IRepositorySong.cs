using Portal.DAL.Entities;
namespace Portal.DAL.Interfaces
{
    public interface IRepositorySong
    {
        Task<IEnumerable<Song>> GetAllSongs();
        Task<IEnumerable<Song>> GetSongsByGenre(Genre genre);
        Task<IEnumerable<Song>> GetSongsByAuthor(string author);
        Task<Song> GetSongById(int id);
        //Task<Song> GetSongByName(string name);
        Task Create(Song item);
        void Update(Song item);
        Task Delete(int id);
    }
   
}
