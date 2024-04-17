using Portal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BLL.Interfaces
{
    public interface ISongService
    {
        Task CreateSong(SongDTO song);
        Task DeleteSong(SongDTO song);
        Task UpdateSong(SongDTO song);
        Task<SongDTO> GetSong(int id);
        Task<IEnumerable<SongDTO>> GetAllSongs();
    }
}
