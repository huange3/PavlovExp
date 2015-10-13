using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlovExp.Shared
{
    public static class Constants
    {
        public enum Locations
        {
            Center = 1,
            TopLeft = 2,
            TopRight = 3,
            BottomLeft = 4,
            BottomRight = 5
        }

        public enum Options
        {
            Yes = 1,
            No = 2
        }

        public enum Order
        {
            Sequential = 1,
            Mixed = 2
        }
    }
}
