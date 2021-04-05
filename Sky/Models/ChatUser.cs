using System;
using System.Collections.Generic;
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
        private static List<ChatUser> chatUsers;
        public static List<ChatUser> ChatUsers { get => chatUsers; set => chatUsers = value; }
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
        private static List<ChatUser> GetChatsByUser(int User_id)
        {
            List<ChatUser> chatUsers = new List<ChatUser>();
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "SELECT * FROM [ChatUser] WHERE User_id = @User_id";
                command.Parameters.Add(new SqlParameter("@User_id", User_id));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ChatUser chatUser = new ChatUser(reader.GetInt32(0),
                                         reader.GetInt32(1),
                                         reader.GetInt32(2),
                                         reader.GetInt32(3));
                    chatUsers.Add(chatUser);
                }
            }
            return chatUsers;
        }
    }
}
