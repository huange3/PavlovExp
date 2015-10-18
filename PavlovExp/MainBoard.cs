using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PavlovExp.Shared;
using System.IO;

namespace PavlovExp
{
    public partial class MainBoard : Form
    {
        public int CurrPhase;
        public Experiment CurrExp;
        public List<Stimulus> StimList;
        public Queue<Stimulus> StimQueue;
        public Stimulus CurrStim;
        public Trial CurrTrial;
        public Stopwatch LatencyTimer = new Stopwatch();

        public List<Trial> PretrainingTrials = new List<Trial>();
        public List<Trial> PretrainingEvalTrials = new List<Trial>();
        public List<Trial> TrainingTrials = new List<Trial>();
        public List<Trial> EvaluationTrials = new List<Trial>();
        public Queue<int> TestQueue;
        public int CurrTest;
        public Queue<Stimulus> SymmetryQueue;
        public Queue<Stimulus> TransitivityQueue;
        public Queue<Stimulus> EquivalenceQueue;

        public int CorrectCount = 0;
        public int TotalTrials = 0;

        Random rnd = new Random();
        public Point Center;
        public Point TopLeft;
        public Point TopRight;
        public Point BottomLeft;
        public Point BottomRight;

        public int ScreenWidth = Screen.PrimaryScreen.Bounds.Width;
        public int ScreenHeight = Screen.PrimaryScreen.Bounds.Height;

        public MainBoard()
        {
            InitializeComponent();

            // set our partition locations
            Center = new Point(ScreenWidth / 2, ScreenHeight / 2);
            TopLeft = new Point((int)Math.Floor(ScreenWidth * 0.25), (int)Math.Floor(ScreenHeight * 0.25));
            TopRight = new Point((int)Math.Floor(ScreenWidth * 0.75), (int)Math.Floor(ScreenHeight * 0.25));
            BottomLeft = new Point((int)Math.Floor(ScreenWidth * 0.25), (int)Math.Floor(ScreenHeight * 0.75));
            BottomRight = new Point((int)Math.Floor(ScreenWidth * 0.75), (int)Math.Floor(ScreenHeight * 0.75));
        }

        private void MainBoard_Load(object sender, EventArgs e)
        {
            // make this fullscreen
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            correctLB.Location = new Point(Center.X - correctLB.Width / 2, Center.Y);
            incorrectLB.Location = new Point(Center.X - incorrectLB.Width / 2, Center.Y);
            nextBtn.Location = new Point(Center.X - nextBtn.Width / 2, Center.Y);
        }

