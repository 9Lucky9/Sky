using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky
{
    class Chat
    {
        public int ID;
        private string name;
        public string Name { get => name; set => name = value; }
        public Chat() { }
        public Chat(string Name)
        {
            ID = GetLastChatID() + 1;
            this.Name = Name;
            Insert();
        }
        public Chat(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }

        private void Insert()
        {
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "INSERT INTO [Chat] VALUES(@Name)";
                command.Parameters.Add(new SqlParameter("@Name", Name));
                command.ExecuteNonQuery();
            }
        }
        private int GetLastChatID()
        {
            int lastChatId = 0;
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "SELECT MAX (Id) FROM [Chat]";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lastChatId = reader.GetInt32(0);
                }
            }
            return lastChatId;
        }
    }
}
