using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlovExp.Shared
{
    public static class Constants
    {
        public static string IntroPretraining = "Thank you for volunteering to participate in this experiment. A purpose of this experiment is to see how well people can detect when things \"go-together\"."
            + "The experiment has several stages. The first stage is to help familiarize you with the procedure. In this first stage you will be presented with common words."
            + "It will be important to watch the screen closely during this first stage. At some points during this stage you will be asked if the two most recent words go together."
            + "To indicate if two words go together you will use the computers mouse to click the \"YES\" button – if the previous two words do not go together then click the \"NO\" button."
            + "\n\nClick the “START” button to begin.";

        public enum Phases
        {
            Pretraining = 1,
            Training = 2,
            Evaluation = 3
        }

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
