using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky.Models
{
    class ContentType
    {
        public enum ContentTypes
        {
            Text = 1,
            VoiceMessage = 2,
            Picture = 3,
            Video = 4
        };
        public int Id;
        public string Name;

        public ContentType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        private static List<ContentType> _contentTypes = GetContentTypes();
        public static List<ContentType> ContentTypes1 { get => _contentTypes; set => _contentTypes = value; }

        private static List<ContentType> GetContentTypes()
        {
            List<ContentType> contentTypes = new List<ContentType>();
            using (SqlConnection Connection = new SqlConnection(Properties.Settings.Default.SkyDatabaseConnectionString))
            {
                Connection.Open();
                SqlCommand command = new SqlCommand()
                {
                    Connection = Connection
                };
                command.CommandText = "SELECT * FROM [ContentType]";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ContentType contentType = new ContentType(reader.GetInt32(0),
                                         reader.GetString(1));
                    contentTypes.Add(contentType);
                }
            }
            return contentTypes;
        }
        private void ContentTypeToEnum()
        {

        }
    }
}
