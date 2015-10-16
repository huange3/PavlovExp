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

namespace PavlovExp
{
    public partial class MainBoard : Form
    {
        /* Experiment Phases
                ----------------------
                1. Pretraining
                    a. Pretraining 
                    b. Yes/No evaluation
                2. Training
                3. Yes/No Evaluation 
        */
        public int CurrPhase;
        public Experiment CurrExp;
        public List<Stimulus> PretrainingStims;
        public Queue<Stimulus> StimQueue;
        public Stimulus CurrStim;
        public Trial CurrTrial;
        public Stopwatch LatencyTimer = new Stopwatch();

        public List<Trial> PretrainingTrials = new List<Trial>();
        public List<Trial> PretrainingEvalTrials = new List<Trial>();
        public int CorrectCount = 0;
        public int TotalTrials = 0;

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
            //this.TopMost = true;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;          
        }

        public void runPreTraining()
        {
            try
            {
               if (initializePreTraining())
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
                if (initializePreTrainingEval())
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
                        nextBtn.Location = new Point(Center.X - nextBtn.Width / 2, Center.Y);

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
                            initializePreTrainingEval();
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
                        correctLB.Location = new Point(Center.X - correctLB.Width / 2, Center.Y);
                        incorrectLB.Location = new Point(Center.X - incorrectLB.Width / 2, Center.Y);
                        yesBtn.Location = new Point(BottomLeft.X - yesBtn.Width / 2, BottomLeft.Y);
                        noBtn.Location = new Point(BottomRight.X - noBtn.Width / 2, BottomRight.Y);
                        nextBtn.Location = new Point(Center.X - nextBtn.Width / 2, Center.Y);

                        mainPanel.BackColor = Color.White;
                        labelA.Visible = true;
                        labelB.Visible = false;
                        firstStimTimer.Start();
                    }
                }

                // TRAINING PHASE

                // EVALUATION PHASE
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while running trial: " + e.Message);
                throw e;
            }
        }


        private void runTraining()
        {

        }

        private void runEvaluation()
        {

        }

        private bool initializePreTraining()
        {
            try
            {
                CurrPhase = (int)Constants.Phases.Pretraining;

                // initialize our stimulus list
                PretrainingStims = new List<Stimulus>();

                // populate our stimulus list, shuffle, then add to a queue
                for (var i = 0; i < CurrExp.TrialsPerPair; i++)
                {
                    PretrainingStims.Add(new Stimulus("Red", "Color", 1));
                    PretrainingStims.Add(new Stimulus("Dog", "Bone", 1));
                    PretrainingStims.Add(new Stimulus("Cat", "Mouse", 1));
                    PretrainingStims.Add(new Stimulus("Peanut Butter", "Jelly", 1));
                    PretrainingStims.Add(new Stimulus("Peas", "Carrots", 1));
                    PretrainingStims.Add(new Stimulus("Ketchup", "Mustard", 1));
                    PretrainingStims.Add(new Stimulus("Cow", "Farm", 1));
                }

                PretrainingStims.Shuffle();

                StimQueue = new Queue<Stimulus>(PretrainingStims);

                // set up our timers
                // default for pretraining is 1000 ms for stimuli presentation
                // 500 ms for within pair delay
                // 3000 ms for between pair delay
                firstStimTimer.Interval = 1000;
                secondStimTimer.Interval = 1000;
                withinPairTimer.Interval = 500;
                betweenPairTimer.Interval = 3000;
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
                MessageBox.Show("Error occurred while initializing pretraining phase: " + e.Message);
                return false;
                throw e;
            }
        }

        private bool initializePreTrainingEval()
        {
            try
            {
                CurrPhase = (int)Constants.Phases.PretrainingEval;

                // initialize our stimulus list
                PretrainingStims = new List<Stimulus>();

                // populate our stimulus list, shuffle, then add to a queue
                for (var i = 0; i < CurrExp.YesTrialsPerPair; i++)
                {
                    // YES types
                    PretrainingStims.Add(new Stimulus("Red", "Color", 1));
                    PretrainingStims.Add(new Stimulus("Dog", "Bone", 1));
                    PretrainingStims.Add(new Stimulus("Cat", "Mouse", 1));
                    PretrainingStims.Add(new Stimulus("Peanut Butter", "Jelly", 1));
                    PretrainingStims.Add(new Stimulus("Peas", "Carrots", 1));
                    PretrainingStims.Add(new Stimulus("Ketchup", "Mustard", 1));
                    PretrainingStims.Add(new Stimulus("Cow", "Farm", 1));                 
                }

                for (var i = 0; i < CurrExp.NoTrialsPerPair; i++)
                {
                    // NO types
                    PretrainingStims.Add(new Stimulus("Soap", "Exit", 0));
                    PretrainingStims.Add(new Stimulus("Right", "Cloud", 0));
                    PretrainingStims.Add(new Stimulus("Blue", "Camel", 0));
                    PretrainingStims.Add(new Stimulus("Button", "Tomato", 0));
                    PretrainingStims.Add(new Stimulus("Candy", "Different", 0));
                    PretrainingStims.Add(new Stimulus("Eggs", "Shoe", 0));
                    PretrainingStims.Add(new Stimulus("Flower", "Black", 0));
                }

                TotalTrials = PretrainingStims.Count;
                CorrectCount = 0;

                PretrainingStims.Shuffle();

                StimQueue = new Queue<Stimulus>(PretrainingStims);

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
                MessageBox.Show("Error occurred while initializing pretraining phase: " + e.Message);
                return false;
                throw e;
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            introPanel.Visible = false;
            runTrial();
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
            labelB.Visible = true;
            secondStimTimer.Start();
        }

        private void betweenTimerTick()
        {
            betweenPairTimer.Stop();

            if (CurrPhase == (int)Constants.Phases.Pretraining)
            {
                // if we're at the end, skip the button and latency timer
                // jump straight into our evaluation trials
                if (StimQueue.Count == 0)
                {
                    runPreTrainingEval();
                    return;
                }
                else nextBtn.Visible = true;
            }
            else if (CurrPhase == (int)Constants.Phases.PretrainingEval)
            {
                nextBtn.Visible = false;
                yesBtn.Visible = true;
                noBtn.Visible = true;
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
            runTrial();
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
                else
                {
                    CurrTrial.IsCorrect = 0;
                }

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

                if (CorrectCount >= correctNeeded)
                {
                    return true;
                }

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

            if (CurrPhase == (int)Constants.Phases.PretrainingEval)
            {
                // if we're at the end, skip the button
                if (StimQueue.Count == 0) runTrial();
                else nextBtn.Visible = true;
            }
        }
    }
}
