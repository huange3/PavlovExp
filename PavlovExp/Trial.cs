using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlovExp
{
    public class Trial
    {
        public int Phase { get; set; }
        public Stimulus StimPair {get; set;}
        public decimal Latency { get; set; }
        public int UserAnswer { get; set; }
        public int IsCorrect { get; set; }
        public int Test { get; set; }

        public Trial(int p)
        {
            this.Phase = p;
        }
    }
}
