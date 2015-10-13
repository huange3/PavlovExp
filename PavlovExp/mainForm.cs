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
    public partial class mainForm : Form
    {
        public Experiment currExperiment;
        public Functions Functions;

        public mainForm()
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
            loadSettings();
        }

        private void loadSettings()
        {
            try
            {
                currExperiment = new Experiment();

                // general settings
                currExperiment.Version = versionTB.Text;
                currExperiment.Date = dateTB.Text;
                currExperiment.ParticipantID = participantIDTB.Text;

                // pretraining settings
                currExperiment.PassCriteria = passCriteriaUD.Value;
                currExperiment.NumTrainingTrials = (int)trainingTrialsUD.Value;
                currExperiment.NumYesTrials = (int)yesTrialsUD.Value;
                currExperiment.NumNoTrials = (int)noTrialsUD.Value;

                // training settings
                currExperiment.TrainingWithinPairDelay = withinPairDelayUD.Value;
                currExperiment.TrainingBetweenPairDelay = betweenPairDelayUD.Value;
                currExperiment.TrainingFirstStimDuration = firstStimDurationUD.Value;
                currExperiment.TrainingSecondStimDuration = secondStimDurationUD.Value;
                currExperiment.TrainingStimLocation = Functions.GetID(stimLocationCB.SelectedItem);
                currExperiment.TrainingStimPresentations = (int)stimTrialsPerPairUD.Value;
                
                if (Functions.GetID(simultaneousCB.SelectedItem) == (int)Constants.Options.Yes)
                {
                    currExperiment.IsSimultaneous = true;
                }
                else
                {
                    currExperiment.IsSimultaneous = false;
                }

                // evaluation settings
                currExperiment.EvalWithinPairDelay = withinPairDelayEvalUD.Value;
                currExperiment.EvalBetweenPairDelay = betweenPairDelayEvalUD.Value;
                currExperiment.EvalFirstStimDuration = firstStimDurationEvalUD.Value;
                currExperiment.EvalSecondStimDuration = secondStimDurationEvalUD.Value;
                currExperiment.EvalStimLocation = Functions.GetID(stimLocationEvalCB.SelectedItem);
                currExperiment.YesLocation = Functions.GetID(yesLocationCB.SelectedItem);
                currExperiment.NoLocation = Functions.GetID(noLocationCB.SelectedItem);
                currExperiment.EvalStimPresentations = (int)stimTrialsPerPairEvalUD.Value;
                currExperiment.EvalTrialOrder = Functions.GetID(evalTrialOrderCB.SelectedItem);

                if (Functions.GetID(symmetryCB.SelectedItem) == (int)Constants.Options.Yes)
                {
                    currExperiment.IsSymmetry = true;
                }
                else
                {
                    currExperiment.IsSymmetry = false;
                }

                if (Functions.GetID(transitivityCB.SelectedItem) == (int)Constants.Options.Yes)
                {
                    currExperiment.IsTransitivity = true;
                }
                else
                {
                    currExperiment.IsTransitivity = false;
                }

                if (Functions.GetID(equivalenceCB.SelectedItem) == (int)Constants.Options.Yes)
                {
                    currExperiment.IsEquivalence = true;
                }
                else
                {
                    currExperiment.IsEquivalence = false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while loading the experiment settings: " + e.Message);
                throw e;
            }
        }
    }
}
