using System.Windows;
using System.Windows.Input;

namespace Sky
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void BackToMainWindowClick(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void RegisterConfirmClick(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "" || Email.Text == "" || Password.Password == "" || PasswordConfirm.Password == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            if (!Email.Text.Contains("@"))
            {
                MessageBox.Show("Не верный E-mail");
                return;
            }

            new User(Login.Text, Email.Text, Password.Password);
        }
    }
}