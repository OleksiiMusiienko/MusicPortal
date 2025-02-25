using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portal.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Заполните поле")]
        [Display (Name = "Имя")]
        public string? Name { get; set; }
        public string? LoginMail { get; set; }
        public bool StatusAdmin { get; set; } //свойство статус определяет права пользователя (админ или пользователь), доступ админ +
        public string? Password { get; set; } //при доступе админа в пароль присваивать null for  - public async Task UpdateUser(UserDTO userDTO), там проверка
        public string? Salt { get; set; }
        public bool Register { get; set; } //необходимость регистрации, доступ админ +
        public string? DateReg { get; set; }
    }
}
