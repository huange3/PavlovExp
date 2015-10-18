namespace PavlovExp
{
    partial class MainBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainBoard));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.introPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.startBtn = new System.Windows.Forms.Button();
            this.introEvalLB = new System.Windows.Forms.Label();
            this.introPTEvalLB = new System.Windows.Forms.Label();
            this.introTrainingLB = new System.Windows.Forms.Label();
            this.introPTLB = new System.Windows.Forms.Label();
            this.labelA = new System.Windows.Forms.Label();
            this.noBtn = new System.Windows.Forms.Button();
            this.incorrectLB = new System.Windows.Forms.Label();
            this.correctLB = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.yesBtn = new System.Windows.Forms.Button();
            this.nextBtn = new System.Windows.Forms.Button();
            this.firstStimTimer = new System.Windows.Forms.Timer(this.components);
            this.secondStimTimer = new System.Windows.Forms.Timer(this.components);
            this.withinPairTimer = new System.Windows.Forms.Timer(this.components);
            this.betweenPairTimer = new System.Windows.Forms.Timer(this.components);
            this.rewardTimer = new System.Windows.Forms.Timer(this.components);
            this.mainPanel.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.introPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Controls.Add(this.controlPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1110, 602);
            this.mainPanel.TabIndex = 0;
            // 
            // controlPanel
            // 
            this.controlPanel.BackColor = System.Drawing.Color.Transparent;
            this.controlPanel.Controls.Add(this.introPanel);
            this.controlPanel.Controls.Add(this.labelA);
            this.controlPanel.Controls.Add(this.noBtn);
            this.controlPanel.Controls.Add(this.incorrectLB);
            this.controlPanel.Controls.Add(this.correctLB);
            this.controlPanel.Controls.Add(this.labelB);
            this.controlPanel.Controls.Add(this.yesBtn);
            this.controlPanel.Controls.Add(this.nextBtn);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(1110, 602);
            this.controlPanel.TabIndex = 2;
            // 
            // introPanel
            // 
            this.introPanel.Controls.Add(this.panel1);
            this.introPanel.Controls.Add(this.introEvalLB);
            this.introPanel.Controls.Add(this.introPTEvalLB);
            this.introPanel.Controls.Add(this.introTrainingLB);
            this.introPanel.Controls.Add(this.introPTLB);
            this.introPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.introPanel.Location = new System.Drawing.Point(0, 0);
            this.introPanel.Name = "introPanel";
            this.introPanel.Size = new System.Drawing.Size(1110, 602);
            this.introPanel.TabIndex = 3;
            this.introPanel.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.startBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 432);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1110, 170);
            this.panel1.TabIndex = 2;
            // 
            // startBtn
            // 
            this.startBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.startBtn.BackColor = System.Drawing.SystemColors.Control;
            this.startBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.startBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startBtn.Location = new System.Drawing.Point(480, 56);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(150, 60);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "START";
            this.startBtn.UseVisualStyleBackColor = false;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // introEvalLB
            // 
            this.introEvalLB.AutoSize = true;
            this.introEvalLB.BackColor = System.Drawing.Color.White;
            this.introEvalLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.introEvalLB.Location = new System.Drawing.Point(715, 207);
            this.introEvalLB.MaximumSize = new System.Drawing.Size(700, 0);
            this.introEvalLB.Name = "introEvalLB";
            this.introEvalLB.Size = new System.Drawing.Size(699, 100);
            this.introEvalLB.TabIndex = 0;
            this.introEvalLB.Text = resources.GetString("introEvalLB.Text");
            // 
            // introPTEvalLB
            // 
            this.introPTEvalLB.AutoSize = true;
            this.introPTEvalLB.BackColor = System.Drawing.Color.White;
            this.introPTEvalLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.introPTEvalLB.Location = new System.Drawing.Point(715, 22);
            this.introPTEvalLB.MaximumSize = new System.Drawing.Size(700, 0);
            this.introPTEvalLB.Name = "introPTEvalLB";
            this.introPTEvalLB.Size = new System.Drawing.Size(700, 140);
            this.introPTEvalLB.TabIndex = 0;
            this.introPTEvalLB.Text = resources.GetString("introPTEvalLB.Text");
            // 
            // introTrainingLB
            // 
            this.introTrainingLB.AutoSize = true;
            this.introTrainingLB.BackColor = System.Drawing.Color.White;
            this.introTrainingLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.introTrainingLB.Location = new System.Drawing.Point(9, 211);
            this.introTrainingLB.MaximumSize = new System.Drawing.Size(700, 0);
            this.introTrainingLB.Name = "introTrainingLB";
            this.introTrainingLB.Size = new System.Drawing.Size(686, 160);
            this.introTrainingLB.TabIndex = 0;
            this.introTrainingLB.Text = resources.GetString("introTrainingLB.Text");
            // 
            // introPTLB
            // 
            this.introPTLB.AutoSize = true;
            this.introPTLB.BackColor = System.Drawing.Color.White;
            this.introPTLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.introPTLB.Location = new System.Drawing.Point(9, 22);
            this.introPTLB.MaximumSize = new System.Drawing.Size(700, 0);
            this.introPTLB.Name = "introPTLB";
            this.introPTLB.Size = new System.Drawing.Size(700, 180);
            this.introPTLB.TabIndex = 0;
            this.introPTLB.Text = resources.GetString("introPTLB.Text");
            // 
            // labelA
            // 
            this.labelA.AutoSize = true;
            this.labelA.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelA.Location = new System.Drawing.Point(14, 11);
            this.labelA.Name = "labelA";
            this.labelA.Size = new System.Drawing.Size(252, 55);
            this.labelA.TabIndex = 0;
            this.labelA.Text = "Stimulus A";
            this.labelA.Visible = false;
            // 
            // noBtn
            // 
            this.noBtn.BackColor = System.Drawing.SystemColors.Control;
            this.noBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.noBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noBtn.ForeColor = System.Drawing.Color.Red;
            this.noBtn.Location = new System.Drawing.Point(24, 290);
            this.noBtn.Name = "noBtn";
            this.noBtn.Size = new System.Drawing.Size(150, 60);
            this.noBtn.TabIndex = 1;
            this.noBtn.Text = "NO";
            this.noBtn.UseVisualStyleBackColor = false;
            this.noBtn.Visible = false;
            this.noBtn.Click += new System.EventHandler(this.noBtn_Click);
            // 
            // incorrectLB
            // 
            this.incorrectLB.AutoSize = true;
            this.incorrectLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.incorrectLB.ForeColor = System.Drawing.Color.Red;
            this.incorrectLB.Location = new System.Drawing.Point(272, 76);
            this.incorrectLB.Name = "incorrectLB";
            this.incorrectLB.Size = new System.Drawing.Size(310, 55);
            this.incorrectLB.TabIndex = 0;
            this.incorrectLB.Text = "INCORRECT";
            this.incorrectLB.Visible = false;
            // 
            // correctLB
            // 
            this.correctLB.AutoSize = true;
            this.correctLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.correctLB.ForeColor = System.Drawing.Color.Green;
            this.correctLB.Location = new System.Drawing.Point(272, 9);
            this.correctLB.Name = "correctLB";
            this.correctLB.Size = new System.Drawing.Size(262, 55);
            this.correctLB.TabIndex = 0;
            this.correctLB.Text = "CORRECT";
            this.correctLB.Visible = false;
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelB.Location = new System.Drawing.Point(14, 78);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(252, 55);
            this.labelB.TabIndex = 0;
            this.labelB.Text = "Stimulus B";
            this.labelB.Visible = false;
            // 
            // yesBtn
            // 
            this.yesBtn.BackColor = System.Drawing.SystemColors.Control;
            this.yesBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.yesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yesBtn.ForeColor = System.Drawing.Color.Green;
            this.yesBtn.Location = new System.Drawing.Point(24, 224);
            this.yesBtn.Name = "yesBtn";
            this.yesBtn.Size = new System.Drawing.Size(150, 60);
            this.yesBtn.TabIndex = 1;
            this.yesBtn.Text = "YES";
            this.yesBtn.UseVisualStyleBackColor = false;
            this.yesBtn.Visible = false;
            this.yesBtn.Click += new System.EventHandler(this.yesBtn_Click);
            // 
            // nextBtn
            // 
            this.nextBtn.BackColor = System.Drawing.SystemColors.Control;
            this.nextBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.nextBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextBtn.Location = new System.Drawing.Point(24, 158);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(200, 60);
            this.nextBtn.TabIndex = 1;
            this.nextBtn.Text = "Present the Next Pair";
            this.nextBtn.UseVisualStyleBackColor = false;
            this.nextBtn.Visible = false;
            this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);
            // 
            // firstStimTimer
            // 
            this.firstStimTimer.Tick += new System.EventHandler(this.firstStimTimer_Tick);
            // 
            // secondStimTimer
            // 
            this.secondStimTimer.Tick += new System.EventHandler(this.secondStimTimer_Tick);
            // 
            // withinPairTimer
            // 
            this.withinPairTimer.Tick += new System.EventHandler(this.withinPairTimer_Tick);
            // 
            // betweenPairTimer
            // 
            this.betweenPairTimer.Tick += new System.EventHandler(this.betweenPairTimer_Tick);
            // 
            // rewardTimer
            // 
            this.rewardTimer.Interval = 2000;
            this.rewardTimer.Tick += new System.EventHandler(this.rewardTimer_Tick);
            // 
            // MainBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1110, 602);
            this.Controls.Add(this.mainPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "MainBoard";
            this.Text = "Experiment Board";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainBoard_FormClosed);
            this.Load += new System.EventHandler(this.MainBoard_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainBoard_KeyDown);
            this.mainPanel.ResumeLayout(false);
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
            this.introPanel.ResumeLayout(false);
            this.introPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel controlPanel;
        private System.Windows.Forms.Label labelA;
        private System.Windows.Forms.Button noBtn;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Button yesBtn;
        private System.Windows.Forms.Button nextBtn;
        private System.Windows.Forms.Timer firstStimTimer;
        private System.Windows.Forms.Timer secondStimTimer;
        private System.Windows.Forms.Timer withinPairTimer;
        private System.Windows.Forms.Timer betweenPairTimer;
        private System.Windows.Forms.Panel introPanel;
        private System.Windows.Forms.Label introPTLB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label incorrectLB;
        private System.Windows.Forms.Label correctLB;
        private System.Windows.Forms.Timer rewardTimer;
        private System.Windows.Forms.Label introEvalLB;
        private System.Windows.Forms.Label introPTEvalLB;
        private System.Windows.Forms.Label introTrainingLB;
    }
}