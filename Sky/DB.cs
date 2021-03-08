using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sky
{
    public class DB
    {
        static readonly MySqlConnectionStringBuilder ConnectionStringBuilder = new MySqlConnectionStringBuilder()
        {
            Server = "sql11.freesqldatabase.com",
            UserID = "sql11396192",
            Password = "gpjXAF2Nha",
            Database = "sql11396192"
        };
        private MySqlConnection Connection = new MySqlConnection(ConnectionStringBuilder.ToString());
        //User
        public User AuthUser(string login, string password)
        {
            //Column index
            int ID = 0;
            int Login = 1;
            int Email = 2;
            int Password = 3;
            Connection.Open();
            MySqlCommand myCommand = new MySqlCommand($"SELECT * FROM `User` WHERE Login = '{ login }' AND Password = '{password}' ", Connection);
            MySqlDataReader dr = myCommand.ExecuteReader();
            dr.Read();
            User user = new User((int)dr[ID], (string)dr[Login], (string)dr[Email], (string)dr[Password]);
            Connection.Close();
            return user;
        }

        public void InsertUser(string login, string email, string password)
        {
            MySqlCommand myCommand = new MySqlCommand($"INSERT INTO `User` (`ID`, `Login`, `Email`, `Password`) VALUES (NULL, '{login}', '{email}', '{password}')", Connection);
            myCommand.ExecuteReader();
            Connection.Close();
        }
        public User ReturnUserByLogin(string login)
        {
            Connection.Open();
            MySqlCommand myCommand = new MySqlCommand($"SELECT * FROM `User` WHERE Login = '{ login }'", Connection);
            MySqlDataReader dr = myCommand.ExecuteReader();
            if (dr.HasRows == true)
            {
                dr.Read();
                return new User((int)dr[0], (string)dr[1], (string)dr[2], (string)dr[3]);
            }
            Connection.Close();
            return null;
        }

        //Chat
        public int GetLastChatID()
        {
            Connection.Open();
            MySqlCommand myCommand = new MySqlCommand($"SELECT MAX(`ID`) from `Chat`", Connection);
            MySqlDataReader dr = myCommand.ExecuteReader();
            dr.Read();
            Connection.Close();
            return (int)dr[0];
        }
        public void InsertNewChat(string name)
        {
            Connection.Open();
            MySqlCommand myCommand = new MySqlCommand($"INSERT INTO `Chat` (`ID`, `Name`) VALUES (NULL, '{name}')", Connection);
            myCommand.ExecuteReader();
            Connection.Close();
        }

        //Chat_user
        public void InsertNewChat_user(int user_id, int role_id, int chat_id)
        {
            Connection.Open();
            MySqlCommand myCommand = new MySqlCommand($"INSERT INTO `Chat_user` (`ID`, `user_id`, `role_id`, `chat_id`) VALUES (NULL, '{user_id}', '{role_id}', '{chat_id}')", Connection);
            myCommand.ExecuteReader();
            Connection.Close();
        }
    }
}
