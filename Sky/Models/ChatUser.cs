using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky.Models
{
    public class ChatUser
    {
        public int ID { get; set; }
        public int user_id { get; set; }
        public int role_id { get; set; }
        public int chat_id { get; set; }
        public string chatName { get; set; }

        public ChatUser() { }
        public ChatUser(int user_id, int role_id, int chat_id)
        {
            this.user_id = user_id;
            this.role_id = role_id;
            this.chat_id = chat_id;
        }
        public ChatUser(int ID, int user_id, int role_id, int chat_id)
        {
            this.ID = ID;
            this.user_id = user_id;
            this.role_id = role_id;
            this.chat_id = chat_id;
        }
        public ChatUser(int ID, int user_id, int role_id, int chat_id, string chatName)
        {
            this.ID = ID;
            this.user_id = user_id;
            this.role_id = role_id;
            this.chat_id = chat_id;
            this.chatName = chatName;
        }
        private static ObservableCollection<ChatUser> chatUsers = GetChatsByUser(User.CurrentUser.ID);
        public static ObservableCollection<ChatUser> ChatUsers { get => chatUsers; set => chatUsers = value; }
        public void Insert()
        {
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "INSERT INTO [ChatUser] VALUES(@user_id, @role_id, @chat_id)";
                command.Parameters.Add(new SqlParameter("@user_id", user_id));
                command.Parameters.Add(new SqlParameter("@role_id", role_id));
                command.Parameters.Add(new SqlParameter("@chat_id", chat_id));
                command.ExecuteNonQuery();
            }
        }
        private static ObservableCollection<ChatUser> GetChatsByUser(int User_id)
        {
            ObservableCollection<ChatUser> chatUsers = new ObservableCollection<ChatUser>();
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "SELECT [ChatUser].[Id], [ChatUser].[User_id], [ChatUser].[Role_id], [ChatUser].[Chat_id], [Chat].[Name] FROM[ChatUser] INNER JOIN[Chat] ON[Chat].[Id] = [ChatUser].[Chat_id] WHERE[ChatUser].[User_id] = @User_id";
                command.Parameters.Add(new SqlParameter("@User_id", User_id));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ChatUser chatUser = new ChatUser(reader.GetInt32(0),
                                         reader.GetInt32(1),
                                         reader.GetInt32(2),
                                         reader.GetInt32(3),
                                         reader.GetString(4));
                    chatUsers.Add(chatUser);
                }
            }
            return chatUsers;
        }
    }
}
