using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlovExp
{
    public class Experiment
    {
        // general variables
        public string Version { get; set; }
        public string Date { get; set; }
        public string ParticipantID { get; set; }

        // pretraining variables
        public decimal PassCriteria { get; set; }
        public int TrialsPerPair { get; set; }
        public int YesTrialsPerPair { get; set; }
        public int NoTrialsPerPair { get; set; }

        // training variables
        public decimal TrainingWithinPairDelay { get; set; }
        public decimal TrainingBetweenPairDelay { get; set; }
        public decimal TrainingFirstStimDuration { get; set; }
        public decimal TrainingSecondStimDuration { get; set; }
        public int TrainingTrialsPerPair { get; set; }
        public bool IsSimultaneous { get; set; }

        public int Location1 { get; set; }
        public int Location2 { get; set; }
        public int Location3 { get; set; }
        public int Location4 { get; set; }
        public int Location5 { get; set; }
        public int Location6 { get; set; }

        // evaluation variables
        public decimal EvalWithinPairDelay { get; set; }
        public decimal EvalBetweenPairDelay { get; set; }
        public decimal EvalFirstStimDuration { get; set; }
        public decimal EvalSecondStimDuration { get; set; }
        public int YesLocation { get; set; }
        public int NoLocation { get; set; }
        public int EvalTrialsPerPair { get; set; }
        public int EvalTrialOrder { get; set; }
        public bool IsSymmetry { get; set; }
        public bool IsTransitivity { get; set; }
        public bool IsEquivalence { get; set; }

        public int SymLocation1 { get; set; }
        public int SymLocation2 { get; set; }
        public int SymLocation3 { get; set; }
        public int SymLocation4 { get; set; }
        public int SymLocation5 { get; set; }
        public int SymLocation6 { get; set; }

        public int TransLocation1 { get; set; }
        public int TransLocation2 { get; set; }
        public int TransLocation3 { get; set; }

        public int EquivLocation1 { get; set; }
        public int EquivLocation2 { get; set; }
        public int EquivLocation3 { get; set; }
    }
}
