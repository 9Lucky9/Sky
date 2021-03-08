using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky.Models
{
    public class ChatUser
    {
        public int? ID { get; set; }
        public int user_id { get; set; }
        public int role_id { get; set; }
        public int chat_id { get; set; }

        public ChatUser() { }
        public ChatUser(int? ID, int user_id, int role_id, int chat_id)
        {
            this.ID = ID;
            this.user_id = user_id;
            this.role_id = role_id;
            this.chat_id = chat_id;
        }
    }
}
