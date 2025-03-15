using AutoMapper;
using Portal.BLL.DTO;
using Portal.BLL.Infrastructure;
using Portal.BLL.Interfaces;
using Portal.DAL.Entities;
using Portal.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BLL.Services
{
    public class GenreService: IGenreService
    {
        IUnitOfWork Database { get; set; }
        public GenreService(IUnitOfWork iuof)
        {
            Database = iuof;
        }
        public async Task CreateGenre(GenreDTO genreDTO)
        {
            var genre = new Genre
            {
                Name = genreDTO.Name,
               
            };
            await Database.Genres.Create(genre);
            await Database.Save();
        }
        public async Task DeleteGenre(int id)
        {
            await Database.Genres.Delete(id);
            await Database.Save();
        }
        public async Task<GenreDTO> GetGenreById(int id)
        {
            var gen = await Database.Genres.GetGenreById(id);
            if (gen == null)
                throw new ValidationExcept("Жанр не найден!", "");
            return new GenreDTO
            {
                Id = gen.Id,
                Name = gen.Name
            };
        }
        public async Task<GenreDTO> GetGenreByName(string name)
        {
            var gen = await Database.Genres.GetGenreByName(name);
            if (gen != null)
            {
                return new GenreDTO
                {
                    Id = gen.Id,
                    Name = gen.Name
                };
            }
            else
            {
                return null;
            }
            
        }
        public async Task<IEnumerable<GenreDTO>> GetAllGenres()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>()).CreateMapper(); //создаем обьект и говорим что на что мы мапим
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(await Database.Genres.GetAllGenres());
        }
    }
}
