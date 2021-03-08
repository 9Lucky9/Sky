using Sky.Models;
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
        private DB dB = new DB();
        public ObservableCollection<ChatUser> ChatUsers = new ObservableCollection<ChatUser>();
        public List<Role> Roles;
        private int chatID;
        public NewChat()
        {
            InitializeComponent();
            UserList.ItemsSource = ChatUsers;
            Roles = dB.GetAllRoles();
            Role.ItemsSource = Roles;
        }

        private void CreateChatClick(object sender, RoutedEventArgs e)
        {
            if(ChatName.Text == "")
            {
                MessageBox.Show("Нельзя создать чат с пустым названием!");
                return;
            }
            if(ChatUsers.Count == 0)
            {
                MessageBox.Show("Нельзя создать чат без пользователей!");
                return;
            }
            dB.InsertNewChat(ChatName.Text);
            chatID = dB.GetLastChatID() + 1;
            foreach (ChatUser chatUser in ChatUsers)
            {
                dB.InsertNewChat_user(chatUser.user_id, chatUser.role_id, chatUser.chat_id);
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
            int userID = dB.GetUserIDByLogin(UserName.Text);
            if (userID == 0)
            {
                MessageBox.Show("Такого пользователя не существует!");
                return;
            }
            ChatUser chatUser = new ChatUser(null, userID, (Role.SelectedItem as Role).ID, chatID);
            ChatUsers.Add(chatUser);
            Console.WriteLine("Успешное добавление участника в список участников!");
        }
        private void DeleteUserFromChatListClick(object sender, RoutedEventArgs e)
        {
            var item = ChatUsers.First(user => user.user_id ==  Convert.ToInt32((sender as Button).Tag));
            ChatUsers.Remove(item);
        }
    }
}
