using System.Windows;

namespace Sky
{
    /// <summary>
    /// Список пользователей чата
    /// </summary>
    public partial class ChatUsers : Window
    {
        public ChatUsers(int chatID)
        {
            InitializeComponent();
            ShowUsers(chatID);
        }
        private void ShowUsers(int chatID)
        {

        }

        private void DeleteUserFromChat_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
