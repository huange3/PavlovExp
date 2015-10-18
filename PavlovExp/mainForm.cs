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

                stimLocation1CB.Items.AddRange(coList);
                stimLocation2CB.Items.AddRange(coList);
                stimLocation3CB.Items.AddRange(coList);
                stimLocation4CB.Items.AddRange(coList);
                stimLocation5CB.Items.AddRange(coList);
                stimLocation6CB.Items.AddRange(coList);

                symLocation1CB.Items.AddRange(coList);
                symLocation2CB.Items.AddRange(coList);
                symLocation3CB.Items.AddRange(coList);
                symLocation4CB.Items.AddRange(coList);
                symLocation5CB.Items.AddRange(coList);
                symLocation6CB.Items.AddRange(coList);

                transLocation1CB.Items.AddRange(coList);
                transLocation2CB.Items.AddRange(coList);
                transLocation3CB.Items.AddRange(coList);

                equivLocation1CB.Items.AddRange(coList);
                equivLocation2CB.Items.AddRange(coList);
                equivLocation3CB.Items.AddRange(coList);

                yesLocationCB.Items.AddRange(coList);
                noLocationCB.Items.AddRange(coList);

                stimLocation1CB.SelectedIndex = 0;
                stimLocation2CB.SelectedIndex = 0;
                stimLocation3CB.SelectedIndex = 0;
                stimLocation4CB.SelectedIndex = 0;
                stimLocation5CB.SelectedIndex = 0;
                stimLocation6CB.SelectedIndex = 0;

                symLocation1CB.SelectedIndex = 0;
                symLocation2CB.SelectedIndex = 0;
                symLocation3CB.SelectedIndex = 0;
                symLocation4CB.SelectedIndex = 0;
                symLocation5CB.SelectedIndex = 0;
                symLocation6CB.SelectedIndex = 0;

                transLocation1CB.SelectedIndex = 0;
                transLocation2CB.SelectedIndex = 0;
                transLocation3CB.SelectedIndex = 0;

                equivLocation1CB.SelectedIndex = 0;
                equivLocation2CB.SelectedIndex = 0;
                equivLocation3CB.SelectedIndex = 0;

                // add a Random option for locations of the Yes/No buttons
                co = new ComboObject(6, "Random");
                yesLocationCB.Items.Add(co);
                noLocationCB.Items.Add(co);

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

                simultaneousCB.SelectedIndex = 1;
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
                if (participantIDTB.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter a participant ID before proceeding.");
                    return false;
                }

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
                CurrExp.TrainingTrialsPerPair = (int)stimTrialsPerPairUD.Value;

                // training pair locations
                CurrExp.Location1 = Functions.GetID(stimLocation1CB.SelectedItem);
                CurrExp.Location2 = Functions.GetID(stimLocation2CB.SelectedItem);
                CurrExp.Location3 = Functions.GetID(stimLocation3CB.SelectedItem);
                CurrExp.Location4 = Functions.GetID(stimLocation4CB.SelectedItem);
                CurrExp.Location5 = Functions.GetID(stimLocation5CB.SelectedItem);
                CurrExp.Location6 = Functions.GetID(stimLocation6CB.SelectedItem);

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
                CurrExp.YesLocation = Functions.GetID(yesLocationCB.SelectedItem);
                CurrExp.NoLocation = Functions.GetID(noLocationCB.SelectedItem);
                CurrExp.EvalTrialsPerPair = (int)stimTrialsPerPairEvalUD.Value;
                CurrExp.EvalTrialOrder = Functions.GetID(evalTrialOrderCB.SelectedItem);

                // evaluation pair locations
                CurrExp.SymLocation1 = Functions.GetID(symLocation1CB.SelectedItem);
                CurrExp.SymLocation2 = Functions.GetID(symLocation2CB.SelectedItem);
                CurrExp.SymLocation3 = Functions.GetID(symLocation3CB.SelectedItem);
                CurrExp.SymLocation4 = Functions.GetID(symLocation4CB.SelectedItem);
                CurrExp.SymLocation5 = Functions.GetID(symLocation5CB.SelectedItem);
                CurrExp.SymLocation6 = Functions.GetID(symLocation6CB.SelectedItem);

                CurrExp.TransLocation1 = Functions.GetID(transLocation1CB.SelectedItem);
                CurrExp.TransLocation2 = Functions.GetID(transLocation2CB.SelectedItem);
                CurrExp.TransLocation3 = Functions.GetID(transLocation3CB.SelectedItem);

                CurrExp.EquivLocation1 = Functions.GetID(equivLocation1CB.SelectedItem);
                CurrExp.EquivLocation2 = Functions.GetID(equivLocation2CB.SelectedItem);
                CurrExp.EquivLocation3 = Functions.GetID(equivLocation3CB.SelectedItem);

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
                //currBoard.runTraining();
                //currBoard.runEvaluation();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while starting experiment screen: " + e.Message);
                throw e;
            }
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {
            tabPage3.Focus();
        }
    }
}
