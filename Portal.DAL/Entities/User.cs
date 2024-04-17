namespace Portal.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LoginMail { get; set; }
        public bool StatusAdmin { get; set; } //свойство статус определяет права пользователя (админ или пользователь)
        public string? Password { get; set; }        
        public string? Salt { get; set; }
        public bool Register { get; set; } //необходимость регистрации
        public string? DateReg { get; set; }
    }
}
