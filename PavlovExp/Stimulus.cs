using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlovExp
{
    public class Stimulus
    {
        public string A { get; set; }
        public string B { get; set; }
        public string Type { get; set; }

        public Stimulus(string a, string b, string type)
        {
            this.A = a;
            this.B = b;
            this.Type = type;
        }
    }
}
