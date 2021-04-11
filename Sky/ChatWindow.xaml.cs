using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Win32;
using Sky.Models;
using Sky.Services;
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
    public partial class ChatWindow : Page
    {
        private int chatID;
        private ObservableCollection<Message> messages;
        private SignalRService rService;
        private Sound sound;
        private bool recording = false;
        public ChatWindow(ChatUser chatUser)
        {
            InitializeComponent();
            chatID = chatUser.chat_id;
            
            messages = Message.GetMessagesFromDB(chatID);
            loadRService();
            LoadMessages();
        }
        private async void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            byte[] array = Encoding.UTF8.GetBytes(messageBox.Text);
            Message message = new Message(User.CurrentUser.ID, chatID, (int)ContentType.ContentTypes.Text, array);
            await rService.SendMessage(message);
        }
        private void loadRService()
        {
            rService = new SignalRService();
            rService.StartConnection();
            rService.addToGroup(chatID.ToString());
            rService.MessageReceived += RService_MessageReceived;
        }
        private void RService_MessageReceived(Message obj)
        {
            messages.Add(obj);
        }
        private void LoadMessages()
        {
            foreach (Message message in messages)
            {
                switch (message.ContentType)
                {
                    case (int)ContentType.ContentTypes.Text:
                        Messages.Items.Add(CreateTextMessageUI(message));
                        break;

                    case (int)ContentType.ContentTypes.VoiceMessage:
                        Messages.Items.Add(CreateAudioMessageUI(message));
                        break;
                    case (int)ContentType.ContentTypes.Picture:

                        break;

                }
            }
        }

        private void AttachFile_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void AuidoMessage_Click(object sender, RoutedEventArgs e)
        {
            if (recording == false)
            {
                sound = new Sound();
                sound.StartRecord();
                recording = true;
            }
            else
            {
                sound.StopRecord();
                Message message = new Message(User.CurrentUser.ID, chatID, (int)ContentType.ContentTypes.VoiceMessage, sound.audioBytes.ToArray());
                //await rService.SendMessage(message);
                recording = false;
            }


        }
        private StackPanel CreateTextMessageUI(Message message)
        {
            StackPanel stackPanel = new StackPanel();
            TextBlock user = new TextBlock()
            {
                Text = message.User_id.ToString(),
                Tag = message.User_id
            };
            TextBlock text = new TextBlock()
            {
                Tag = message.ID,
                Text = Encoding.UTF8.GetString(message.Content)
            };
            stackPanel.Children.Add(user);
            stackPanel.Children.Add(text);
            return stackPanel;
        }
        private StackPanel CreateAudioMessageUI(Message message)
        {
            StackPanel stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock user = new TextBlock()
            {
                Text = message.User_id.ToString(),
                Tag = message.User_id
            };
            Button button = new Button()
            {
                Tag = message.ID,
                Content = new Image
                {
                    Height = 40,
                    Source = new BitmapImage(new Uri("Resources/ChatWindow/TransparentPlay.png", UriKind.Relative))
                }
            };
            button.Click += PlayAudioMessage_Click;
            //Slider slider = new Slider();
            stackPanel.Children.Add(user);
            stackPanel.Children.Add(button);
            //stackPanel.Children.Add(slider);
            return stackPanel;
        }

        private void PlayAudioMessage_Click(object sender, RoutedEventArgs e)
        {
            foreach (Message message in messages)
            {
                if (message.ID == Convert.ToInt32((sender as Button).Tag))
                {
                    Sound.PlayAudio(message.Content);
                    break;
                }
            }
        }
    }
}
