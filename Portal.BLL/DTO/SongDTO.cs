using Portal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BLL.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "NameRequiredView")]
        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string? Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "NameRequiredView")]
        [Display(Name = "Author", ResourceType = typeof(Resources.Resource))]
        public string? Author { get; set; }
        public string? Path { get; set; }  //путь к папке с песней

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "NameRequiredView")]
        [Display(Name = "Genre", ResourceType = typeof(Resources.Resource))]
        public int? GenreId { get; set; } //для реализации комбобокса

        [Display(Name = "Genre", ResourceType = typeof(Resources.Resource))]
        public string? Genre { get; set; } //отображение жанра
    }
}
