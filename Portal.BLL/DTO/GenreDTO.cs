using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.BLL.DTO
{
    public class GenreDTO
    {
        public int Id { get; set;}

        [Required(ErrorMessage = "Заполните поле")]
        [Display(Name = "Название")]
        public string? Name { get; set; }
    }
}
