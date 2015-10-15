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
        public Stopwatch LatencyTimer = new Stopwatch();

        public List<Trial> PretrainingTrials = new List<Trial>();
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
                        //runTraining();
                        this.Close();
                    }
                    else
                    {
                        CurrStim = StimQueue.Dequeue();

                        labelA.Text = CurrStim.A;
                        labelB.Text = CurrStim.B;

                        // our intro text!
                        labelA.Location = new Point(Center.X - labelA.Width / 2, Center.Y);
                        labelB.Location = new Point(Center.X - labelB.Width / 2, Center.Y);
                        nextBtn.Location = new Point(Center.X - nextBtn.Width / 2, Center.Y);

                        mainPanel.BackColor = Color.White;
                        labelA.Visible = true;
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
                    PretrainingStims.Add(new Stimulus("Red", "Color", "Yes"));
                    PretrainingStims.Add(new Stimulus("Dog", "Bone", "Yes"));
                    PretrainingStims.Add(new Stimulus("Cat", "Mouse", "Yes"));
                    PretrainingStims.Add(new Stimulus("Peanut Butter", "Jelly", "Yes"));
                    PretrainingStims.Add(new Stimulus("Peas", "Carrots", "Yes"));
                    PretrainingStims.Add(new Stimulus("Ketchup", "Mustard", "Yes"));
                    PretrainingStims.Add(new Stimulus("Cow", "Farm", "Yes"));
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
                throw;
            }
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            introPanel.Visible = false;
            runTrial();
        }

        private void firstStimTimer_Tick(object sender, EventArgs e)
        {
            labelA.Visible = false;
            firstStimTimer.Stop();
            withinPairTimer.Start();
        }

        private void withinPairTimer_Tick(object sender, EventArgs e)
        {
            withinPairTimer.Stop();
            labelB.Visible = true;
            secondStimTimer.Start();
        }

        private void secondStimTimer_Tick(object sender, EventArgs e)
        {
            secondStimTimer.Stop();
            labelB.Visible = false;
            controlPanel.Visible = false;
            mainPanel.BackColor = Color.Black;
            betweenPairTimer.Start();
        }

        private void betweenPairTimer_Tick(object sender, EventArgs e)
        {
            betweenPairTimer.Stop();

            if (CurrPhase == (int)Constants.Phases.Pretraining)
            {
                controlPanel.Visible = true;
                nextBtn.Visible = true;
            }

            LatencyTimer.Start();
        }

        private void nextBtn_Click(object sender, EventArgs e)
        {
            nextBtn.Visible = false;
            LatencyTimer.Stop();
            logTrial();
            LatencyTimer.Reset();
            runTrial();
        }

        private void logTrial()
        {
            Trial currTrial;

            try
            {
                currTrial = new Trial(CurrPhase);
                currTrial.StimPair = CurrStim;
                currTrial.Latency = (decimal)LatencyTimer.ElapsedMilliseconds / 1000;

                if (CurrPhase == (int)Constants.Phases.Pretraining)
                {
                    PretrainingTrials.Add(currTrial);
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
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
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
    }
}
