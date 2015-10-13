using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlovExp.Shared
{
    public class Functions
    {
        public int GetID(Object o)
        {
            ComboObject co = (ComboObject)o;
            return co.id;
        }

        public string GetDesc(Object o)
        {
            ComboObject co = (ComboObject)o;
            return co.desc;
        }
    }
}
