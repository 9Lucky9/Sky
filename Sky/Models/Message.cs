using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Windows;

namespace Sky.Models
{
    public class Message
    {
        public Message() { }
        public int ID { get; set; }
        public int User_id { get; set; }
        public int Chat_id { get; set; }
        public int ContentType { get; set; }
        public byte[] Content { get; set; }
        public Message(int user_id, int chat_id, int contentType, byte[] content)
        {
            User_id = user_id;
            Chat_id = chat_id;
            ContentType = contentType;
            Content = content;
            Insert();
        }
        public Message(int iD, int user_id, int chat_id, int contentType, byte[] content)
        {
            ID = iD;
            User_id = user_id;
            Chat_id = chat_id;
            ContentType = contentType;
            Content = content;
        }
        public void Insert()
        {
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                var binaryContent = new System.Data.SqlTypes.SqlBinary(Content);
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "INSERT INTO [Message] VALUES(@user_id, @chat_id, @ContentType, @content)";
                command.Parameters.Add(new SqlParameter("@user_id", User_id));
                command.Parameters.Add(new SqlParameter("@chat_id", Chat_id));
                command.Parameters.Add(new SqlParameter("@ContentType", ContentType));
                command.Parameters.Add(new SqlParameter("@content", binaryContent));
                command.ExecuteNonQuery();
            }
        }
        public void Delete()
        {
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = Connection;
                command.CommandText = "DELETE FROM [Message] WHERE [Id] = @Id";
                command.Parameters.Add(new SqlParameter("@Id", ID));
                command.ExecuteNonQuery();
            }
        }
        public void Update()
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
                command.CommandText = "SELECT * FROM [Message] " +
                "WHERE [Message].[Chat_id] = @Id";
                command.Parameters.Add(new SqlParameter("@Id", chat_id));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Message message = new Message(reader.GetInt32(0),
                                                  reader.GetInt32(1),
                                                  reader.GetInt32(2),
                                                  reader.GetInt32(3),
                                                  (byte[])reader.GetSqlBinary(4));
                    messages.Add(message);
                }
            }
            return messages;
        }
    }
}
