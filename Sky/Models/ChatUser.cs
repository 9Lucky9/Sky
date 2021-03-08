using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky.Models
{
    class ChatUser
    {
        public int? ID;
        public int user_id;
        public int role_id;
        public int chat_id;

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
