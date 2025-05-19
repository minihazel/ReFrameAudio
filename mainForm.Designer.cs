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
            button1 = new Button();
            volumeStatus = new Label();
            bMute = new Button();
            volumeSlider = new TrackBar();
            bPlayback = new Button();
            endTime = new Label();
            currentTime = new Label();
            timestamp = new TrackBar();
            panel1 = new Panel();
            controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)timestamp).BeginInit();
            SuspendLayout();
            // 
            // controlPanel
            // 
            controlPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            controlPanel.BackColor = SystemColors.ControlLight;
            controlPanel.Controls.Add(button1);
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
            // button1
            // 
            button1.BackColor = SystemColors.ControlLight;
            button1.BackgroundImage = (Image)resources.GetObject("button1.BackgroundImage");
            button1.BackgroundImageLayout = ImageLayout.Zoom;
            button1.Cursor = Cursors.Hand;
            button1.FlatAppearance.BorderColor = Color.FromArgb(59, 130, 246);
            button1.FlatAppearance.BorderSize = 2;
            button1.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            button1.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Bahnschrift SemiLight", 20F);
            button1.Location = new Point(49, 31);
            button1.Name = "button1";
            button1.Size = new Size(32, 32);
            button1.TabIndex = 8;
            button1.UseVisualStyleBackColor = false;
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
            bPlayback.BackgroundImage = (Image)resources.GetObject("bPlayback.BackgroundImage");
            bPlayback.BackgroundImageLayout = ImageLayout.Zoom;
            bPlayback.Cursor = Cursors.Hand;
            bPlayback.FlatAppearance.BorderColor = Color.FromArgb(59, 130, 246);
            bPlayback.FlatAppearance.BorderSize = 2;
            bPlayback.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            bPlayback.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
            bPlayback.FlatStyle = FlatStyle.Flat;
            bPlayback.Font = new Font("Bahnschrift SemiLight", 20F);
            bPlayback.Location = new Point(8, 31);
            bPlayback.Name = "bPlayback";
            bPlayback.Size = new Size(32, 32);
            bPlayback.TabIndex = 3;
            bPlayback.UseVisualStyleBackColor = false;
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
            // panel1
            // 
            panel1.AllowDrop = true;
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.FromArgb(32, 34, 36);
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(493, 357);
            panel1.TabIndex = 1;
            // 
            // mainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(493, 432);
            Controls.Add(panel1);
            Controls.Add(controlPanel);
            Font = new Font("Bahnschrift SemiLight", 11F);
            ForeColor = Color.FromArgb(28, 28, 28);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "mainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ReFrame";
            Load += mainForm_Load;
            controlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)volumeSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)timestamp).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel controlPanel;
        private Panel panel1;
        private TrackBar timestamp;
        private Label currentTime;
        private Label endTime;
        private Button bPlayback;
        private Button bMute;
        private TrackBar volumeSlider;
        private Label volumeStatus;
        private Button button1;
    }
}