        public void runPreTraining()
        {
            try
            {
               if (setupPreTraining())
                {
                    mainPanel.BackColor = Color.White;
                    introPanel.Visible = true;
                    introLB.Left = this.Width / 2 - introLB.Width / 2;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while running experiment phases: " + e.Message);
                throw e;
            }
        }

        public void runPreTrainingEval()
        {
            try
            {
                if (setupPreTrainingEval())
                {
                    mainPanel.BackColor = Color.White;
                    introPanel.Visible = true;
                    introLB.Left = this.Width / 2 - introLB.Width / 2;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while running experiment phases: " + e.Message);
                throw e;
            }
        }

        public void runTrial()
        {
            Point currPoint;

            try
            {
                // PRETRAINING PHASE
                if (CurrPhase == (int)Constants.Phases.Pretraining)
                {
                    if (StimQueue.Count == 0)
                    {
                        runPreTrainingEval();
                    }
                    else
                    {
                        CurrTrial = new Trial(CurrPhase);
                        CurrStim = StimQueue.Dequeue();
                        CurrTrial.StimPair = CurrStim;

                        labelA.Text = CurrStim.A;
                        labelB.Text = CurrStim.B;

                        // our locations
                        labelA.Location = new Point(Center.X - labelA.Width / 2, Center.Y);
                        labelB.Location = new Point(Center.X - labelB.Width / 2, Center.Y);

                        mainPanel.BackColor = Color.White;
                        labelA.Visible = true;
                        labelB.Visible = false;
                        firstStimTimer.Start();
                    }
                }
                // PRETRAINING EVALUATION PHASE
                else if (CurrPhase == (int)Constants.Phases.PretrainingEval)
                {
                    if (StimQueue.Count == 0)
                    {
                        // check if we've reached the expected accuracy
                        // otherwise, continue with the trials
                        if (checkAccuracy())
                        {
                            runTraining();
                        }
                        else
                        {
                            // start over!
                            setupPreTrainingEval();
                            runTrial();
                        }
                    }
                    else
                    {
                        CurrTrial = new Trial(CurrPhase);
                        CurrStim = StimQueue.Dequeue();
                        CurrTrial.StimPair = CurrStim;

                        labelA.Text = CurrStim.A;
                        labelB.Text = CurrStim.B;

                        // our locations
                        labelA.Location = new Point(Center.X - labelA.Width / 2, Center.Y);
                        labelB.Location = new Point(Center.X - labelB.Width / 2, Center.Y);

                        setButtonLocations();

                        mainPanel.BackColor = Color.White;
                        labelA.Visible = true;
                        labelB.Visible = false;
                        firstStimTimer.Start();
                    }
                }
                // TRAINING PHASE
                else if (CurrPhase == (int)Constants.Phases.Training)
                {
                    if (StimQueue.Count == 0)
                    {
                        runEvaluation();
                    }
                    else
                    {
                        CurrTrial = new Trial(CurrPhase);
                        CurrStim = StimQueue.Dequeue();
                        CurrTrial.StimPair = CurrStim;

                        labelA.Text = CurrStim.A;
                        labelB.Text = CurrStim.B;

                        // our locations
                        currPoint = findLocation(CurrStim.Location);

                        labelA.Location = new Point(currPoint.X - labelA.Width / 2, currPoint.Y);
                        labelB.Location = new Point(currPoint.X - labelB.Width / 2, currPoint.Y);

                        mainPanel.BackColor = Color.White;
                        labelA.Visible = true;
                        labelB.Visible = false;
                        firstStimTimer.Start();
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while running trial: " + e.Message);
                throw e;
            }
        }

        private void runEvaluationTrial()
        {
            Point currPoint;

            try
            {
                /* Scenarios:
                    1. We're at the end of the experiment.
                        - StimQueue = 0
                        - TestQueue = 0
                    2. We've finished a test, proceed to next one.
                        - StimQueue = 0
                        - TestQueue > 0
                    3. We still have stimuli pairs to go through.
                        - StimQueue > 0
                */

                if (StimQueue.Count == 0 && TestQueue.Count == 0)
                {
                    MessageBox.Show("Experiment completed! Thank you for participating!");
                    outputData();
                    this.Close();
                    return;
                }
                
                if (StimQueue.Count == 0)
                {
                    CurrTest = TestQueue.Dequeue();

                    findTestQueue(CurrTest);

                    CurrTrial = new Trial(CurrPhase);
                    CurrStim = StimQueue.Dequeue();
                    CurrTrial.StimPair = CurrStim;
                    CurrTrial.Test = CurrTest;

                    labelA.Text = CurrStim.A;
                    labelB.Text = CurrStim.B;

                    // our locations
                    currPoint = findLocation(CurrStim.Location);

                    labelA.Location = new Point(currPoint.X - labelA.Width / 2, currPoint.Y);
                    labelB.Location = new Point(currPoint.X - labelB.Width / 2, currPoint.Y);

                    setButtonLocations();

                    mainPanel.BackColor = Color.White;
                    labelA.Visible = true;
                    labelB.Visible = false;
                    firstStimTimer.Start();
                }
                else
                {
                    CurrTrial = new Trial(CurrPhase);
                    CurrStim = StimQueue.Dequeue();
                    CurrTrial.StimPair = CurrStim;
                    CurrTrial.Test = CurrTest;

                    labelA.Text = CurrStim.A;
                    labelB.Text = CurrStim.B;

                    // our locations
                    currPoint = findLocation(CurrStim.Location);

                    labelA.Location = new Point(currPoint.X - labelA.Width / 2, currPoint.Y);
                    labelB.Location = new Point(currPoint.X - labelB.Width / 2, currPoint.Y);

                    setButtonLocations();

                    mainPanel.BackColor = Color.White;
                    labelA.Visible = true;
                    labelB.Visible = false;
                    firstStimTimer.Start();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while running evaluation trials: " + e.Message);
                throw e;
            }
        }

        public void runTraining()
        {
            try
            {
                if (setupTraining())
                {
                    mainPanel.BackColor = Color.White;
                    introPanel.Visible = true;
                    introLB.Left = this.Width / 2 - introLB.Width / 2;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while running experiment phases: " + e.Message);
                throw e;
            }
        }

        public void runEvaluation()
        {
            try
            {
                if (setupEvaluation())
                {
                    mainPanel.BackColor = Color.White;
                    introPanel.Visible = true;
                    introLB.Left = this.Width / 2 - introLB.Width / 2;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while running experiment phases: " + e.Message);
                throw e;
            }
        }

        private bool setupPreTraining()
        {
            try
            {
                CurrPhase = (int)Constants.Phases.Pretraining;

                // initialize our stimulus list
                StimList = new List<Stimulus>();

                // populate our stimulus list, shuffle, then add to a queue
                for (var i = 0; i < CurrExp.TrialsPerPair; i++)
                {
                    StimList.Add(new Stimulus("Red", "Color", 1));
                    StimList.Add(new Stimulus("Dog", "Bone", 1));
                    StimList.Add(new Stimulus("Cat", "Mouse", 1));
                    StimList.Add(new Stimulus("Peanut Butter", "Jelly", 1));
                    StimList.Add(new Stimulus("Peas", "Carrots", 1));
                    StimList.Add(new Stimulus("Ketchup", "Mustard", 1));
                    StimList.Add(new Stimulus("Cow", "Farm", 1));
                }

                StimList.Shuffle();

                StimQueue = new Queue<Stimulus>(StimList);

                // set up our timers
                // default for pretraining is 1000 ms for stimuli presentation
                // 500 ms for within pair delay
                // 3000 ms for between pair delay
                firstStimTimer.Interval = 1000;
                secondStimTimer.Interval = 1000;
                withinPairTimer.Interval = 500;
                betweenPairTimer.Interval = 3000;

                //for testing
                //firstStimTimer.Interval = 100;
                //secondStimTimer.Interval = 100;
                //withinPairTimer.Interval = 100;
                //betweenPairTimer.Interval = 100;

                introLB.Text = Constants.IntroPretraining;

                labelA.Visible = false;
                labelB.Visible = false;
                nextBtn.Visible = false;
                yesBtn.Visible = false;
                noBtn.Visible = false;

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while setting up pretraining phase: " + e.Message);
                return false;
                throw e;
            }
        }

        private bool setupPreTrainingEval()
        {
            try
            {
                CurrPhase = (int)Constants.Phases.PretrainingEval;

                // initialize our stimulus list
                StimList = new List<Stimulus>();

                // populate our stimulus list, shuffle, then add to a queue
                for (var i = 0; i < CurrExp.YesTrialsPerPair; i++)
                {
                    // YES types
                    StimList.Add(new Stimulus("Red", "Color", 1));
                    StimList.Add(new Stimulus("Dog", "Bone", 1));
                    StimList.Add(new Stimulus("Cat", "Mouse", 1));
                    StimList.Add(new Stimulus("Peanut Butter", "Jelly", 1));
                    StimList.Add(new Stimulus("Peas", "Carrots", 1));
                    StimList.Add(new Stimulus("Ketchup", "Mustard", 1));
                    StimList.Add(new Stimulus("Cow", "Farm", 1));                 
                }

                for (var i = 0; i < CurrExp.NoTrialsPerPair; i++)
                {
                    // NO types
                    StimList.Add(new Stimulus("Soap", "Exit", 0));
                    StimList.Add(new Stimulus("Right", "Cloud", 0));
                    StimList.Add(new Stimulus("Blue", "Camel", 0));
                    StimList.Add(new Stimulus("Button", "Tomato", 0));
                    StimList.Add(new Stimulus("Candy", "Different", 0));
                    StimList.Add(new Stimulus("Eggs", "Shoe", 0));
                    StimList.Add(new Stimulus("Flower", "Black", 0));
                }

                TotalTrials = StimList.Count;
                CorrectCount = 0;

                StimList.Shuffle();

                StimQueue = new Queue<Stimulus>(StimList);

                // set up our timers
                // default for pretraining eval is 1000 ms for stimuli presentation
                // 1000 ms for within pair delay
                // 3000 ms for between pair delay
                firstStimTimer.Interval = 1000;
                secondStimTimer.Interval = 1000;
                withinPairTimer.Interval = 1000;
                betweenPairTimer.Interval = 3000;

                introLB.Text = Constants.IntroPretrainingEval;

                labelA.Visible = false;
                labelB.Visible = false;
                nextBtn.Visible = false;
                yesBtn.Visible = false;
                noBtn.Visible = false;
                correctLB.Visible = false;
                incorrectLB.Visible = false;

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while setting up pretraining phase: " + e.Message);
                return false;
                throw e;
            }
        }

        private bool setupTraining()
        {
            try
            {
                CurrPhase = (int)Constants.Phases.Training;

                // initialize our stimulus list
                StimList = new List<Stimulus>();

                // populate our stimulus list, shuffle, then add to a queue
                for (var i = 0; i < CurrExp.TrainingTrialsPerPair; i++)
                {
                    // YES types
                    StimList.Add(new Stimulus("CUZ", "PIP", 0, CurrExp.Location1));
                    StimList.Add(new Stimulus("PIP", "FIP", 0, CurrExp.Location2));
                    StimList.Add(new Stimulus("ZAC", "DUZ", 0, CurrExp.Location3));
                    StimList.Add(new Stimulus("DUZ", "VAM", 0, CurrExp.Location4));
                    StimList.Add(new Stimulus("ZID", "JOM", 0, CurrExp.Location5));
                    StimList.Add(new Stimulus("JOM", "XAD", 0, CurrExp.Location6));
                }

                StimList.Shuffle();

                StimQueue = new Queue<Stimulus>(StimList);

                // set up our timers
                firstStimTimer.Interval = (int)(CurrExp.TrainingFirstStimDuration * 1000);
                secondStimTimer.Interval = (int)(CurrExp.TrainingSecondStimDuration * 1000);
                withinPairTimer.Interval = (int)(CurrExp.TrainingWithinPairDelay * 1000);
                betweenPairTimer.Interval = (int)(CurrExp.TrainingBetweenPairDelay * 1000);

                introLB.Text = Constants.IntroTraining;

                labelA.Visible = false;
                labelB.Visible = false;
                nextBtn.Visible = false;
                yesBtn.Visible = false;
                noBtn.Visible = false;
                correctLB.Visible = false;
                incorrectLB.Visible = false;

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while setting up pretraining phase: " + e.Message);
                return false;
                throw e;
            }
        }

        private bool setupEvaluation()
        {
            List<int> tempList;

            try
            {
                CurrPhase = (int)Constants.Phases.Evaluation;

                // check which tests we're going to do
                tempList = new List<int>();

                if (CurrExp.IsSymmetry) tempList.Add((int)Constants.Tests.Symmetry);

                if (CurrExp.IsTransitivity) tempList.Add((int)Constants.Tests.Transitivity);

                if (CurrExp.IsEquivalence) tempList.Add((int)Constants.Tests.Equivalence);

                if (CurrExp.EvalTrialOrder == 2) tempList.Shuffle();

                TestQueue = new Queue<int>(tempList);

                // initialize our stimulus list
                StimList = new List<Stimulus>();

                // SYMMETRY QUEUE             
                for (var i = 0; i < CurrExp.EvalTrialsPerPair; i++)
                {
                    StimList.Add(new Stimulus("PIP", "CUZ", 1, CurrExp.SymLocation1));
                    StimList.Add(new Stimulus("FIP", "PIP", 1, CurrExp.SymLocation2));
                    StimList.Add(new Stimulus("DUZ", "ZAC", 1, CurrExp.SymLocation3));
                    StimList.Add(new Stimulus("VAM", "DUZ", 1, CurrExp.SymLocation4));
                    StimList.Add(new Stimulus("JOM", "ZID", 1, CurrExp.SymLocation5));
                    StimList.Add(new Stimulus("XAD", "JOM", 1, CurrExp.SymLocation6));
                }

                StimList.Shuffle();

                SymmetryQueue = new Queue<Stimulus>(StimList);

                // TRANSITIVITY QUEUE 
                StimList.Clear();

                for (var i = 0; i < CurrExp.EvalTrialsPerPair; i++)
                {
                    StimList.Add(new Stimulus("CUZ", "FIP", 1, CurrExp.TransLocation1));
                    StimList.Add(new Stimulus("ZAC", "VAM", 1, CurrExp.TransLocation2));
                    StimList.Add(new Stimulus("ZID", "ZAD", 1, CurrExp.TransLocation3));
                }

                StimList.Shuffle();

                TransitivityQueue = new Queue<Stimulus>(StimList);

                // EQUIVALENCE QUEUE 
                StimList.Clear();

                for (var i = 0; i < CurrExp.EvalTrialsPerPair; i++)
                {
                    StimList.Add(new Stimulus("FIP", "CUZ", 1, CurrExp.EquivLocation1));
                    StimList.Add(new Stimulus("VAM", "ZAC", 1, CurrExp.EquivLocation2));
                    StimList.Add(new Stimulus("ZAD", "ZID", 1, CurrExp.EquivLocation3));
                }

                StimList.Shuffle();

                EquivalenceQueue = new Queue<Stimulus>(StimList);

                // get our current queues set up!
                CurrTest = TestQueue.Dequeue();

                findTestQueue(CurrTest);

                // set up our timers
                firstStimTimer.Interval = (int)(CurrExp.EvalFirstStimDuration * 1000);
                secondStimTimer.Interval = (int)(CurrExp.EvalSecondStimDuration * 1000);
                withinPairTimer.Interval = (int)(CurrExp.EvalWithinPairDelay * 1000);
                betweenPairTimer.Interval = (int)(CurrExp.EvalBetweenPairDelay * 1000);

                introLB.Text = Constants.IntroEvaluation;

                labelA.Visible = false;
                labelB.Visible = false;
                nextBtn.Visible = false;
                yesBtn.Visible = false;
                noBtn.Visible = false;
                correctLB.Visible = false;
                incorrectLB.Visible = false;

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while setting up pretraining phase: " + e.Message);
                return false;
                throw e;
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            introPanel.Visible = false;

            if (CurrPhase == (int)Constants.Phases.Evaluation) runEvaluationTrial();
            else runTrial();
        }

        private void firstStimTimer_Tick(object sender, EventArgs e)
        {
            firstTimerTick();
        }

        private void withinPairTimer_Tick(object sender, EventArgs e)
        {
            withinTimerTick();
        }

        private void secondStimTimer_Tick(object sender, EventArgs e)
        {
            secondTimerTick();
        }

        private void betweenPairTimer_Tick(object sender, EventArgs e)
        {
            betweenTimerTick();
        }

        private void firstTimerTick()
        {
            labelA.Visible = false;
            firstStimTimer.Stop();
            withinPairTimer.Start();
        }

        private void secondTimerTick()
        {
            secondStimTimer.Stop();
            labelB.Visible = false;
            mainPanel.BackColor = Color.Black;
            betweenPairTimer.Start();
        }

        private void withinTimerTick()
        {
            withinPairTimer.Stop();

            // the training phase has the option for simulataneous presentation
            // show both stimuli together
            if (CurrPhase == (int)Constants.Phases.Training)
            {
                if (CurrExp.IsSimultaneous)
                {
                    labelB.Text = CurrStim.A + "   " + CurrStim.B;
                    labelB.Location = new Point(labelB.Location.X - 80, labelB.Location.Y);
                }
            }

            labelB.Visible = true;
            secondStimTimer.Start();
        }

        private void betweenTimerTick()
        {
            betweenPairTimer.Stop();

            if (CurrPhase == (int)Constants.Phases.Pretraining)
            {
                nextBtn.Visible = true;
            }
            else if (CurrPhase == (int)Constants.Phases.PretrainingEval || CurrPhase == (int)Constants.Phases.Evaluation)
            {
                nextBtn.Visible = false;
                yesBtn.Visible = true;
                noBtn.Visible = true;
            }
            else if (CurrPhase == (int)Constants.Phases.Training)
            {
               nextBtn.Visible = true;
            }

            LatencyTimer.Start();
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            nextBtn.Visible = false;

            // Latency Timer is already stopped in the Yes/No trials
            if (CurrPhase == (int)Constants.Phases.Pretraining) LatencyTimer.Stop();

            logTrial();
            LatencyTimer.Reset();

            // different trial methodology between the evalulation phase and the rest
            if (CurrPhase == (int)Constants.Phases.Evaluation) runEvaluationTrial();
            else runTrial();
        }

        private void logTrial()
        {
            try
            {
                CurrTrial.Latency = (decimal)LatencyTimer.ElapsedMilliseconds / 1000;

                if (CurrPhase == (int)Constants.Phases.Pretraining)
                {
                    PretrainingTrials.Add(CurrTrial);
                }
                
                if (CurrPhase == (int)Constants.Phases.PretrainingEval)
                {
                    PretrainingEvalTrials.Add(CurrTrial);
                }  
                
                if (CurrPhase == (int)Constants.Phases.Training)
                {
                    TrainingTrials.Add(CurrTrial);
                }

                if (CurrPhase == (int)Constants.Phases.Evaluation)
                {
                    EvaluationTrials.Add(CurrTrial);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while saving trial info: " + e.Message);
                throw;
            }
        }

        private void MainBoard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void MainBoard_FormClosed(object sender, FormClosedEventArgs e)
        {
            stopTimers();
        }

        private void stopTimers()
        {
            firstStimTimer.Stop();
            secondStimTimer.Stop();
            withinPairTimer.Stop();
            betweenPairTimer.Stop();
            LatencyTimer.Stop();
        }

        private void yesBtn_Click(object sender, EventArgs e)
        {
            LatencyTimer.Stop();
            yesBtn.Visible = false;
            noBtn.Visible = false;

            CurrTrial.UserAnswer = 1;

            if (checkAnswer()) correctLB.Visible = true;
            else incorrectLB.Visible = true;

            rewardTimer.Start();
        }

        private void noBtn_Click(object sender, EventArgs e)
        {
            LatencyTimer.Stop();
            yesBtn.Visible = false;
            noBtn.Visible = false;

            CurrTrial.UserAnswer = 0;

            if (checkAnswer()) correctLB.Visible = true;
            else incorrectLB.Visible = true;

            rewardTimer.Start();
        }

        private bool checkAnswer()
        {
            int currType = 0;

            try
            {
                currType = CurrStim.Type;

                if (CurrTrial.UserAnswer == currType)
                {
                    CurrTrial.IsCorrect = 1;
                    CorrectCount++;
                    return true;
                }
                else CurrTrial.IsCorrect = 0;

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while checking answer: " + e.Message);
                return false;
                throw e;
            }
        }

        private bool checkAccuracy()
        {
            int correctNeeded = 0;

            try
            {
                correctNeeded = (int)Math.Round(CurrExp.PassCriteria * TotalTrials, MidpointRounding.AwayFromZero);

                if (CorrectCount >= correctNeeded) return true;

                return false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while checking accuracy: " + e.Message);
                return false;
                throw;
            }
        }

        private void rewardTimer_Tick(object sender, EventArgs e)
        {
            rewardTimerTick();
        }

        private void rewardTimerTick()
        {
            rewardTimer.Stop();
            correctLB.Visible = false;
            incorrectLB.Visible = false;
            nextBtn.Visible = true;
        }

        private Point findLocation(int id)
        {
            switch (id)
            {
                case (int)Constants.Locations.Center:
                    return Center;
                case (int)Constants.Locations.TopLeft:
                    return TopLeft;
                case (int)Constants.Locations.TopRight:
                    return TopRight;
                case (int)Constants.Locations.BottomLeft:
                    return BottomLeft;
                case (int)Constants.Locations.BottomRight:
                    return BottomRight;             
            }

            return new Point(0, 0);
        }

        private void findTestQueue(int id)
        {
            if (id == (int)Constants.Tests.Symmetry) StimQueue = SymmetryQueue;

            if (id == (int)Constants.Tests.Transitivity) StimQueue = TransitivityQueue;

            if (id == (int)Constants.Tests.Equivalence) StimQueue = EquivalenceQueue;
        }

        private void setButtonLocations()
        {
            int randNum = 0;
            Point currPoint;          

            if (CurrPhase == (int)Constants.Phases.Evaluation)
            {
                // randomize the location of the yes/no buttons between left and right partitions
                if (CurrExp.YesLocation == (int)Constants.Locations.Random || CurrExp.NoLocation == (int)Constants.Locations.Random)
                {
                    randNum = rnd.Next(1, 20);

                    if (randNum % 2 == 0)
                    {
                        yesBtn.Location = new Point(BottomLeft.X - yesBtn.Width / 2, BottomLeft.Y);
                        noBtn.Location = new Point(BottomRight.X - noBtn.Width / 2, BottomRight.Y);
                    }
                    else
                    {
                        yesBtn.Location = new Point(BottomRight.X - yesBtn.Width / 2, BottomRight.Y);
                        noBtn.Location = new Point(BottomLeft.X - noBtn.Width / 2, BottomLeft.Y);
                    }
                }
                else
                {
                    currPoint = findLocation(CurrExp.YesLocation);
                    yesBtn.Location = new Point(currPoint.X - yesBtn.Width / 2, currPoint.Y);

                    currPoint = findLocation(CurrExp.NoLocation);
                    noBtn.Location = new Point(currPoint.X - noBtn.Width / 2, currPoint.Y);
                }
            } else // place the yes/no buttons in their default locations
            {
                yesBtn.Location = new Point(BottomLeft.X - yesBtn.Width / 2, BottomLeft.Y);
                noBtn.Location = new Point(BottomRight.X - noBtn.Width / 2, BottomRight.Y);
            }
            
        }

        public void outputData()
        {
            StringBuilder builder = new StringBuilder();
            var filePath = "";
            var newLine = "";
            Trial t;

            try
            {
                // check if our directory exists, if it doesn't then create it
                if (!Directory.Exists("./Data"))
                {
                    Directory.CreateDirectory("./Data");
                }

                filePath = String.Format("{0}{1}-{2}.csv", "./Data/", DateTime.Now.ToString("yyyyMMdd"), CurrExp.ParticipantID);

                // format the pretraining settings and trials
                newLine = "Version,Date,Participant ID\n";
                newLine += String.Format("{0},{1},{2}{3}{4}", 
                    CurrExp.Version, 
                    CurrExp.Date, 
                    CurrExp.ParticipantID,
                    Environment.NewLine,
                    Environment.NewLine);

                newLine += "PRETRAINING PARAMETERS\n";
                newLine += String.Format("{0},{1}{2}", "Pass Criteria", CurrExp.PassCriteria, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Pair Presentations", CurrExp.TrialsPerPair, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "YES Pair Presentations", CurrExp.YesTrialsPerPair, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "NO Pair Presentations", CurrExp.NoTrialsPerPair, Environment.NewLine);

                newLine += "\nPRETRAINING TRIALS\n";
                newLine += String.Format("{0},{1},{2}{3}", "Stimulus A", "Stimulus B", "Latency (s)", Environment.NewLine);
                
                for (var i = 0; i < PretrainingTrials.Count(); i++)
                {
                    t = PretrainingTrials[i];

                    newLine += String.Format("{0},{1},{2}{3}", t.StimPair.A, t.StimPair.B, t.Latency, Environment.NewLine);
                }

                newLine += "\nPRETRAINING EVALUATION TRIALS\n";
                newLine += String.Format("{0},{1},{2},{3},{4},{5}{6}", "Stimulus A", "Stimulus B", "Type (Y/N)", "User Answer (Y/N)", "Is Correct? (Y/N)", "Latency (s)", Environment.NewLine);

                for (var i = 0; i < PretrainingEvalTrials.Count(); i++)
                {
                    t = PretrainingEvalTrials[i];

                    newLine += String.Format("{0},{1},{2},{3},{4},{5}{6}", t.StimPair.A, t.StimPair.B, t.StimPair.Type, t.UserAnswer, t.IsCorrect, t.Latency, Environment.NewLine);
                }

                newLine += "\nTRAINING PARAMETERS\n";
                newLine += String.Format("{0},{1}{2}", "Within Pair Delay", CurrExp.TrainingWithinPairDelay , Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Between Pair Delay", CurrExp.TrainingBetweenPairDelay, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "First Stimulus Duration", CurrExp.TrainingFirstStimDuration, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Second Stimulus Duration", CurrExp.TrainingSecondStimDuration, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Pair Presentations", CurrExp.TrainingTrialsPerPair, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Simultaneous Presentation?", CurrExp.IsSimultaneous, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Pair 1 Location", CurrExp.Location1, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Pair 2 Location", CurrExp.Location2, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Pair 3 Location", CurrExp.Location3, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Pair 4 Location", CurrExp.Location4, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Pair 5 Location", CurrExp.Location5, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Pair 6 Location", CurrExp.Location6, Environment.NewLine);

                newLine += "\nTRAINING TRIALS\n";
                newLine += String.Format("{0},{1},{2}{3}", "Stimulus A", "Stimulus B", "Latency (s)", Environment.NewLine);

                for (var i = 0; i < TrainingTrials.Count(); i++)
                {
                    t = TrainingTrials[i];

                    newLine += String.Format("{0},{1},{2}{3}", t.StimPair.A, t.StimPair.B, t.Latency, Environment.NewLine);
                }

                newLine += "\nEVALUATION PARAMETERS\n";
                newLine += String.Format("{0},{1}{2}", "Within Pair Delay", CurrExp.EvalWithinPairDelay, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Between Pair Delay", CurrExp.EvalBetweenPairDelay, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "First Stimulus Duration", CurrExp.EvalFirstStimDuration, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Second Stimulus Duration", CurrExp.EvalSecondStimDuration, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Pair Presentations", CurrExp.EvalTrialsPerPair, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "YES Button Location", CurrExp.YesLocation, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "NO Button Location", CurrExp.NoLocation, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Evaluation Trial Order", Enum.GetName(typeof(Constants.Order), CurrExp.EvalTrialOrder), Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Symmetry Test", CurrExp.IsSymmetry, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Transitivity Test", CurrExp.IsTransitivity, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Equivalence Test", CurrExp.IsEquivalence, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Symmetry Pair 1 Location", CurrExp.SymLocation1, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Symmetry Pair 2 Location", CurrExp.SymLocation2, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Symmetry Pair 3 Location", CurrExp.SymLocation3, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Symmetry Pair 4 Location", CurrExp.SymLocation4, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Symmetry Pair 5 Location", CurrExp.SymLocation5, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Symmetry Pair 6 Location", CurrExp.SymLocation6, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Transitivity Pair 1 Location", CurrExp.TransLocation1, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Transitivity Pair 2 Location", CurrExp.TransLocation2, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Transitivity Pair 3 Location", CurrExp.TransLocation3, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Equivalence Pair 1 Location", CurrExp.EquivLocation1, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Equivalence Pair 2 Location", CurrExp.EquivLocation2, Environment.NewLine);
                newLine += String.Format("{0},{1}{2}", "Equivalence Pair 3 Location", CurrExp.EquivLocation3, Environment.NewLine);

                newLine += "\nEVALUATION TRIALS\n";
                newLine += String.Format("{0},{1},{2},{3},{4},{5},{6}{7}", "Test Type", "Stimulus A", "Stimulus B", "Type (Y/N)", "User Answer (Y/N)", "Is Correct? (Y/N)", "Latency (s)", Environment.NewLine);

                for (var i = 0; i < EvaluationTrials.Count(); i++)
                {
                    t = EvaluationTrials[i];

                    newLine += String.Format("{0},{1},{2},{3},{4},{5},{6}{7}", Enum.GetName(typeof(Constants.Tests), t.Test), t.StimPair.A, t.StimPair.B, t.StimPair.Type, t.UserAnswer, t.IsCorrect, t.Latency, Environment.NewLine);
                }

                newLine += "\n";
                builder.Append(newLine);

                File.WriteAllText(filePath, builder.ToString());

                MessageBox.Show("Success! Data file saved as: " + filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while writing data to file: " + e.Message);
                throw e;
            }
            finally
            {
                builder = null;
            }
        }
    }
}
