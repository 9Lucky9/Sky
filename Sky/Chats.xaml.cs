using Microsoft.AspNetCore.SignalR.Client;
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
        private readonly HubConnection hubConnection;
        public Chats()
        {
            InitializeComponent();
            hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/Chat")
            .Build();

            ChatList.ItemsSource = ChatUser.ChatUsers;
        }

        private void NewChat(object sender, RoutedEventArgs e)
        {
            ChatFrame.Navigate(new NewChat());
        }

        private void TextBlock_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Chat_LeftClick(object sender, MouseButtonEventArgs e)
        {
            var item = ChatList.SelectedItem as ChatUser;
            ChatFrame.Navigate(new ChatWindow(item));
        }
    }
}
