using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class EditUserAdminModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Имя")]        
        public string? Name { get; set; }

        [Display(Name = "Зарегистриван")]
        public bool Register {  get; set; }
        public bool StatusAdmin { get; set; }

    }
}
