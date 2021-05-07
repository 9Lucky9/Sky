using System.Collections.Generic;
using System.Data.SqlClient;

namespace Sky.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        private Role() { }
        public Role(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
        
        private static List<Role> roles = GetRoles();
        public static List<Role> Roles { get => roles; set => roles = value; }

        private static List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "SELECT * FROM [Role]";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Role role = new Role(reader.GetInt32(0),
                                         reader.GetString(1));
                    roles.Add(role);
                }
            }
            return roles;
        }
    }
}
