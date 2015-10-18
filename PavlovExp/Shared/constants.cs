using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlovExp.Shared
{
    public static class Constants
    {
        public static string IntroPretraining = "Thank you for volunteering to participate in this experiment. A purpose of this experiment is to see how well people can detect when things \"go-together\". "
            + "The experiment has several stages. The first stage is to help familiarize you with the procedure. In this first stage you will be presented with common words. "
            + "It will be important to watch the screen closely during this first stage. At some points during this stage you will be asked if the two most recent words go together. "
            + "To indicate if two words go together you will use the computers mouse to click the \"YES\" button – if the previous two words do not go together then click the \"NO\" button."
            + "\n\nClick the “START” button to begin.";

        public static string IntroPretrainingEval = "Now it is time to see how well you remember the stimuli that go together. "
            + "In this next stage of the experiment you will see pairs of words followed by a chance to indicate whether or not those two words \"go-together\". "
            + "If you had just seen a pair of words that go together indicate this by selecting the \"YES\" button. Use the \"NO\" button for those pairs that you see that are not meant to go together. "
            + "\n\nClick the \"START\" button to begin.";

        public static string IntroTraining = "You are now ready for the next stage of the study. This stage is similar to the previous with one small change – the words you will see are not real words. "
            + "Your task will be to watch and see which nonsense words go together. Following that you will be asked to determine if the pairs of words you see \"go-together\" or not. "
            + "You may or may not be told if you are making the correct selection, however the program is recording your performance and the better you do the sooner the study will be over. "
            + "\n\nClick the \"START\" button when you are ready.";

        public static string IntroEvaluation = "You will now be asked to determine if the pairs of words you see “go-together” or not."
            + "You may or may not be told if you are making the correct selection, however the program is recording your performance and the better you do the sooner the study will be over."
            + "\n\nClick the \"START\" button when you are ready.";

        public enum Phases
        {
            Pretraining = 1,
            PretrainingEval = 2,
            Training = 3,
            Evaluation = 4
        }

        public enum Locations
        {
            Center = 1,
            TopLeft = 2,
            TopRight = 3,
            BottomLeft = 4,
            BottomRight = 5,
            Random = 6
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

        public enum Tests
        {
            Symmetry = 1,
            Transitivity = 2,
            Equivalence = 3
        }
    }
}
