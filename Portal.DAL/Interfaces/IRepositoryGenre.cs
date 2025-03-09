using Portal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.DAL.Interfaces
{
    public interface IRepositoryGenre
    {
        Task<IEnumerable<Genre>> GetAllGenres();
        Task<Genre> GetGenreById(int id);
        Task<Genre> GetGenreByName(string name);
        Task Create(Genre item);
        Task Delete(int id);
    }
}
