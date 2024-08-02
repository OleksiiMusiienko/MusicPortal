using Portal.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BLL.Interfaces
{
    public interface IGenreService
    {
        Task CreateGenre(GenreDTO genreDTO);
        Task DeleteGenre(int id);
        //Task<GenreDTO> GetGenreById(int id);
        Task<IEnumerable<GenreDTO>> GetAllGenres();
    }
}
