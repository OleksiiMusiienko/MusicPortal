using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class EditModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Имя")]        
        public string? Name { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Логин")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес электронной почты")]
        public string? LoginMail { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [Display(Name = "Старый пароль")]
        [DataType(DataType.Password)]
        
        public string? Password { get; set; } //при доступе админа в пароль присваивать null for  - public async Task UpdateUser(UserDTO userDTO), там проверка

        [Display(Name = "Новый пароль")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{6,16}$", ErrorMessage = "Пароль должен быть не менее 6 знаков, большие, маленькие буквы, символы")]
        public string? NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string? PasswordConfirm { get; set; }      
        
    }
}
