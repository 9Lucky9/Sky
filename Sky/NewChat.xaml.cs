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
    public partial class NewChat : UserControl
    {
        private ObservableCollection<ChatUser> ChatUsers = new ObservableCollection<ChatUser>();
        private ObservableCollection<User> Users = new ObservableCollection<User>();
        public NewChat()
        {
            InitializeComponent();
            UserList.ItemsSource = Users;
            Roles.ItemsSource = Role.Roles;
        }

        private void CreateChatClick(object sender, RoutedEventArgs e)
        {
            if (ChatName.Text == "")
            {
                MessageBox.Show("Нельзя создать чат с пустым названием!");
                return;
            }
            if (ChatUsers.Count == 0)
            {
                MessageBox.Show("Нельзя создать чат без пользователей!");
                return;
            }
            Chat chat = new Chat(ChatName.Text);
            ChatUser currentChatUser = new ChatUser(User.CurrentUser.ID, 1, chat.ID);
            ChatUsers.Add(currentChatUser);
            foreach (ChatUser chatUser in ChatUsers)
            {
                chatUser.chat_id = chat.ID;
                chatUser.Insert();
            }
            MessageBox.Show("Новый чат успешно создан!");
            Properties.Settings.Default.Save();
        }

        private void CancelNewChatClick(object sender, RoutedEventArgs e)
        {

        }

        private void AddUserToChatListClick(object sender, RoutedEventArgs e)
        {
            if (UserLogin.Text == "")
            {
                MessageBox.Show("Заполните логин участника!");
                return;
            }
            if (UserLogin.Text == User.CurrentUser.Login)
            {
                MessageBox.Show("Вы не можете добавить себя в ваш новый чат!");
                return;
            }
            var user = User.GetUserByLogin(UserLogin.Text);
            if (user.ID == 0)
            {
                MessageBox.Show("Такого пользователя не существует!");
                return;
            }
            foreach (ChatUser chatUser1 in ChatUsers)
            {
                if (chatUser1.user_id == user.ID)
                {
                    MessageBox.Show("Вы не можете добавить одинаковых пользователей в чат!");
                    return;
                }
            }
            Users.Add(user);
            ChatUser chatUser = new ChatUser(user.ID, (Roles.SelectedItem as Role).ID, 0);
            ChatUsers.Add(chatUser);
        }
        private void DeleteUserFromChatListClick(object sender, RoutedEventArgs e)
        {
            var user1 = Users.First(user => user.ID == Convert.ToInt32((sender as Button).Tag));
            var chatUser = ChatUsers.First(chatUser1 => chatUser1.user_id == Convert.ToInt32((sender as Button).Tag));
            Users.Remove(user1);
            ChatUsers.Remove(chatUser);
        }
    }
}
