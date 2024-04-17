namespace MusicPortal.Models
{
    public class ViewUserRegister
    {
        public string? Name { get; set; }
        public string? LoginMail { get; set; }      
        public string? Password { get; set; } //при доступе админа в пароль присваивать null for  - public async Task UpdateUser(UserDTO userDTO), там проверка
        public string? PasswordConfirm { get; set; }       
        
    }
}
