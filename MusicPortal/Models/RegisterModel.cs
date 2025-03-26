using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                   ErrorMessageResourceName = "NameRequiredView")]
        [Display(Name = "UserName", ResourceType = typeof(Resources.Resource))]
        public string? Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                   ErrorMessageResourceName = "NameRequiredView")]
        [Display(Name = "Login", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес электронной почты")]
        public string? LoginMail { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                   ErrorMessageResourceName = "NameRequiredView")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{6,16}$", ErrorMessage = "Не менее 6 символов, A, a, спецсимволы")]
        public string? Password { get; set; } //при доступе админа в пароль присваивать null for  - public async Task UpdateUser(UserDTO userDTO), там проверка

        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource),
                   ErrorMessageResourceName = "PasswordsDoNotMatch")]
        [DataType(DataType.Password)]
        [Display(Name = "RepeatPassword", ResourceType = typeof(Resources.Resource))]
        public string? PasswordConfirm { get; set; }

        [Display(Name = "Registration", ResourceType = typeof(Resources.Resource))]
        public bool Register {  get; set; }
        public string? DateReg { get; set; }
        public bool StatusAdmin { get; set; }

    }
}
