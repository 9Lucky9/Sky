using System.Data.SqlClient;
namespace Sky
{
    class Chat
    {
        public int ID;
        public string Name { get; set; }
        public Chat() { }
        /// <summary>
        /// Insert new chat to database
        /// </summary>
        public Chat(string Name)
        {
            ID = GetLastChatID() + 1;
            this.Name = Name;
            Insert();
        }
        /// <summary>
        /// Load chat from database
        /// </summary>
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
