using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PavlovExp.Shared
{
    public class Experiment
    {
        // general variables
        public string Version { get; set; }
        public string Date { get; set; }
        public string ParticipantID { get; set; }

        // pretraining variables
        public decimal PassCriteria { get; set; }
        public int NumTrainingTrials { get; set; }
        public int NumYesTrials { get; set; }
        public int NumNoTrials { get; set; }

        // training variables
        public decimal TrainingWithinPairDelay { get; set; }
        public decimal TrainingBetweenPairDelay { get; set; }
        public decimal TrainingFirstStimDuration { get; set; }
        public decimal TrainingSecondStimDuration { get; set; }
        public int TrainingStimLocation { get; set; }
        public int TrainingStimPresentations { get; set; }
        public bool IsSimultaneous { get; set; }

        // evaluation variables
        public decimal EvalWithinPairDelay { get; set; }
        public decimal EvalBetweenPairDelay { get; set; }
        public decimal EvalFirstStimDuration { get; set; }
        public decimal EvalSecondStimDuration { get; set; }
        public int EvalStimLocation { get; set; }
        public int YesLocation { get; set; }
        public int NoLocation { get; set; }
        public int EvalStimPresentations { get; set; }
        public int EvalTrialOrder { get; set; }
        public bool IsSymmetry { get; set; }
        public bool IsTransitivity { get; set; }
        public bool IsEquivalence { get; set; }
    }
}
