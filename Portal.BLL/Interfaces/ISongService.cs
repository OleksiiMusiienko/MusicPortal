using Portal.BLL.DTO;
using Portal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BLL.Interfaces
{
    public interface ISongService
    {
        Task Create(SongDTO song);
        Task Delete(int id);
        Task Update(SongDTO song);
        Task<IEnumerable<SongDTO>> GetAllSongs();
        Task<IEnumerable<SongDTO>> GetSongsByGenre(GenreDTO genre);
        Task<IEnumerable<SongDTO>> GetSongsByAuthor(string author);
        Task<SongDTO> GetSongById(int id);        
    }
}
