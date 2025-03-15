using Microsoft.AspNetCore.Mvc.Rendering;
using Portal.BLL.DTO;

namespace MusicPortal.Models
{
    public class FilterViewModel
    {
        public SelectList Genres { get; }
        public int SelectedGenre { get; }
        public string SelectedAuthor { get; }

        public FilterViewModel(List<GenreDTO> genres, int genre, string author)
        {
            genres.Insert(0, new GenreDTO { Name = "All", Id = 0 });
            Genres = new SelectList(genres, "Id", "Name", genre);
            SelectedGenre = genre;
            SelectedAuthor = author;
        }
    }
}
