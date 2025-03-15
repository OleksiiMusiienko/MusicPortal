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
    public class SongService: ISongService
    {
        IUnitOfWork Database { get; set; }
        public SongService(IUnitOfWork database)
        {
            Database = database;
        }
        public async Task Create(SongDTO _song)
        {
            var song = new Song
            {
                Id = _song.Id,
                Name = _song.Name,
                Author = _song.Author,
                Path = _song.Path,
                GenreId = _song.GenreId
            };
            await Database.Songs.Create(song);
            await Database.Save();
        }
        public async Task Delete(int id)
        {
            await Database.Songs.Delete(id);    
            await Database.Save();
        }
        public async Task Update(SongDTO _song)
        {
            var song = new Song
            {
                Id = _song.Id,
                Name = _song.Name,
                Author = _song.Author,
                Path = _song.Path,
                GenreId = _song.GenreId
            };
            Database.Songs.Update(song);
            await Database.Save();
        }
        public async Task<IEnumerable<SongDTO>> GetAllSongs()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>().ForMember("Genre", opt => opt.MapFrom(g => g.Genre.Name))); //создаем обьект и говорим что на что мы мапим
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.Songs.GetAllSongs());
        }
        public async Task<IEnumerable<SongDTO>> GetSongsByGenre(GenreDTO genre)
        {
            Genre gen = new Genre();
            gen.Id = genre.Id;
            gen.Name = genre.Name;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>().ForMember("Genre", opt => opt.MapFrom(g => g.Genre.Name))); //создаем обьект и говорим что на что мы мапим
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.Songs.GetSongsByGenre(gen));
        }
        public async Task<IEnumerable<SongDTO>> GetSongsByAuthor(string author)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>().ForMember("Genre", opt => opt.MapFrom(g => g.Genre.Name))); //создаем обьект и говорим что на что мы мапим
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.Songs.GetSongsByAuthor(author));
        }
        public async Task<SongDTO> GetSongById(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>().ForMember("Genre", opt => opt.MapFrom(g => g.Genre!.Name))); //создаем обьект и говорим что на что мы мапим
            var mapper = new Mapper(config);
            return mapper.Map<Song, SongDTO>(await Database.Songs.GetSongById(id));           
        }
    }
}
