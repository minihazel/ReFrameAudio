namespace ReFrameAudio
{
    partial class mainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            controlPanel = new Panel();
            bRepeat = new Button();
            volumeStatus = new Label();
            bMute = new Button();
            volumeSlider = new TrackBar();
            bPlayback = new Button();
            endTime = new Label();
            currentTime = new Label();
            timestamp = new TrackBar();
            mainPanel = new Panel();
            browserPanel = new Panel();
            controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)timestamp).BeginInit();
            SuspendLayout();
            // 
            // controlPanel
            // 
            controlPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            controlPanel.BackColor = SystemColors.ControlLight;
            controlPanel.Controls.Add(bRepeat);
            controlPanel.Controls.Add(volumeStatus);
            controlPanel.Controls.Add(bMute);
            controlPanel.Controls.Add(volumeSlider);
            controlPanel.Controls.Add(bPlayback);
            controlPanel.Controls.Add(endTime);
            controlPanel.Controls.Add(currentTime);
            controlPanel.Controls.Add(timestamp);
            controlPanel.Location = new Point(0, 361);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(493, 71);
            controlPanel.TabIndex = 0;
            // 
            // bRepeat
            // 
            bRepeat.BackColor = SystemColors.ControlLight;
            bRepeat.BackgroundImage = Properties.Resources.loop;
            bRepeat.BackgroundImageLayout = ImageLayout.Zoom;
            bRepeat.Cursor = Cursors.Hand;
            bRepeat.FlatAppearance.BorderColor = Color.FromArgb(59, 130, 246);
            bRepeat.FlatAppearance.BorderSize = 0;
            bRepeat.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            bRepeat.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
            bRepeat.FlatStyle = FlatStyle.Flat;
            bRepeat.Font = new Font("Bahnschrift SemiLight", 20F);
            bRepeat.Location = new Point(44, 48);
            bRepeat.Name = "bRepeat";
            bRepeat.Size = new Size(20, 20);
            bRepeat.TabIndex = 8;
            bRepeat.UseVisualStyleBackColor = false;
            bRepeat.Click += bRepeat_Click;
            // 
            // volumeStatus
            // 
            volumeStatus.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            volumeStatus.Font = new Font("Bahnschrift SemiLight", 8F);
            volumeStatus.Location = new Point(376, 18);
            volumeStatus.Name = "volumeStatus";
            volumeStatus.Size = new Size(35, 18);
            volumeStatus.TabIndex = 7;
            volumeStatus.Text = "XX%";
            volumeStatus.TextAlign = ContentAlignment.BottomCenter;
            // 
            // bMute
            // 
            bMute.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bMute.BackColor = SystemColors.ControlLight;
            bMute.Cursor = Cursors.Hand;
            bMute.FlatAppearance.BorderSize = 0;
            bMute.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            bMute.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
            bMute.FlatStyle = FlatStyle.Flat;
            bMute.Font = new Font("Bahnschrift SemiLight", 12F);
            bMute.Location = new Point(354, 37);
            bMute.Name = "bMute";
            bMute.Size = new Size(25, 25);
            bMute.TabIndex = 6;
            bMute.Text = "🔊";
            bMute.UseVisualStyleBackColor = false;
            // 
            // volumeSlider
            // 
            volumeSlider.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            volumeSlider.AutoSize = false;
            volumeSlider.Location = new Point(376, 39);
            volumeSlider.Maximum = 100;
            volumeSlider.Name = "volumeSlider";
            volumeSlider.Size = new Size(115, 23);
            volumeSlider.TabIndex = 5;
            volumeSlider.TickStyle = TickStyle.None;
            volumeSlider.Scroll += volumeSlider_Scroll;
            // 
            // bPlayback
            // 
            bPlayback.BackColor = SystemColors.ControlLight;
            bPlayback.BackgroundImage = Properties.Resources.pause;
            bPlayback.BackgroundImageLayout = ImageLayout.Zoom;
            bPlayback.Cursor = Cursors.Hand;
            bPlayback.FlatAppearance.BorderColor = Color.FromArgb(59, 130, 246);
            bPlayback.FlatAppearance.BorderSize = 0;
            bPlayback.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            bPlayback.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
            bPlayback.FlatStyle = FlatStyle.Flat;
            bPlayback.Font = new Font("Bahnschrift SemiLight", 20F);
            bPlayback.Location = new Point(3, 38);
            bPlayback.Name = "bPlayback";
            bPlayback.Size = new Size(30, 30);
            bPlayback.TabIndex = 3;
            bPlayback.UseVisualStyleBackColor = false;
            bPlayback.Click += bPlayback_Click;
            // 
            // endTime
            // 
            endTime.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            endTime.Font = new Font("Bahnschrift SemiLight", 10F);
            endTime.Location = new Point(440, 3);
            endTime.Name = "endTime";
            endTime.Size = new Size(50, 18);
            endTime.TabIndex = 2;
            endTime.Text = "00:00";
            endTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // currentTime
            // 
            currentTime.Font = new Font("Bahnschrift SemiLight", 10F);
            currentTime.Location = new Point(3, 3);
            currentTime.Name = "currentTime";
            currentTime.Size = new Size(49, 18);
            currentTime.TabIndex = 1;
            currentTime.Text = "00:00";
            currentTime.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // timestamp
            // 
            timestamp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            timestamp.AutoSize = false;
            timestamp.Location = new Point(44, 2);
            timestamp.Name = "timestamp";
            timestamp.Size = new Size(404, 23);
            timestamp.TabIndex = 0;
            timestamp.TickStyle = TickStyle.None;
            // 
            // mainPanel
            // 
            mainPanel.AllowDrop = true;
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.BackColor = Color.FromArgb(32, 34, 36);
            mainPanel.Location = new Point(12, 12);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(469, 345);
            mainPanel.TabIndex = 1;
            mainPanel.DragDrop += mainPanel_DragDrop;
            mainPanel.DragEnter += mainPanel_DragEnter;
            // 
            // browserPanel
            // 
            browserPanel.AllowDrop = true;
            browserPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            browserPanel.BackColor = Color.FromArgb(32, 34, 36);
            browserPanel.Location = new Point(12, 12);
            browserPanel.Name = "browserPanel";
            browserPanel.Size = new Size(469, 345);
            browserPanel.TabIndex = 2;
            // 
            // mainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(32, 34, 36);
            ClientSize = new Size(493, 432);
            Controls.Add(mainPanel);
            Controls.Add(controlPanel);
            Controls.Add(browserPanel);
            Font = new Font("Bahnschrift SemiLight", 11F);
            ForeColor = Color.FromArgb(28, 28, 28);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "mainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ReFrame";
            FormClosing += mainForm_FormClosing;
            Load += mainForm_Load;
            controlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)volumeSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)timestamp).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel controlPanel;
        private Panel mainPanel;
        private TrackBar timestamp;
        private Label currentTime;
        private Label endTime;
        private Button bPlayback;
        private Button bMute;
        private TrackBar volumeSlider;
        private Label volumeStatus;
        private Button bRepeat;
        private Panel browserPanel;
    }
}
