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

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [Display(Name = "Название")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [Display(Name = "Автор")]
        public string? Author { get; set; }
        public string? Path { get; set; }  //путь к папке с песней

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [Display(Name = "Жанр")]
        public int? GenreId { get; set; } //для реализации комбобокса

        [Display(Name = "Жанр")]
        public string? Genre { get; set; } //отображение жанра
    }
}
