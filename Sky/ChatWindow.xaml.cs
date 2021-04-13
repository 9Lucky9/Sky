using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Win32;
using Sky.Models;
using Sky.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
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
        private bool recordingSound = false;

        public ChatWindow(ChatUser chatUser)
        {
            InitializeComponent();
            chatID = chatUser.chat_id;
            
            messages = Message.GetMessagesFromDB(chatID);
            //loadRService();
            LoadMessages();
        }

        private async void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            if (messageBox.Text == "")
                return;
            byte[] array = Encoding.UTF8.GetBytes(messageBox.Text);
            Message message = new Message(User.CurrentUser.ID, chatID, (int)ContentType.ContentTypes.Text, array);
            Messages.Items.Add(CreateTextMessageUI(message));
            //await rService.SendMessage(message);
        }
        private async void loadRService()
        {
            rService = new SignalRService();
            rService.MessageReceived += RService_MessageReceived;
            await rService.StartConnection();
            await rService.addToGroup(chatID.ToString());
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
                        Messages.Items.Add(CreatePhotoMessageUI(message));
                        break;
                    case (int)ContentType.ContentTypes.Video:
                        
                        break;
                }
            }
        }

        private void AttachFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == false)
                return;
            string filename = openFileDialog.FileName;
            FileInfo fileInfo = new FileInfo(filename);
            if (fileInfo.Extension == ".jpg" || fileInfo.Extension == ".jpeg" || fileInfo.Extension == ".png")
            {
                Console.WriteLine("Сработал!");
                Message message = new Message(User.CurrentUser.ID, chatID, (int)ContentType.ContentTypes.Picture, File.ReadAllBytes(filename));
                Messages.Items.Add(CreatePhotoMessageUI(message));
            }
            if(fileInfo.Extension == ".mp4")
            {

            }
        }

        private async void AuidoMessage_Click(object sender, RoutedEventArgs e)
        {
            if (recordingSound == false)
            {
                sound = new Sound();
                sound.StartRecord();
                recordingSound = true;
            }
            else
            {
                sound.StopRecord();
                Message message = new Message(User.CurrentUser.ID, chatID, (int)ContentType.ContentTypes.VoiceMessage, sound.audioBytes.ToArray());
                messages.Add(message);
                //await rService.SendMessage(message);
                recordingSound = false;
            }
        }
        private StackPanel CreateTextMessageUI(Message message)
        {
            StackPanel stackPanel = new StackPanel();
            TextBlock user = new TextBlock()
            {
                Text = message.User_login,
                FontWeight = FontWeights.Bold,
                Tag = message.User_id
            };
            TextBlock text = new TextBlock()
            {
                Tag = message.ID,
                Text = Encoding.UTF8.GetString(message.Content)
            };
            TextBlock date = new TextBlock()
            {
                Tag = message.ID,
                Text = message.Date.ToString()
            };
            stackPanel.Children.Add(user);
            stackPanel.Children.Add(text);
            stackPanel.Children.Add(date);
            return stackPanel;
        }
        private StackPanel CreateAudioMessageUI(Message message)
        {
            StackPanel stackPanel = new StackPanel();
            TextBlock user = new TextBlock()
            {
                Text = message.User_login,
                FontWeight = FontWeights.Bold,
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
        private StackPanel CreatePhotoMessageUI(Message message)
        {
            StackPanel stackPanel = new StackPanel();
            TextBlock user = new TextBlock()
            {
                Text = message.User_login,
                FontWeight = FontWeights.Bold,
                Tag = message.User_id
            };
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(message.Content);
            bi.EndInit();
            Image image = new Image()
            {
                Source = bi,
                Height = 250
            };
            stackPanel.Children.Add(user);
            stackPanel.Children.Add(image);
            return stackPanel;
        }
        private StackPanel CreateVideoMessageUI()
        {
            StackPanel stackPanel = new StackPanel();
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
