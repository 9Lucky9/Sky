using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Sky.Models
{
    public class Message
    {
        public int ID { get; set; }
        public int User_id { get; set; }
        public int Chat_id { get; set; }
        public int ContentType { get; set; }
        public byte[] Content { get; set; }
        public DateTime Date { get; set; }
        public string User_login { get; set; }

        public Message() { }
        /// <summary>
        /// Insert new message to database
        /// </summary>
        public Message(int user_id, int chat_id, int contentType, byte[] content)
        {
            User_id = user_id;
            Chat_id = chat_id;
            ContentType = contentType;
            Content = content;
            Date = DateTime.Now;
            User_login = User.CurrentUser.Login;
            Insert();
        }
        /// <summary>
        /// Constuctor for load messages from database
        /// </summary>
        public Message(int iD, int user_id, int chat_id, int contentType, byte[] content, DateTime date, string user_login)
        {
            ID = iD;
            User_id = user_id;
            Chat_id = chat_id;
            ContentType = contentType;
            Content = content;
            Date = date;
            User_login = user_login;
        }
        private void Insert()
        {
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                var binaryContent = new System.Data.SqlTypes.SqlBinary(Content);
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "INSERT INTO [Message] VALUES(@user_id, @chat_id, @ContentType, @content, @date)";
                command.Parameters.Add(new SqlParameter("@user_id", User_id));
                command.Parameters.Add(new SqlParameter("@chat_id", Chat_id));
                command.Parameters.Add(new SqlParameter("@ContentType", ContentType));
                command.Parameters.Add(new SqlParameter("@content", binaryContent));
                command.Parameters.Add(new SqlParameter("@date", Date));
                command.ExecuteNonQuery();
            }
        }
        private void Update()
        {
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = Connection;
                command.CommandText = "UPDATE [Message] SET [Content] = @Content " +
                "WHERE [Message].[Id] = @Id";
                command.Parameters.Add(new SqlParameter("@Content", Content));
                command.Parameters.Add(new SqlParameter("@Id", ID));
                command.ExecuteNonQuery();
            }
        }
        public static ObservableCollection<Message> GetMessagesFromDB(int chat_id)
        {
            ObservableCollection<Message> messages = new ObservableCollection<Message>();
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = Connection;
                command.CommandText = "SELECT [Message].[Id], [Message].[User_id], [Message].[Chat_id], [Message].[ContentType], [Message].[Content], [Message].[Date], [User].[Login] FROM [Message] INNER JOIN [User] ON [User].[Id] = [Message].[User_id] WHERE [Message].[Chat_id] = @Id";
                command.Parameters.Add(new SqlParameter("@Id", chat_id));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Message message = new Message(reader.GetInt32(0),
                                                  reader.GetInt32(1),
                                                  reader.GetInt32(2),
                                                  reader.GetInt32(3),
                                                  (byte[])reader.GetSqlBinary(4),
                                                  reader.GetDateTime(5),
                                                  reader.GetString(6));
                    messages.Add(message);
                }
            }
            return messages;
        }
    }
}
