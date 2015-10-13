using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlovExp.Shared
{
    public class ComboObject
    {
        public int id { get; set; }
        public string desc { get; set; }
        public object tag { get; set; }

        public ComboObject(int currID, string currDesc)
        {
            this.id = currID;
            this.desc = currDesc;
        }

        public override string ToString()
        {
            return desc;
        }
    }
}
