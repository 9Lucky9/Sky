namespace Sky
{
    public class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User() { }
        public User(int ID, string Login, string Email, string Password) 
        {
            this.ID = ID;
            this.Login = Login;
            this.Email = Email;
            this.Password = Password;
        }
    }
}
