using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sky
{
    public partial class NewChat : Page
    {
        private ObservableCollection<User> NewChatUsers = new ObservableCollection<User>();
        public NewChat()
        {
            InitializeComponent();
            UserList.ItemsSource = NewChatUsers;
        }

        private void CreateChatClick(object sender, RoutedEventArgs e)
        {
            DB dB = new DB();
            dB.GetLastChatID();
            foreach (User user in NewChatUsers)
            {
                dB.InsertNewChat_user(user.ID, );
            }
        }

        private void CancelNewChatClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void AddUserToChatListClick(object sender, RoutedEventArgs e)
        {
            if (UserName.Text == "")
            {
                MessageBox.Show("Заполните логин участника!");
                return;
            }
            DB dB = new DB();
            User user = dB.ReturnUserByLogin(UserName.Text);
            if (user != null)
            {
                NewChatUsers.Add(user);
                Console.WriteLine("Успешное добавление участника!");
            }

        }
    }
}
