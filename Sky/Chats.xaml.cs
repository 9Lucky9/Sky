using Sky.Models;
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
    public partial class Chats : Window
    {
        public Chats()
        {
            InitializeComponent();
            ChatList.ItemsSource = ChatUser.ChatUsers;
        }

        private void NewChat(object sender, RoutedEventArgs e)
        {
            ChatFrame.Navigate(new NewChat());
        }
    }
}
