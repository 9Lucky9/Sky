using Sky.Models;
using System;
using System.Windows;
using System.Windows.Input;

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
            AddElementToFrame(new NewChat());
        }

        private void Chat_LeftClick(object sender, MouseButtonEventArgs e)
        {
            var item = ChatList.SelectedItem as ChatUser;
            AddElementToFrame(new ChatWindow(item));
        }
        private void AddElementToFrame(UIElement element)
        {
            if (Frame.Children.Count > 0)
            {
                Frame.Children.RemoveAt(0);
                Frame.Children.Add(element);
            }
            else
                Frame.Children.Add(element);
        }
    }
}
