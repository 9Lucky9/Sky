using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky
{
    class Chat
    {
        public int? ID;
        public string Name;
        public Chat() { }
        public Chat(int? ID, string Name) 
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}
