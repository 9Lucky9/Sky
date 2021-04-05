using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            User user = new User(Login.Text, Email.Text, Password.Password);
            user.Insert();
        }
    }
}
