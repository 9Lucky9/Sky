using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Role(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}
