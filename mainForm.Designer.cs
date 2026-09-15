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
            panelSeparator1 = new Panel();
            bResizeWindow = new Button();
            resolutionStrip = new ContextMenuStrip(components);
            resolution1 = new ToolStripMenuItem();
            resolution2 = new ToolStripMenuItem();
            resolution3 = new ToolStripMenuItem();
            resetRes = new ToolStripMenuItem();
            volumeSlider = new ReFrameVolumeSlider();
            timestamp = new ReFrameSlider();
            bSwitchPageOnPlay = new Button();
            bStopAudio = new Button();
            bRepeat = new Button();
            bMute = new Button();
            bPlayback = new Button();
            endTime = new Label();
            currentTime = new Label();
            mainPanel = new Panel();
            mediaViewer = new LibVLCSharp.WinForms.VideoView();
            browserPanel = new Panel();
            panelBrowser = new Panel();
            browseFolders = new ComboBox();
            bDrawer = new Button();
            bSettings = new Button();
            settingsPanel = new Panel();
            lblAvailableFolders = new Label();
            availableFolders = new ComboBox();
            panelSettings = new Panel();
            chkResetWindowSizeOnStartup = new CheckBox();
            chkAutoOpenVideos = new CheckBox();
            chkHideScroll = new CheckBox();
            bResetWindowSize = new Button();
            chkPlayLastUsedTrack = new CheckBox();
            label1 = new Label();
            chkAutoloadFolder = new CheckBox();
            chkUseLastTimestamp = new CheckBox();
            settingsContent = new Panel();
            bFactoryReset = new Button();
            panel1 = new Panel();
            barAddress = new TextBox();
            panel2 = new Panel();
            barFolderName = new TextBox();
            lblAddress = new Label();
            lblFolderName = new Label();
            bBrowseFolder = new Button();
            bRemoveFolder = new Button();
            videoToolTip = new ToolTip(components);
            mediaStatus = new Label();
            controlPanel.SuspendLayout();
            resolutionStrip.SuspendLayout();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)mediaViewer).BeginInit();
            browserPanel.SuspendLayout();
            settingsPanel.SuspendLayout();
            panelSettings.SuspendLayout();
            settingsContent.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // controlPanel
            // 
            controlPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            controlPanel.BackColor = SystemColors.ControlLight;
            controlPanel.Controls.Add(panelSeparator1);
            controlPanel.Controls.Add(bResizeWindow);
            controlPanel.Controls.Add(volumeSlider);
            controlPanel.Controls.Add(timestamp);
            controlPanel.Controls.Add(bSwitchPageOnPlay);
            controlPanel.Controls.Add(bStopAudio);
            controlPanel.Controls.Add(bRepeat);
            controlPanel.Controls.Add(bMute);
            controlPanel.Controls.Add(bPlayback);
            controlPanel.Controls.Add(endTime);
            controlPanel.Controls.Add(currentTime);
            controlPanel.Location = new Point(0, 432);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(493, 71);
            controlPanel.TabIndex = 0;
            // 
            // panelSeparator1
            // 
            panelSeparator1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            panelSeparator1.Location = new Point(73, 42);
            panelSeparator1.Name = "panelSeparator1";
            panelSeparator1.Size = new Size(10, 22);
            panelSeparator1.TabIndex = 19;
            panelSeparator1.Paint += panelSeparator1_Paint;
            // 
            // bResizeWindow
            // 
            bResizeWindow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            bResizeWindow.BackColor = SystemColors.ControlLight;
            bResizeWindow.BackgroundImage = Properties.Resources.double_arrows_deselected;
            bResizeWindow.BackgroundImageLayout = ImageLayout.Zoom;
            bResizeWindow.ContextMenuStrip = resolutionStrip;
            bResizeWindow.Cursor = Cursors.Hand;
            bResizeWindow.Enabled = false;
            bResizeWindow.FlatAppearance.BorderColor = Color.FromArgb(59, 130, 246);
            bResizeWindow.FlatAppearance.BorderSize = 0;
            bResizeWindow.FlatAppearance.MouseDownBackColor = SystemColors.ControlLight;
            bResizeWindow.FlatAppearance.MouseOverBackColor = SystemColors.ControlLight;
            bResizeWindow.FlatStyle = FlatStyle.Flat;
            bResizeWindow.Font = new Font("Bahnschrift SemiLight", 20F);
            bResizeWindow.Location = new Point(159, 44);
            bResizeWindow.Name = "bResizeWindow";
            bResizeWindow.Size = new Size(16, 16);
            bResizeWindow.TabIndex = 18;
            bResizeWindow.Tag = "";
            bResizeWindow.UseVisualStyleBackColor = false;
            // 
            // resolutionStrip
            // 
            resolutionStrip.Items.AddRange(new ToolStripItem[] { resolution1, resolution2, resolution3, resetRes });
            resolutionStrip.Name = "resolutionStrip";
            resolutionStrip.RenderMode = ToolStripRenderMode.Professional;
            resolutionStrip.ShowImageMargin = false;
            resolutionStrip.Size = new Size(100, 92);
            // 
            // resolution1
            // 
            resolution1.Name = "resolution1";
            resolution1.Size = new Size(99, 22);
            resolution1.Text = "⛶   480p";
            resolution1.Click += resolution1_Click;
            // 
            // resolution2
            // 
            resolution2.Name = "resolution2";
            resolution2.Size = new Size(99, 22);
            resolution2.Text = "⛶   720p";
            resolution2.Click += resolution2_Click;
            // 
            // resolution3
            // 
            resolution3.Name = "resolution3";
            resolution3.Size = new Size(99, 22);
            resolution3.Text = "⛶   1080p";
            resolution3.Click += resolution3_Click;
            // 
            // resetRes
            // 
            resetRes.Name = "resetRes";
            resetRes.Size = new Size(99, 22);
            resetRes.Text = "❌   Reset";
            resetRes.Click += resetRes_Click;
            // 
            // volumeSlider
            // 
            volumeSlider.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            volumeSlider.ForeColor = SystemColors.ControlLight;
            volumeSlider.Location = new Point(385, 35);
            volumeSlider.Name = "volumeSlider";
            volumeSlider.ProgressColor = Color.FromArgb(72, 210, 72);
            volumeSlider.Size = new Size(96, 30);
            volumeSlider.TabIndex = 17;
            volumeSlider.Text = "reFrameVolumeSlider1";
            volumeSlider.ThumbColor = Color.White;
            volumeSlider.ThumbSize = 0;
            volumeSlider.TrackColor = Color.FromArgb(116, 116, 116);
            volumeSlider.TrackHeight = 4;
            volumeSlider.Value = 25F;
            // 
            // timestamp
            // 
            timestamp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            timestamp.IsLoopActive = false;
            timestamp.Location = new Point(49, 1);
            timestamp.LoopEnd = -1L;
            timestamp.LoopStart = -1L;
            timestamp.Maximum = 100L;
            timestamp.Minimum = 0L;
            timestamp.Name = "timestamp";
            timestamp.ProgressColor = Color.FromArgb(0, 120, 215);
            timestamp.Size = new Size(392, 23);
            timestamp.TabIndex = 15;
            timestamp.Text = "reFrameSlider1";
            timestamp.ThumbColor = Color.White;
            timestamp.ThumbSize = 12;
            timestamp.TrackColor = Color.FromArgb(189, 189, 189);
            timestamp.TrackHeight = 8;
            timestamp.Value = 0L;
            // 
            // bSwitchPageOnPlay
            // 
            bSwitchPageOnPlay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
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
            bSwitchPageOnPlay.Location = new Point(125, 42);
            bSwitchPageOnPlay.Name = "bSwitchPageOnPlay";
            bSwitchPageOnPlay.Size = new Size(20, 20);
            bSwitchPageOnPlay.TabIndex = 10;
            bSwitchPageOnPlay.Tag = "";
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
            bRepeat.Location = new Point(94, 42);
            bRepeat.Name = "bRepeat";
            bRepeat.Size = new Size(20, 20);
            bRepeat.TabIndex = 8;
            bRepeat.UseVisualStyleBackColor = false;
            bRepeat.Click += bRepeat_Click;
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
            // mainPanel
            // 
            mainPanel.AllowDrop = true;
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.BackColor = Color.FromArgb(28, 30, 32);
            mainPanel.Controls.Add(mediaStatus);
            mainPanel.Controls.Add(mediaViewer);
            mainPanel.ForeColor = Color.DarkGray;
            mainPanel.Location = new Point(-1, 30);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(495, 401);
            mainPanel.TabIndex = 1;
            mainPanel.DragDrop += mainPanel_DragDrop;
            mainPanel.DragEnter += mainPanel_DragEnter;
            mainPanel.MouseDoubleClick += mainPanel_MouseDoubleClick;
            // 
            // mediaViewer
            // 
            mediaViewer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mediaViewer.BackColor = Color.FromArgb(45, 45, 45);
            mediaViewer.Location = new Point(0, 0);
            mediaViewer.MediaPlayer = null;
            mediaViewer.Name = "mediaViewer";
            mediaViewer.Size = new Size(495, 402);
            mediaViewer.TabIndex = 0;
            mediaViewer.Text = "videoView1";
            // 
            // browserPanel
            // 
            browserPanel.AllowDrop = true;
            browserPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            browserPanel.BackColor = Color.FromArgb(28, 30, 32);
            browserPanel.Controls.Add(panelBrowser);
            browserPanel.ForeColor = Color.DarkGray;
            browserPanel.Location = new Point(0, 30);
            browserPanel.Name = "browserPanel";
            browserPanel.Size = new Size(493, 400);
            browserPanel.TabIndex = 2;
            // 
            // panelBrowser
            // 
            panelBrowser.AllowDrop = true;
            panelBrowser.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelBrowser.Location = new Point(0, 3);
            panelBrowser.Name = "panelBrowser";
            panelBrowser.Size = new Size(493, 397);
            panelBrowser.TabIndex = 7;
            panelBrowser.DragDrop += panelBrowser_DragDrop;
            panelBrowser.DragEnter += panelBrowser_DragEnter;
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
            settingsPanel.Controls.Add(lblAvailableFolders);
            settingsPanel.Controls.Add(availableFolders);
            settingsPanel.Controls.Add(panelSettings);
            settingsPanel.Controls.Add(settingsContent);
            settingsPanel.Font = new Font("Bahnschrift SemiLight", 10F);
            settingsPanel.ForeColor = Color.DarkGray;
            settingsPanel.Location = new Point(1, 30);
            settingsPanel.Name = "settingsPanel";
            settingsPanel.Size = new Size(492, 400);
            settingsPanel.TabIndex = 5;
            // 
            // lblAvailableFolders
            // 
            lblAvailableFolders.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblAvailableFolders.Font = new Font("Bahnschrift SemiLight", 9F);
            lblAvailableFolders.Location = new Point(3, 5);
            lblAvailableFolders.Name = "lblAvailableFolders";
            lblAvailableFolders.Size = new Size(486, 38);
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
            availableFolders.Items.AddRange(new object[] { "➕ Add new folder", "⚙️ Settings" });
            availableFolders.Location = new Point(29, 46);
            availableFolders.Name = "availableFolders";
            availableFolders.Size = new Size(434, 29);
            availableFolders.TabIndex = 0;
            availableFolders.SelectedIndexChanged += availableFolders_SelectedIndexChanged;
            // 
            // panelSettings
            // 
            panelSettings.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelSettings.Controls.Add(chkResetWindowSizeOnStartup);
            panelSettings.Controls.Add(chkAutoOpenVideos);
            panelSettings.Controls.Add(chkHideScroll);
            panelSettings.Controls.Add(bResetWindowSize);
            panelSettings.Controls.Add(chkPlayLastUsedTrack);
            panelSettings.Controls.Add(label1);
            panelSettings.Controls.Add(chkAutoloadFolder);
            panelSettings.Controls.Add(chkUseLastTimestamp);
            panelSettings.Location = new Point(11, 93);
            panelSettings.Name = "panelSettings";
            panelSettings.Size = new Size(470, 272);
            panelSettings.TabIndex = 16;
            // 
            // chkResetWindowSizeOnStartup
            // 
            chkResetWindowSizeOnStartup.AutoSize = true;
            chkResetWindowSizeOnStartup.Cursor = Cursors.Hand;
            chkResetWindowSizeOnStartup.FlatAppearance.MouseOverBackColor = Color.DimGray;
            chkResetWindowSizeOnStartup.Location = new Point(18, 201);
            chkResetWindowSizeOnStartup.Name = "chkResetWindowSizeOnStartup";
            chkResetWindowSizeOnStartup.Size = new Size(218, 21);
            chkResetWindowSizeOnStartup.TabIndex = 18;
            chkResetWindowSizeOnStartup.Text = "Reset window size on startup";
            chkResetWindowSizeOnStartup.UseVisualStyleBackColor = true;
            chkResetWindowSizeOnStartup.CheckedChanged += chkResetWindowSizeOnStartup_CheckedChanged;
            // 
            // chkAutoOpenVideos
            // 
            chkAutoOpenVideos.AutoSize = true;
            chkAutoOpenVideos.Cursor = Cursors.Hand;
            chkAutoOpenVideos.FlatAppearance.MouseOverBackColor = Color.DimGray;
            chkAutoOpenVideos.Location = new Point(18, 164);
            chkAutoOpenVideos.Name = "chkAutoOpenVideos";
            chkAutoOpenVideos.Size = new Size(227, 21);
            chkAutoOpenVideos.TabIndex = 17;
            chkAutoOpenVideos.Text = "Auto-open video tab for videos";
            chkAutoOpenVideos.UseVisualStyleBackColor = true;
            chkAutoOpenVideos.CheckedChanged += chkAutoOpenVideos_CheckedChanged;
            // 
            // chkHideScroll
            // 
            chkHideScroll.AutoSize = true;
            chkHideScroll.Cursor = Cursors.Hand;
            chkHideScroll.FlatAppearance.MouseOverBackColor = Color.DimGray;
            chkHideScroll.Location = new Point(18, 127);
            chkHideScroll.Name = "chkHideScroll";
            chkHideScroll.Size = new Size(186, 21);
            chkHideScroll.TabIndex = 16;
            chkHideScroll.Text = "Hide browsing scroll bar";
            chkHideScroll.UseVisualStyleBackColor = true;
            chkHideScroll.CheckedChanged += chkHideScroll_CheckedChanged;
            // 
            // bResetWindowSize
            // 
            bResetWindowSize.Cursor = Cursors.Hand;
            bResetWindowSize.ForeColor = Color.Black;
            bResetWindowSize.Location = new Point(276, 18);
            bResetWindowSize.Name = "bResetWindowSize";
            bResetWindowSize.Size = new Size(176, 38);
            bResetWindowSize.TabIndex = 15;
            bResetWindowSize.Text = "📐 Reset window size";
            bResetWindowSize.UseVisualStyleBackColor = true;
            bResetWindowSize.Click += bResetWindowSize_Click;
            // 
            // chkPlayLastUsedTrack
            // 
            chkPlayLastUsedTrack.AutoSize = true;
            chkPlayLastUsedTrack.Cursor = Cursors.Hand;
            chkPlayLastUsedTrack.FlatAppearance.MouseOverBackColor = Color.DimGray;
            chkPlayLastUsedTrack.Location = new Point(18, 18);
            chkPlayLastUsedTrack.Name = "chkPlayLastUsedTrack";
            chkPlayLastUsedTrack.Size = new Size(206, 21);
            chkPlayLastUsedTrack.TabIndex = 11;
            chkPlayLastUsedTrack.Text = "Play last used track on start";
            chkPlayLastUsedTrack.UseVisualStyleBackColor = true;
            chkPlayLastUsedTrack.CheckedChanged += chkPlayLastUsedTrack_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bahnschrift SemiLight", 12F);
            label1.ForeColor = Color.FromArgb(64, 64, 64);
            label1.Location = new Point(18, 52);
            label1.Name = "label1";
            label1.Size = new Size(23, 19);
            label1.TabIndex = 14;
            label1.Text = "└─";
            // 
            // chkAutoloadFolder
            // 
            chkAutoloadFolder.AutoSize = true;
            chkAutoloadFolder.Cursor = Cursors.Hand;
            chkAutoloadFolder.FlatAppearance.MouseOverBackColor = Color.DimGray;
            chkAutoloadFolder.Location = new Point(18, 90);
            chkAutoloadFolder.Name = "chkAutoloadFolder";
            chkAutoloadFolder.Size = new Size(190, 21);
            chkAutoloadFolder.TabIndex = 12;
            chkAutoloadFolder.Text = "Auto-load topmost folder";
            chkAutoloadFolder.UseVisualStyleBackColor = true;
            chkAutoloadFolder.CheckedChanged += chkAutoloadFolder_CheckedChanged;
            // 
            // chkUseLastTimestamp
            // 
            chkUseLastTimestamp.AutoSize = true;
            chkUseLastTimestamp.Cursor = Cursors.Hand;
            chkUseLastTimestamp.FlatAppearance.MouseOverBackColor = Color.DimGray;
            chkUseLastTimestamp.Location = new Point(47, 53);
            chkUseLastTimestamp.Name = "chkUseLastTimestamp";
            chkUseLastTimestamp.Size = new Size(196, 21);
            chkUseLastTimestamp.TabIndex = 13;
            chkUseLastTimestamp.Text = "Resume where you left off";
            chkUseLastTimestamp.UseVisualStyleBackColor = true;
            chkUseLastTimestamp.CheckedChanged += chkUseLastTimestamp_CheckedChanged;
            // 
            // settingsContent
            // 
            settingsContent.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            settingsContent.Controls.Add(bFactoryReset);
            settingsContent.Controls.Add(panel1);
            settingsContent.Controls.Add(panel2);
            settingsContent.Controls.Add(lblAddress);
            settingsContent.Controls.Add(lblFolderName);
            settingsContent.Controls.Add(bBrowseFolder);
            settingsContent.Controls.Add(bRemoveFolder);
            settingsContent.Location = new Point(11, 93);
            settingsContent.Name = "settingsContent";
            settingsContent.Size = new Size(470, 272);
            settingsContent.TabIndex = 10;
            settingsContent.Visible = false;
            // 
            // bFactoryReset
            // 
            bFactoryReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bFactoryReset.Cursor = Cursors.Hand;
            bFactoryReset.ForeColor = Color.IndianRed;
            bFactoryReset.Location = new Point(138, 179);
            bFactoryReset.Name = "bFactoryReset";
            bFactoryReset.Size = new Size(132, 38);
            bFactoryReset.TabIndex = 10;
            bFactoryReset.Text = "☢️ Factory reset";
            bFactoryReset.UseVisualStyleBackColor = true;
            bFactoryReset.Click += bFactoryReset_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(barAddress);
            panel1.Location = new Point(18, 50);
            panel1.Name = "panel1";
            panel1.Size = new Size(391, 34);
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
            barAddress.PlaceholderText = "The path of the audio folder";
            barAddress.Size = new Size(380, 20);
            barAddress.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(barFolderName);
            panel2.Location = new Point(18, 139);
            panel2.Name = "panel2";
            panel2.Size = new Size(434, 34);
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
            barFolderName.Size = new Size(423, 20);
            barFolderName.TabIndex = 5;
            barFolderName.KeyDown += barFolderName_KeyDown;
            // 
            // lblAddress
            // 
            lblAddress.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblAddress.Font = new Font("Bahnschrift SemiLight", 9F);
            lblAddress.Location = new Point(18, 18);
            lblAddress.Name = "lblAddress";
            lblAddress.Padding = new Padding(5, 0, 0, 0);
            lblAddress.Size = new Size(434, 29);
            lblAddress.TabIndex = 2;
            lblAddress.Text = "🔗 Address";
            lblAddress.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblFolderName
            // 
            lblFolderName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblFolderName.Font = new Font("Bahnschrift SemiLight", 9F);
            lblFolderName.Location = new Point(18, 107);
            lblFolderName.Name = "lblFolderName";
            lblFolderName.Padding = new Padding(5, 0, 0, 0);
            lblFolderName.Size = new Size(434, 29);
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
            bBrowseFolder.Location = new Point(418, 52);
            bBrowseFolder.Name = "bBrowseFolder";
            bBrowseFolder.Size = new Size(30, 30);
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
            bRemoveFolder.Location = new Point(276, 179);
            bRemoveFolder.Name = "bRemoveFolder";
            bRemoveFolder.Size = new Size(176, 38);
            bRemoveFolder.TabIndex = 6;
            bRemoveFolder.Text = "🗑 Remove folder";
            bRemoveFolder.UseVisualStyleBackColor = true;
            bRemoveFolder.Click += bRemoveFolder_Click;
            // 
            // videoToolTip
            // 
            videoToolTip.ToolTipTitle = "File information";
            // 
            // mediaStatus
            // 
            mediaStatus.Anchor = AnchorStyles.None;
            mediaStatus.AutoSize = true;
            mediaStatus.BackColor = Color.FromArgb(45, 45, 45);
            mediaStatus.Font = new Font("Bahnschrift Light", 11F);
            mediaStatus.ForeColor = Color.Gray;
            mediaStatus.Location = new Point(128, 191);
            mediaStatus.Name = "mediaStatus";
            mediaStatus.Size = new Size(239, 18);
            mediaStatus.TabIndex = 1;
            mediaStatus.Text = "🖥 Videos you play will show here!";
            // 
            // mainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLight;
            ClientSize = new Size(493, 503);
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
            MinimumSize = new Size(509, 542);
            Name = "mainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ReFrame";
            FormClosing += mainForm_FormClosing;
            Load += mainForm_Load;
            Shown += mainForm_Shown;
            KeyDown += mainForm_KeyDown;
            controlPanel.ResumeLayout(false);
            resolutionStrip.ResumeLayout(false);
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)mediaViewer).EndInit();
            browserPanel.ResumeLayout(false);
            settingsPanel.ResumeLayout(false);
            panelSettings.ResumeLayout(false);
            panelSettings.PerformLayout();
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
        private Label currentTime;
        private Label endTime;
        private Button bPlayback;
        private Button bMute;
        private Button bRepeat;
        private Panel browserPanel;
        private Button bDrawer;
        private Button bSettings;
        private Panel settingsPanel;
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
        private Button bFactoryReset;
        private CheckBox chkPlayLastUsedTrack;
        private CheckBox chkAutoloadFolder;
        private Label label1;
        private CheckBox chkUseLastTimestamp;
        private ReFrameSlider timestamp;
        private ReFrameVolumeSlider volumeSlider;
        private LibVLCSharp.WinForms.VideoView mediaViewer;
        private Button bResetWindowSize;
        private Button bResizeWindow;
        private ContextMenuStrip resolutionStrip;
        private ToolStripMenuItem resolution1;
        private ToolStripMenuItem resolution2;
        private ToolStripMenuItem resolution3;
        private ToolStripMenuItem resetRes;
        private Panel panelSettings;
        private CheckBox chkHideScroll;
        private Panel panelSeparator1;
        private CheckBox chkAutoOpenVideos;
        private CheckBox chkResetWindowSizeOnStartup;
        private Label mediaStatus;
    }
}
