using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sky.Models
{
    class Role
    {
        private readonly int ID;
        private readonly string Name;

        public Role(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
}
