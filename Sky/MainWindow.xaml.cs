using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Sky
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void RegistrationClick(object sender, MouseButtonEventArgs e)
        {
            new Registration().Show();
            Close();
        }

        private void SignInClick(object sender, RoutedEventArgs e)
        {
            if (Email.Text == "" || Password.Password == "")
            {
                MessageBox.Show("Заполните поля");
                return;
            }
            User.CurrentUser = User.AuthUser(Email.Text, Password.Password);
            if(User.CurrentUser.ID == 0)
            {
                MessageBox.Show("Не верный логин или пароль!");
                return;
            }
            new Chats().Show();
            Close();
        }
        private void SHA512PasswordHash(string password)
        {
            byte[] data = Encoding.UTF8.GetBytes(password);
            byte[] result;
            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(data);

            var builder = new StringBuilder(result.Length * 2);
            for (int i = 0; i < result.Length; i++)
            {
                builder.Append(result[i].ToString("X2"));
            }
            MessageBox.Show(builder.ToString());
        }
    }
}
