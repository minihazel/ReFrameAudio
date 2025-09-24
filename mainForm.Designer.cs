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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            controlPanel = new Panel();
            bSwitchPageOnPlay = new Button();
            bStopAudio = new Button();
            bRepeat = new Button();
            volumeStatus = new Label();
            bMute = new Button();
            volumeSlider = new TrackBar();
            bPlayback = new Button();
            endTime = new Label();
            currentTime = new Label();
            timestamp = new TrackBar();
            mainPanel = new Panel();
            notice = new Label();
            browserPanel = new Panel();
            panelBrowser = new Panel();
            browseFolders = new ComboBox();
            bDrawer = new Button();
            bSettings = new Button();
            settingsPanel = new Panel();
            settingsContent = new Panel();
            panel1 = new Panel();
            barAddress = new TextBox();
            panel2 = new Panel();
            barFolderName = new TextBox();
            lblAddress = new Label();
            lblFolderName = new Label();
            bBrowseFolder = new Button();
            bRemoveFolder = new Button();
            lblAvailableFolders = new Label();
            availableFolders = new ComboBox();
            videoToolTip = new ToolTip(components);
            controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)volumeSlider).BeginInit();
            ((System.ComponentModel.ISupportInitialize)timestamp).BeginInit();
            mainPanel.SuspendLayout();
            browserPanel.SuspendLayout();
            settingsPanel.SuspendLayout();
            settingsContent.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // controlPanel
            // 
            controlPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            controlPanel.BackColor = SystemColors.ControlLight;
            controlPanel.Controls.Add(bSwitchPageOnPlay);
            controlPanel.Controls.Add(bStopAudio);
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
            // bSwitchPageOnPlay
            // 
            bSwitchPageOnPlay.BackColor = SystemColors.ControlLight;
            bSwitchPageOnPlay.BackgroundImage = Properties.Resources.flip;
            bSwitchPageOnPlay.BackgroundImageLayout = ImageLayout.Zoom;
            bSwitchPageOnPlay.Cursor = Cursors.Hand;
            bSwitchPageOnPlay.FlatAppearance.BorderColor = Color.FromArgb(59, 130, 246);
            bSwitchPageOnPlay.FlatAppearance.BorderSize = 0;
            bSwitchPageOnPlay.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            bSwitchPageOnPlay.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
            bSwitchPageOnPlay.FlatStyle = FlatStyle.Flat;
            bSwitchPageOnPlay.Font = new Font("Bahnschrift SemiLight", 20F);
            bSwitchPageOnPlay.Location = new Point(118, 43);
            bSwitchPageOnPlay.Name = "bSwitchPageOnPlay";
            bSwitchPageOnPlay.Size = new Size(20, 20);
            bSwitchPageOnPlay.TabIndex = 10;
            bSwitchPageOnPlay.UseVisualStyleBackColor = false;
            bSwitchPageOnPlay.Click += bSwitchPageOnPlay_Click;
            // 
            // bStopAudio
            // 
            bStopAudio.BackColor = SystemColors.ControlLight;
            bStopAudio.BackgroundImage = Properties.Resources.stop_button;
            bStopAudio.BackgroundImageLayout = ImageLayout.Zoom;
            bStopAudio.Cursor = Cursors.Hand;
            bStopAudio.FlatAppearance.BorderColor = Color.FromArgb(59, 130, 246);
            bStopAudio.FlatAppearance.BorderSize = 0;
            bStopAudio.FlatAppearance.MouseDownBackColor = SystemColors.ControlLightLight;
            bStopAudio.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
            bStopAudio.FlatStyle = FlatStyle.Flat;
            bStopAudio.Font = new Font("Bahnschrift SemiLight", 20F);
            bStopAudio.Location = new Point(36, 38);
            bStopAudio.Name = "bStopAudio";
            bStopAudio.Size = new Size(30, 30);
            bStopAudio.TabIndex = 9;
            bStopAudio.UseVisualStyleBackColor = false;
            bStopAudio.Click += bStopAudio_Click;
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
            bRepeat.Location = new Point(87, 43);
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
            volumeSlider.SmallChange = 5;
            volumeSlider.TabIndex = 5;
            volumeSlider.TickFrequency = 10;
            volumeSlider.TickStyle = TickStyle.None;
            volumeSlider.Scroll += volumeSlider_Scroll;
            volumeSlider.ValueChanged += volumeSlider_ValueChanged;
            // 
            // bPlayback
            // 
            bPlayback.BackColor = SystemColors.ControlLight;
            bPlayback.BackgroundImage = Properties.Resources.paused_play;
            bPlayback.BackgroundImageLayout = ImageLayout.Zoom;
            bPlayback.Cursor = Cursors.Hand;
            bPlayback.FlatAppearance.BorderColor = Color.FromArgb(59, 130, 246);
            bPlayback.FlatAppearance.BorderSize = 0;
            bPlayback.FlatAppearance.MouseDownBackColor = SystemColors.ControlLightLight;
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
            timestamp.Maximum = 1000;
            timestamp.Name = "timestamp";
            timestamp.Size = new Size(404, 23);
            timestamp.TabIndex = 0;
            timestamp.TickStyle = TickStyle.None;
            timestamp.Scroll += timestamp_Scroll;
            timestamp.MouseDown += timestamp_MouseDown;
            timestamp.MouseMove += timestamp_MouseMove;
            timestamp.MouseUp += timestamp_MouseUp;
            // 
            // mainPanel
            // 
            mainPanel.AllowDrop = true;
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.BackColor = Color.FromArgb(28, 30, 32);
            mainPanel.Controls.Add(notice);
            mainPanel.ForeColor = Color.DarkGray;
            mainPanel.Location = new Point(1, 30);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(491, 329);
            mainPanel.TabIndex = 1;
            mainPanel.DragDrop += mainPanel_DragDrop;
            mainPanel.DragEnter += mainPanel_DragEnter;
            mainPanel.Paint += mainPanel_Paint;
            mainPanel.MouseDoubleClick += mainPanel_MouseDoubleClick;
            // 
            // notice
            // 
            notice.AllowDrop = true;
            notice.Anchor = AnchorStyles.None;
            notice.AutoSize = true;
            notice.Font = new Font("Bahnschrift SemiLight", 9F);
            notice.ForeColor = Color.Gray;
            notice.Location = new Point(56, 157);
            notice.Name = "notice";
            notice.Size = new Size(378, 14);
            notice.TabIndex = 0;
            notice.Text = "📥 Drag and drop audio files here, alternatively double-click anywhere";
            notice.DragDrop += notice_DragDrop;
            notice.DragEnter += notice_DragEnter;
            // 
            // browserPanel
            // 
            browserPanel.AllowDrop = true;
            browserPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            browserPanel.BackColor = Color.FromArgb(28, 30, 32);
            browserPanel.Controls.Add(panelBrowser);
            browserPanel.ForeColor = Color.DarkGray;
            browserPanel.Location = new Point(1, 30);
            browserPanel.Name = "browserPanel";
            browserPanel.Size = new Size(492, 329);
            browserPanel.TabIndex = 2;
            // 
            // panelBrowser
            // 
            panelBrowser.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBrowser.AutoScroll = true;
            panelBrowser.Location = new Point(1, 5);
            panelBrowser.Name = "panelBrowser";
            panelBrowser.Size = new Size(490, 320);
            panelBrowser.TabIndex = 7;
            // 
            // browseFolders
            // 
            browseFolders.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            browseFolders.Cursor = Cursors.Hand;
            browseFolders.DropDownStyle = ComboBoxStyle.DropDownList;
            browseFolders.Font = new Font("Bahnschrift SemiLight", 10F);
            browseFolders.FormattingEnabled = true;
            browseFolders.Location = new Point(37, 3);
            browseFolders.Name = "browseFolders";
            browseFolders.Size = new Size(422, 24);
            browseFolders.TabIndex = 2;
            browseFolders.SelectedIndexChanged += browseFolders_SelectedIndexChanged;
            // 
            // bDrawer
            // 
            bDrawer.BackgroundImage = Properties.Resources.bars;
            bDrawer.BackgroundImageLayout = ImageLayout.Zoom;
            bDrawer.Cursor = Cursors.Hand;
            bDrawer.FlatAppearance.BorderSize = 0;
            bDrawer.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
            bDrawer.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
            bDrawer.FlatStyle = FlatStyle.Flat;
            bDrawer.Location = new Point(6, 3);
            bDrawer.Name = "bDrawer";
            bDrawer.Size = new Size(24, 24);
            bDrawer.TabIndex = 3;
            bDrawer.UseVisualStyleBackColor = true;
            bDrawer.Click += bDrawer_Click;
            bDrawer.MouseEnter += bDrawer_MouseEnter;
            bDrawer.MouseLeave += bDrawer_MouseLeave;
            // 
            // bSettings
            // 
            bSettings.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bSettings.BackgroundImage = Properties.Resources.altsettings;
            bSettings.BackgroundImageLayout = ImageLayout.Zoom;
            bSettings.Cursor = Cursors.Hand;
            bSettings.FlatAppearance.BorderSize = 0;
            bSettings.FlatAppearance.MouseDownBackColor = Color.Gainsboro;
            bSettings.FlatAppearance.MouseOverBackColor = Color.Gainsboro;
            bSettings.FlatStyle = FlatStyle.Flat;
            bSettings.Location = new Point(468, 6);
            bSettings.Name = "bSettings";
            bSettings.Size = new Size(18, 18);
            bSettings.TabIndex = 4;
            bSettings.UseVisualStyleBackColor = true;
            bSettings.Click += bSettings_Click;
            bSettings.MouseEnter += bSettings_MouseEnter;
            bSettings.MouseLeave += bSettings_MouseLeave;
            // 
            // settingsPanel
            // 
            settingsPanel.AllowDrop = true;
            settingsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            settingsPanel.BackColor = Color.FromArgb(28, 30, 32);
            settingsPanel.Controls.Add(settingsContent);
            settingsPanel.Controls.Add(lblAvailableFolders);
            settingsPanel.Controls.Add(availableFolders);
            settingsPanel.Font = new Font("Bahnschrift SemiLight", 10F);
            settingsPanel.ForeColor = Color.DarkGray;
            settingsPanel.Location = new Point(1, 30);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new Size(491, 329);
            settingsPanel.TabIndex = 5;
            // 
            // settingsContent
            // 
            settingsContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            settingsContent.Controls.Add(panel1);
            settingsContent.Controls.Add(panel2);
            settingsContent.Controls.Add(lblAddress);
            settingsContent.Controls.Add(lblFolderName);
            settingsContent.Controls.Add(bBrowseFolder);
            settingsContent.Controls.Add(bRemoveFolder);
            settingsContent.Location = new Point(11, 81);
            settingsContent.Name = "settingsContent";
            settingsContent.Size = new Size(469, 225);
            settingsContent.TabIndex = 10;
            settingsContent.Visible = false;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(barAddress);
            panel1.Location = new Point(18, 50);
            panel1.Name = "panel1";
            panel1.Size = new Size(390, 34);
            panel1.TabIndex = 8;
            // 
            // barAddress
            // 
            barAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            barAddress.BackColor = Color.FromArgb(28, 30, 32);
            barAddress.BorderStyle = BorderStyle.None;
            barAddress.Enabled = false;
            barAddress.Font = new Font("Bahnschrift SemiLight", 12F);
            barAddress.ForeColor = Color.Silver;
            barAddress.Location = new Point(6, 4);
            barAddress.Name = "barAddress";
            barAddress.PlaceholderText = "The path of the audio file";
            barAddress.Size = new Size(379, 20);
            barAddress.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(barFolderName);
            panel2.Location = new Point(18, 129);
            panel2.Name = "panel2";
            panel2.Size = new Size(433, 34);
            panel2.TabIndex = 9;
            // 
            // barFolderName
            // 
            barFolderName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            barFolderName.BackColor = Color.FromArgb(28, 30, 32);
            barFolderName.BorderStyle = BorderStyle.None;
            barFolderName.Enabled = false;
            barFolderName.Font = new Font("Bahnschrift SemiLight", 12F);
            barFolderName.ForeColor = Color.Silver;
            barFolderName.Location = new Point(6, 4);
            barFolderName.Name = "barFolderName";
            barFolderName.PlaceholderText = "The folder alias";
            barFolderName.Size = new Size(422, 20);
            barFolderName.TabIndex = 5;
            // 
            // lblAddress
            // 
            lblAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblAddress.Location = new Point(18, 18);
            lblAddress.Name = "lblAddress";
            lblAddress.Padding = new Padding(5, 0, 0, 0);
            lblAddress.Size = new Size(433, 29);
            lblAddress.TabIndex = 2;
            lblAddress.Text = "🔗 Address";
            lblAddress.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFolderName
            // 
            lblFolderName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblFolderName.Location = new Point(18, 97);
            lblFolderName.Name = "lblFolderName";
            lblFolderName.Padding = new Padding(5, 0, 0, 0);
            lblFolderName.Size = new Size(433, 29);
            lblFolderName.TabIndex = 4;
            lblFolderName.Text = "✏️ Type folder name";
            lblFolderName.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // bBrowseFolder
            // 
            bBrowseFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bBrowseFolder.BackgroundImage = Properties.Resources.expandarrows;
            bBrowseFolder.BackgroundImageLayout = ImageLayout.Zoom;
            bBrowseFolder.Cursor = Cursors.Hand;
            bBrowseFolder.Enabled = false;
            bBrowseFolder.FlatAppearance.BorderSize = 0;
            bBrowseFolder.FlatAppearance.MouseDownBackColor = Color.FromArgb(54, 56, 58);
            bBrowseFolder.FlatAppearance.MouseOverBackColor = Color.FromArgb(54, 56, 58);
            bBrowseFolder.FlatStyle = FlatStyle.Flat;
            bBrowseFolder.Location = new Point(416, 57);
            bBrowseFolder.Name = "bBrowseFolder";
            bBrowseFolder.Size = new Size(21, 21);
            bBrowseFolder.TabIndex = 7;
            bBrowseFolder.UseVisualStyleBackColor = true;
            bBrowseFolder.Click += bBrowseFolder_Click;
            // 
            // bRemoveFolder
            // 
            bRemoveFolder.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bRemoveFolder.Cursor = Cursors.Hand;
            bRemoveFolder.Enabled = false;
            bRemoveFolder.ForeColor = Color.Black;
            bRemoveFolder.Location = new Point(275, 169);
            bRemoveFolder.Name = "bRemoveFolder";
            bRemoveFolder.Size = new Size(176, 38);
            bRemoveFolder.TabIndex = 6;
            bRemoveFolder.Text = "🗑 Remove folder";
            bRemoveFolder.UseVisualStyleBackColor = true;
            bRemoveFolder.Click += bRemoveFolder_Click;
            // 
            // lblAvailableFolders
            // 
            lblAvailableFolders.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblAvailableFolders.Location = new Point(3, 5);
            lblAvailableFolders.Name = "lblAvailableFolders";
            lblAvailableFolders.Size = new Size(485, 38);
            lblAvailableFolders.TabIndex = 1;
            lblAvailableFolders.Text = "🗂 Available folders";
            lblAvailableFolders.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // availableFolders
            // 
            availableFolders.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            availableFolders.Cursor = Cursors.Hand;
            availableFolders.DropDownStyle = ComboBoxStyle.DropDownList;
            availableFolders.Font = new Font("Bahnschrift SemiLight", 13F);
            availableFolders.FormattingEnabled = true;
            availableFolders.Items.AddRange(new object[] { "➕ Add new folder" });
            availableFolders.Location = new Point(29, 46);
            availableFolders.Name = "availableFolders";
            availableFolders.Size = new Size(433, 29);
            availableFolders.TabIndex = 0;
            availableFolders.SelectedIndexChanged += availableFolders_SelectedIndexChanged;
            // 
            // videoToolTip
            // 
            videoToolTip.ToolTipTitle = "File information";
            // 
            // mainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(493, 432);
            Controls.Add(bSettings);
            Controls.Add(browseFolders);
            Controls.Add(bDrawer);
            Controls.Add(controlPanel);
            Controls.Add(mainPanel);
            Controls.Add(browserPanel);
            Controls.Add(settingsPanel);
            Font = new Font("Bahnschrift SemiLight", 11F);
            ForeColor = Color.FromArgb(28, 28, 28);
            Icon = (Icon)resources.GetObject("$this.Icon");
            KeyPreview = true;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(509, 142);
            Name = "mainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ReFrame";
            FormClosing += mainForm_FormClosing;
            Load += mainForm_Load;
            KeyDown += mainForm_KeyDown;
            controlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)volumeSlider).EndInit();
            ((System.ComponentModel.ISupportInitialize)timestamp).EndInit();
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            browserPanel.ResumeLayout(false);
            settingsPanel.ResumeLayout(false);
            settingsContent.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
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
        private Button bDrawer;
        private Button bSettings;
        private Panel settingsPanel;
        private Label notice;
        private ComboBox availableFolders;
        private Label lblAvailableFolders;
        private Label lblAddress;
        private TextBox barAddress;
        private TextBox barFolderName;
        private Label lblFolderName;
        private Button bRemoveFolder;
        private Button bBrowseFolder;
        private Panel panel1;
        private Panel panel2;
        private Panel settingsContent;
        private ComboBox browseFolders;
        private Button bStopAudio;
        private Panel panelBrowser;
        private ToolTip videoToolTip;
        private Button bSwitchPageOnPlay;
    }
}
