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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.introPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.startBtn = new System.Windows.Forms.Button();
            this.introLB = new System.Windows.Forms.Label();
            this.controlPanel = new System.Windows.Forms.Panel();
            this.labelA = new System.Windows.Forms.Label();
            this.noBtn = new System.Windows.Forms.Button();
            this.labelB = new System.Windows.Forms.Label();
            this.yesBtn = new System.Windows.Forms.Button();
            this.nextBtn = new System.Windows.Forms.Button();
            this.firstStimTimer = new System.Windows.Forms.Timer(this.components);
            this.secondStimTimer = new System.Windows.Forms.Timer(this.components);
            this.withinPairTimer = new System.Windows.Forms.Timer(this.components);
            this.betweenPairTimer = new System.Windows.Forms.Timer(this.components);
            this.mainPanel.SuspendLayout();
            this.introPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.controlPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BackColor = System.Drawing.Color.White;
            this.mainPanel.Controls.Add(this.introPanel);
            this.mainPanel.Controls.Add(this.controlPanel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(712, 416);
            this.mainPanel.TabIndex = 0;
            // 
            // introPanel
            // 
            this.introPanel.Controls.Add(this.panel1);
            this.introPanel.Controls.Add(this.introLB);
            this.introPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.introPanel.Location = new System.Drawing.Point(0, 0);
            this.introPanel.Name = "introPanel";
            this.introPanel.Size = new System.Drawing.Size(712, 416);
            this.introPanel.TabIndex = 3;
            this.introPanel.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.startBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 246);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(712, 170);
            this.panel1.TabIndex = 2;
            // 
            // startBtn
            // 
            this.startBtn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.startBtn.BackColor = System.Drawing.Color.Transparent;
            this.startBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startBtn.Location = new System.Drawing.Point(281, 56);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(150, 60);
            this.startBtn.TabIndex = 1;
            this.startBtn.Text = "START";
            this.startBtn.UseVisualStyleBackColor = false;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // introLB
            // 
            this.introLB.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.introLB.AutoSize = true;
            this.introLB.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.introLB.Location = new System.Drawing.Point(290, 64);
            this.introLB.MaximumSize = new System.Drawing.Size(700, 0);
            this.introLB.Name = "introLB";
            this.introLB.Size = new System.Drawing.Size(124, 20);
            this.introLB.TabIndex = 0;
            this.introLB.Text = "Introduction text";
            // 
            // controlPanel
            // 
            this.controlPanel.BackColor = System.Drawing.Color.Transparent;
            this.controlPanel.Controls.Add(this.labelA);
            this.controlPanel.Controls.Add(this.noBtn);
            this.controlPanel.Controls.Add(this.labelB);
            this.controlPanel.Controls.Add(this.yesBtn);
            this.controlPanel.Controls.Add(this.nextBtn);
            this.controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlPanel.Location = new System.Drawing.Point(0, 0);
            this.controlPanel.Name = "controlPanel";
            this.controlPanel.Size = new System.Drawing.Size(712, 416);
            this.controlPanel.TabIndex = 2;
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
            // 
            // noBtn
            // 
            this.noBtn.BackColor = System.Drawing.Color.Transparent;
            this.noBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.noBtn.Location = new System.Drawing.Point(24, 290);
            this.noBtn.Name = "noBtn";
            this.noBtn.Size = new System.Drawing.Size(150, 60);
            this.noBtn.TabIndex = 1;
            this.noBtn.Text = "NO";
            this.noBtn.UseVisualStyleBackColor = false;
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
            // 
            // yesBtn
            // 
            this.yesBtn.BackColor = System.Drawing.Color.Transparent;
            this.yesBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yesBtn.Location = new System.Drawing.Point(24, 224);
            this.yesBtn.Name = "yesBtn";
            this.yesBtn.Size = new System.Drawing.Size(150, 60);
            this.yesBtn.TabIndex = 1;
            this.yesBtn.Text = "YES";
            this.yesBtn.UseVisualStyleBackColor = false;
            // 
            // nextBtn
            // 
            this.nextBtn.BackColor = System.Drawing.Color.Transparent;
            this.nextBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nextBtn.Location = new System.Drawing.Point(24, 158);
            this.nextBtn.Name = "nextBtn";
            this.nextBtn.Size = new System.Drawing.Size(200, 60);
            this.nextBtn.TabIndex = 1;
            this.nextBtn.Text = "Present the Next Pair";
            this.nextBtn.UseVisualStyleBackColor = false;
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
            // MainBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 416);
            this.Controls.Add(this.mainPanel);
            this.KeyPreview = true;
            this.Name = "MainBoard";
            this.Text = "Experiment Screen";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainBoard_FormClosed);
            this.Load += new System.EventHandler(this.MainBoard_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainBoard_KeyDown);
            this.mainPanel.ResumeLayout(false);
            this.introPanel.ResumeLayout(false);
            this.introPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.controlPanel.ResumeLayout(false);
            this.controlPanel.PerformLayout();
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
        private System.Windows.Forms.Label introLB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button startBtn;
    }
}