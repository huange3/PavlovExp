using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PavlovExp.Shared;

namespace PavlovExp
{
    public partial class MainForm : Form
    {
        public Experiment CurrExp;
        public Functions Functions = new Functions();

        public MainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            loadAllowableValues();
        }

        private void loadAllowableValues()
        {
            ComboObject co;
            ComboObject[] coList;

            try
            {
                // location comboboxes
                coList = new ComboObject[5];

                coList[0] = new ComboObject(1, "Center");
                coList[1] = new ComboObject(2, "Top Left");
                coList[2] = new ComboObject(3, "Top Right");
                coList[3] = new ComboObject(4, "Bottom Left");
                coList[4] = new ComboObject(5, "Bottom Right");

                stimLocationCB.Items.AddRange(coList);
                stimLocationEvalCB.Items.AddRange(coList);

                yesLocationCB.Items.AddRange(coList);
                noLocationCB.Items.AddRange(coList);

                // add a Random option for locations of the Yes/No buttons
                co = new ComboObject(6, "Random");
                yesLocationCB.Items.Add(co);
                noLocationCB.Items.Add(co);

                stimLocationCB.SelectedIndex = 0;
                stimLocationEvalCB.SelectedIndex = 0;
                yesLocationCB.SelectedIndex = 3;
                noLocationCB.SelectedIndex = 4;

                // yes/no comboboxes
                coList = new ComboObject[2];

                coList[0] = new ComboObject(1, "Yes");
                coList[1] = new ComboObject(2, "No");

                simultaneousCB.Items.AddRange(coList);
                symmetryCB.Items.AddRange(coList);
                transitivityCB.Items.AddRange(coList);
                equivalenceCB.Items.AddRange(coList);

                simultaneousCB.SelectedIndex = 0;
                symmetryCB.SelectedIndex = 0;
                transitivityCB.SelectedIndex = 0;
                equivalenceCB.SelectedIndex = 0;

                // trial order combobox
                co = new ComboObject(1, "Sequential");
                evalTrialOrderCB.Items.Add(co);

                co = new ComboObject(2, "Mixed");
                evalTrialOrderCB.Items.Add(co);

                evalTrialOrderCB.SelectedIndex = 0;

                // general fields
                dateTB.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while loading allowable values: " + e.Message);
                throw e;
            }
            finally
            {
                co = null;
                coList = null;
            }
        }

        private void startExpBtn_Click(object sender, EventArgs e)
        {
            if (loadSettings())
            {
                startExperiment();
            }
        }

        private bool loadSettings()
        {
            try
            {
                CurrExp = new Experiment();

                // general settings
                CurrExp.Version = versionTB.Text;
                CurrExp.Date = dateTB.Text;
                CurrExp.ParticipantID = participantIDTB.Text;

                // pretraining settings
                CurrExp.PassCriteria = passCriteriaUD.Value;
                CurrExp.TrialsPerPair = (int)stimTrialsPerPairPretrainUD.Value;
                CurrExp.YesTrialsPerPair = (int)yesTrialsPerPairUD.Value;
                CurrExp.NoTrialsPerPair = (int)noTrialsPerPairUD.Value;

                // training settings
                CurrExp.TrainingWithinPairDelay = withinPairDelayUD.Value;
                CurrExp.TrainingBetweenPairDelay = betweenPairDelayUD.Value;
                CurrExp.TrainingFirstStimDuration = firstStimDurationUD.Value;
                CurrExp.TrainingSecondStimDuration = secondStimDurationUD.Value;
                CurrExp.TrainingStimLocation = Functions.GetID(stimLocationCB.SelectedItem);
                CurrExp.TrainingStimPresentations = (int)stimTrialsPerPairUD.Value;
                
                if (Functions.GetID(simultaneousCB.SelectedItem) == (int)Constants.Options.Yes)
                {
                    CurrExp.IsSimultaneous = true;
                }
                else
                {
                    CurrExp.IsSimultaneous = false;
                }

                // evaluation settings
                CurrExp.EvalWithinPairDelay = withinPairDelayEvalUD.Value;
                CurrExp.EvalBetweenPairDelay = betweenPairDelayEvalUD.Value;
                CurrExp.EvalFirstStimDuration = firstStimDurationEvalUD.Value;
                CurrExp.EvalSecondStimDuration = secondStimDurationEvalUD.Value;
                CurrExp.EvalStimLocation = Functions.GetID(stimLocationEvalCB.SelectedItem);
                CurrExp.YesLocation = Functions.GetID(yesLocationCB.SelectedItem);
                CurrExp.NoLocation = Functions.GetID(noLocationCB.SelectedItem);
                CurrExp.EvalStimPresentations = (int)stimTrialsPerPairEvalUD.Value;
                CurrExp.EvalTrialOrder = Functions.GetID(evalTrialOrderCB.SelectedItem);

                if (Functions.GetID(symmetryCB.SelectedItem) == (int)Constants.Options.Yes)
                {
                    CurrExp.IsSymmetry = true;
                }
                else
                {
                    CurrExp.IsSymmetry = false;
                }

                if (Functions.GetID(transitivityCB.SelectedItem) == (int)Constants.Options.Yes)
                {
                    CurrExp.IsTransitivity = true;
                }
                else
                {
                    CurrExp.IsTransitivity = false;
                }

                if (Functions.GetID(equivalenceCB.SelectedItem) == (int)Constants.Options.Yes)
                {
                    CurrExp.IsEquivalence = true;
                }
                else
                {
                    CurrExp.IsEquivalence = false;
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while loading the experiment settings: " + e.Message);
                return false;
                throw e;              
            }
        }

        private void startExperiment()
        {
            MainBoard currBoard;

            try
            {
                currBoard = new MainBoard();
                currBoard.Show();
                currBoard.CurrExp = CurrExp;
                currBoard.runPreTraining();
                //currBoard.runPreTrainingEval();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while starting experiment screen: " + e.Message);
                throw e;
            }
        }
    }
}
