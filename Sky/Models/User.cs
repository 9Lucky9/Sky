using System.Data.SqlClient;
using System.Windows;

namespace Sky
{
    public class User
    {
        public static User CurrentUser { get; set; }
        public int ID { get; set; }
        public string Login { get; set; }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set => password = value;
        }
        public User() { }
        public User(int ID, string Login, string Email, string Password)
        {
            this.ID = ID;
            this.Login = Login;
            this.Email = Email;
            this.Password = Password;
        }
        public User(string Login, string Email, string Password)
        {
            this.Login = Login;
            this.Email = Email;
            this.Password = Password;
        }

        public void Insert()
        {
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "INSERT INTO [User] VALUES(@Login, @Email, @Password)";
                command.Parameters.Add(new SqlParameter("@Login", Login));
                command.Parameters.Add(new SqlParameter("@Email", Email));
                command.Parameters.Add(new SqlParameter("@Password", Password));
                command.ExecuteNonQuery();
            }
        }
        private void Delete()
        {
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = Connection;
                command.CommandText = "DELETE FROM [User] WHERE [Id] = @Id";
                command.Parameters.Add(new SqlParameter("@Id", ID));
                command.ExecuteNonQuery();
            }
        }
        public void Update()
        {
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = Connection;
                command.CommandText = "UPDATE [User] SET [Email] = @Email, " +
                "[Password] = @Password, " +
                "WHERE [User].[Id] = @Id";
                command.Parameters.Add(new SqlParameter("@Email", Email));
                command.Parameters.Add(new SqlParameter("@Password", Password));
                command.Parameters.Add(new SqlParameter("@Id", ID));
                command.ExecuteNonQuery();
            }
        }
        public static User GetUserByLogin(string Login)
        {
            User user = new User();
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "SELECT * FROM [User] WHERE Login = @Login";
                command.Parameters.Add(new SqlParameter("@Login", Login));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.ID = reader.GetInt32(0);
                    user.Login = reader.GetString(1);
                    user.Email = reader.GetString(2);
                    user.Password = reader.GetString(3);
                }
            }
            return user;
        }
        public static User AuthUser(string email, string password)
        {
            User user = new User();
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "SELECT * FROM [User] WHERE Email = @Email AND Password = @Password";
                command.Parameters.Add(new SqlParameter("@Email", email));
                command.Parameters.Add(new SqlParameter("@Password", password));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user.ID = reader.GetInt32(0);
                    user.Login = reader.GetString(1);
                    user.Email = reader.GetString(2);
                    user.Password = reader.GetString(3);
                }
            }
            return user;
        }
    }
}
