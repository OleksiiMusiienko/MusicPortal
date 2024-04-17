using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Логин")]
        public string? LoginMail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string? Password { get; set; }
    }
}
