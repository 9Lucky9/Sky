using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Sky.Models
{
    public class ChatUser
    {
        public int ID { get; set; }
        public int User_id { get; set; }
        public int Role_id { get; set; }
        public int Chat_id { get; set; }
        public string ChatName { get; set; }

        public ChatUser() { }
        /// <summary>
        /// Insert ChatUser to database
        /// </summary>
        public ChatUser(int user_id, int role_id, int chat_id)
        {
            User_id = user_id;
            Role_id = role_id;
            Chat_id = chat_id;
        }
        /// <summary>
        /// Load ChatUser from database
        /// </summary>
        public ChatUser(int ID, int user_id, int role_id, int chat_id, string chatName)
        {
            this.ID = ID;
            User_id = user_id;
            Role_id = role_id;
            Chat_id = chat_id;
            ChatName = chatName;
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
                command.Parameters.Add(new SqlParameter("@user_id", User_id));
                command.Parameters.Add(new SqlParameter("@role_id", Role_id));
                command.Parameters.Add(new SqlParameter("@chat_id", Chat_id));
                command.ExecuteNonQuery();
            }
        }
        public static void Delete(int chatID, int chatUser)
        {
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = Connection;
                command.CommandText = "DELETE FROM [ChatUser] WHERE [user_id] = @user_id AND [chat_id] = @chat_id";
                command.Parameters.Add(new SqlParameter("@user_id", chatID));
                command.Parameters.Add(new SqlParameter("@chat_id", chatUser));
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
