using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class EditUserAdmin
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Имя")]
        public string? Name { get; set; }
        public string? LoginMail { get; set; }
        public bool StatusAdmin { get; set; }
        public bool Register { get; set; }
    }
}
