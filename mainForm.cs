using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Text.Json.Nodes;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace ReFrameAudio
{
    public partial class mainForm : Form
    {
        public string audioBaseConfig =
            "{" +
            "   \"folders\": []" +
            "}";

        private bool isPaused = true;
        private bool isBrowserOpen = false;
        private bool isSettingsOpen = false;

        public Color listBackcolor = Color.FromArgb(255, 32, 34, 36);
        public Color listSelectedcolor = Color.FromArgb(255, 42, 44, 46);
        public Color listHovercolor = Color.FromArgb(255, 46, 48, 50);

        public Color borderColor = Color.FromArgb(255, 100, 100, 100);
        public Color borderColorActive = Color.DodgerBlue;

        private Timer playbackTimer;
        private WaveOutEvent waveOut;
        private AudioFileReader audioFileReader;

        public mainForm()
        {
            InitializeComponent();
        }

        private void populateDropdowns()
        {
            availableFolders.Items.Clear();
            browseFolders.Items.Clear();

            availableFolders.Items.Add("➕ Add new folder");

            Debug.WriteLine(Properties.Settings.Default.audioFolders);
            JObject contentObject = JObject.Parse(Properties.Settings.Default.audioFolders);
            if (contentObject != null)
            {
                JArray foldersArray = (JArray?)contentObject["folders"] ?? new JArray();
                if (foldersArray.Count > 0)
                {
                    foreach (var folder in foldersArray)
                    {
                        if (folder is JObject obj)
                        {
                            string? alias = obj["alias"]?.ToString();
                            string? path = obj["path"]?.ToString();

                            if (string.IsNullOrEmpty(alias))
                            {
                                return;
                            }

                            availableFolders.Items.Add(alias);
                            browseFolders.Items.Add(alias);
                        }
                    }
                }
            }
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.audioFolders))
            {
                Properties.Settings.Default.audioFolders = audioBaseConfig;
                Properties.Settings.Default.Save();
            }

            if (Properties.Settings.Default.audioFolders != audioBaseConfig &&
                !string.IsNullOrEmpty(Properties.Settings.Default.audioFolders))
            {
                populateDropdowns();
            }

            if (Properties.Settings.Default.audioVolume > 0)
            {
                volumeSlider.Value = Properties.Settings.Default.audioVolume;
                volumeStatus.Text = volumeSlider.Value.ToString() + "%";
            }

            volumeStatus.Text = volumeSlider.Value.ToString() + "%";

            if (Properties.Settings.Default.appLocationX != 0 && Properties.Settings.Default.appLocationY != 0)
            {
                this.Location = new Point(Properties.Settings.Default.appLocationX, Properties.Settings.Default.appLocationY);
            }

            if (Properties.Settings.Default.appSizeWidth != 0 && Properties.Settings.Default.appSizeHeight != 0)
            {
                this.Size = new Size(Properties.Settings.Default.appSizeWidth, Properties.Settings.Default.appSizeHeight);
            }

            mainPanel.MouseWheel += mainPanel_MouseWheel;
            notice.DragDrop += mainPanel_DragDrop;
            notice.DragEnter += mainPanel_DragEnter;

            bRemoveFolder.Visible = false;
            mainPanel.BringToFront();

            currentAudioFiles.Layout += (s, e) =>
            {
                currentAudioFiles.HorizontalScroll.Value = 0;
                currentAudioFiles.VerticalScroll.Value = 1;
                currentAudioFiles.PerformLayout();
            };
        }

        private void listAudioFiles(string[] audioFiles)
        {
            currentAudioFiles.Controls.Clear();
            List<Button> list = new List<Button>();

            for (int i = 0; i < audioFiles.Length; i++)
            {
                Button newFile = new Button();
                newFile.AutoSize = false;
                newFile.Name = $"audioFile{i}";
                newFile.Font = new Font("Bahnschrift", 11, FontStyle.Regular);
                newFile.Text = Path.GetFileName(audioFiles[i]);
                newFile.Tag = audioFiles[i]; // Store the file path in Tag property
                newFile.ForeColor = Color.DarkGray;
                newFile.BackColor = Color.FromArgb(32, 34, 36);
                newFile.FlatAppearance.BorderColor = Color.FromArgb(100, 100, 100);
                newFile.FlatAppearance.BorderSize = 0;
                newFile.FlatAppearance.MouseDownBackColor = Color.FromArgb(42, 44, 46);
                newFile.FlatAppearance.MouseOverBackColor = Color.FromArgb(46, 48, 50);
                newFile.MouseDown += new MouseEventHandler(btn_MouseDown);
                newFile.MouseDoubleClick += new MouseEventHandler(btn_MouseDoubleClick);
                // newFile.MouseEnter += new EventHandler(lbl_MouseEnter);
                // newFile.MouseLeave += new EventHandler(lbl_MouseLeave);
                // newFile.MouseUp += new MouseEventHandler(lbl_MouseUp);
                newFile.FlatStyle = FlatStyle.Flat;
                newFile.TextAlign = ContentAlignment.MiddleLeft;
                newFile.Margin = new Padding(0, 1, 0, 0);
                newFile.Padding = new Padding(10, 0, 0, 0);

                newFile.Size = new Size(currentAudioFiles.Size.Width, 32);
                newFile.Cursor = Cursors.Hand;

                list.Add(newFile);
            }

            Control[] allTracks = list.ToArray();
            currentAudioFiles.Controls.AddRange(allTracks);
            list.Clear();
        }

        private void lbl_MouseEnter(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            if (btn.Text != "")
            {
                if (btn.BackColor != listSelectedcolor)
                {
                    btn.BackColor = listHovercolor;
                }
            }
        }

        private void lbl_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            if (btn.Text != "")
            {
                btn.BackColor = listBackcolor;
                /*
                if (label.BackColor != listSelectedcolor &&
                    label.BackColor == listHovercolor)
                {
                }
                */
            }
        }

        private void btn_MouseDown(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;

            if (btn.Text != "")
            {
                if (e.Clicks == 2)
                {
                    btn_MouseDoubleClick(sender, e);
                }
            }
        }

        private void btn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            if (btn.Text != "")
            {
                string? filePath = btn.Tag?.ToString();
                Debug.WriteLine(filePath);
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("There's no valid path to fetch from the item.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!File.Exists(filePath))
                {
                    MessageBox.Show("The selected audio file does not exist.", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                stopAudio();
                playAudio(filePath);
            }
        }

        private void lbl_MouseUp(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            if (btn.Text != "")
            {
                // label.BackColor = listHovercolor;
            }
        }

        private void listAudioFiles()
        {
            if (browseFolders.SelectedItem != null)
            {
                string? selectedFolder = browseFolders.SelectedItem?.ToString();
                JObject contentObject = JObject.Parse(Properties.Settings.Default.audioFolders);
                JArray foldersArray = (JArray?)contentObject["folders"] ?? new JArray();

                foreach (var folder in foldersArray)
                {
                    if (folder is JObject obj)
                    {
                        string? alias = obj["alias"]?.ToString();
                        string? path = obj["path"]?.ToString();
                        if (alias == selectedFolder && !string.IsNullOrEmpty(path) && Directory.Exists(path))
                        {
                            string[] audioFiles = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                                .Where(file => isValidAudio(file))
                                .OrderByDescending(file => File.GetLastWriteTime(file))
                                .ToArray();

                            listAudioFiles(audioFiles);
                        }
                    }
                }
            }
        }

        private void mainPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) // Scrolling up
            {
                if (volumeSlider.Value < volumeSlider.Maximum)
                {
                    volumeSlider.Value += 5; // Increase volume by 5%
                }
            }
            else if (e.Delta < 0) // Scrolling down
            {
                if (volumeSlider.Value > volumeSlider.Minimum)
                {
                    volumeSlider.Value -= 5; // Decrease volume by 5%
                }
            }

            volumeStatus.Text = volumeSlider.Value.ToString() + "%";
            if (waveOut != null && isPaused == false)
            {
                waveOut.Volume = (float)volumeSlider.Value / 100f; // Set volume based on slider value
            }
        }

        private void volumeSlider_Scroll(object sender, EventArgs e)
        {
            volumeStatus.Text = volumeSlider.Value.ToString() + "%";

            if (waveOut != null && isPaused == false)
            {
                waveOut.Volume = (float)volumeSlider.Value / 100f; // Set volume based on slider value
            }
        }

        private bool isValidAudio(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLower();
            return extension == ".mp3" || extension == ".wav";
        }

        private void stopAudio()
        {
            if (playbackTimer != null)
            {
                playbackTimer.Elapsed -= PlaybackTimer_Elapsed;
                playbackTimer.Stop();
                playbackTimer.Dispose();
                playbackTimer = null;
            }

            if (waveOut != null)
            {
                waveOut.PlaybackStopped -= WaveOut_PlaybackStopped;
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }

            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
                audioFileReader.Close();
                audioFileReader = null;
            }
        }

        private void playAudio(string filePath)
        {
            Properties.Settings.Default.currentFileName = Path.GetFileName(filePath);
            Properties.Settings.Default.currentFilePath = filePath;
            Properties.Settings.Default.Save();

            audioFileReader = new AudioFileReader(filePath);
            waveOut = new WaveOutEvent();
            waveOut.Init(audioFileReader);
            waveOut.Volume = (float)volumeSlider.Value / 100f; // Set volume based on slider value
            waveOut.PlaybackStopped += WaveOut_PlaybackStopped;

            timestamp.Maximum = (int)audioFileReader.TotalTime.TotalSeconds;
            timestamp.Value = 0;

            endTime.Text = audioFileReader.TotalTime.ToString(@"mm\:ss");

            playbackTimer = new Timer();
            playbackTimer.Interval = 500; // update every 0.5 seconds
            playbackTimer.Elapsed += PlaybackTimer_Elapsed;

            playbackTimer.Start();

            waveOut.Play();
            isPaused = false;

            Text = "ReFrame" + " - " + Path.GetFileName(filePath);
        }

        private void PlaybackTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
            {
                if (!this.IsDisposed && !this.Disposing)
                {
                    if (audioFileReader != null)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            timestamp.Value = Math.Min((int)audioFileReader.CurrentTime.TotalSeconds, timestamp.Maximum);
                            currentTime.Text = audioFileReader.CurrentTime.ToString(@"mm\:ss");
                        });
                    }
                }
            }
        }

        private void mainPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                    if (files == null || files.Length == 0)
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }

                    if (files.Length == 1)
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
        }

        private void mainPanel_DragDrop(object sender, DragEventArgs e)
        {

            if (e.Data != null)
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files == null || files.Length == 0)
                {
                    return;
                }

                if (files.Length == 1)
                {
                    string audioFilePath = files[0];

                    bool isRealPath = File.Exists(audioFilePath);
                    if (isRealPath)
                    {
                        stopAudio();
                        playAudio(audioFilePath);
                    }
                }
            }
        }

        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs args)
        {
            if (Properties.Settings.Default.audioRepeat)
            {
                if (audioFileReader != null)
                {
                    playAudio(audioFileReader.FileName);
                }
            }
            else
            {
                stopAudio();
                Text = "ReFrame";
            }
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopAudio();

            if (volumeSlider.Value > 0)
            {
                Properties.Settings.Default.audioVolume = volumeSlider.Value;
            }

            Properties.Settings.Default.appLocationX = this.Location.X;
            Properties.Settings.Default.appLocationY = this.Location.Y;
            Properties.Settings.Default.appSizeWidth = this.Width;
            Properties.Settings.Default.appSizeHeight = this.Height;

            Properties.Settings.Default.Save();
        }

        private void bPlayback_Click(object sender, EventArgs e)
        {
            if (waveOut != null && waveOut.PlaybackState == PlaybackState.Paused)
            {
                waveOut.Play();
                isPaused = true;
            }
            else if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Pause();
                isPaused = false;
            }
            else
            {
                MessageBox.Show("No audio is currently playing.", "Playback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bRepeat_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.audioRepeat)
            {
                Properties.Settings.Default.audioRepeat = false;
                bRepeat.BackgroundImage = Properties.Resources.nonloop;
            }
            else
            {
                Properties.Settings.Default.audioRepeat = true;
                bRepeat.BackgroundImage = Properties.Resources.loop;
            }
        }

        private void timestamp_Scroll(object sender, EventArgs e)
        {
            if (audioFileReader != null)
            {
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(timestamp.Value);
            }
        }

        private void bDrawer_Click(object sender, EventArgs e)
        {
            if (isSettingsOpen)
            {
                if (!isBrowserOpen)
                {
                    browserPanel.BringToFront();
                    isBrowserOpen = true;
                }
                else
                {
                    mainPanel.BringToFront();
                    isBrowserOpen = false;
                }

                isSettingsOpen = false;
            }
            else
            {
                if (!isBrowserOpen)
                {
                    browserPanel.BringToFront();
                    isBrowserOpen = true;
                }
                else
                {
                    mainPanel.BringToFront();
                    isBrowserOpen = false;
                }
            }
        }

        private void bSettings_Click(object sender, EventArgs e)
        {
            if (isSettingsOpen)
            {
                settingsPanel.SendToBack();
                isSettingsOpen = false;
                return;
            }

            settingsPanel.BringToFront();
            isSettingsOpen = true;
            isBrowserOpen = true;
        }

        private void bRemoveFolder_Click(object sender, EventArgs e)
        {
            if (bRemoveFolder.Text.Contains("➕"))
            {
                string? matchFolder = availableFolders.SelectedItem?.ToString();
                string? addressPath = barAddress.Text.Trim();
                string? barFolder = barFolderName.Text.Trim();

                if (string.IsNullOrEmpty(matchFolder))
                {
                    return;
                }

                if (string.IsNullOrEmpty(addressPath))
                {
                    return;
                }

                if (string.IsNullOrEmpty(barFolder))
                {
                    return;
                }

                string targetPath = addressPath;
                string targetAlias = barFolder;

                //
                JObject configObject = JObject.Parse(Properties.Settings.Default.audioFolders);
                JArray folderArray = (JArray?)configObject["folders"] ?? new JArray();

                bool folderExists = folderArray.Any(folder =>
                    folder is JObject obj &&
                    string.Equals((string?)obj["path"], targetPath, StringComparison.OrdinalIgnoreCase)
                );

                if (folderExists)
                {
                    MessageBox.Show("Folder path of " + barFolder + " already exists in the database! Try another path.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    bool aliasExists = folderArray.Any(folder =>
                       folder is JObject obj &&
                       string.Equals((string?)obj["alias"], targetAlias, StringComparison.OrdinalIgnoreCase)
                    );

                    if (aliasExists)
                    {
                        MessageBox.Show("Alias " + targetAlias + " already exists in the database! Try another path.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                JObject newFolder = new JObject
                {
                    ["alias"] = barFolder,
                    ["path"] = @addressPath
                };

                folderArray.Add(newFolder);
                configObject["folders"] = folderArray;

                string updatedConfig = configObject.ToString(Formatting.Indented);
                Properties.Settings.Default.audioFolders = updatedConfig;
                Properties.Settings.Default.Save();

                barAddress.Text = string.Empty;
                barFolderName.Text = string.Empty;

                populateDropdowns();
                MessageBox.Show("Folder " + barFolder + " successfully added to the database!", Text, MessageBoxButtons.OK);
            }
            else
            {
                if (barFolderName.Text.Length > 0)
                {
                    if (availableFolders.Items.Count > 0)
                    {
                        string? matchFolder = availableFolders.SelectedItem?.ToString();
                        string? barFolder = barFolderName.Text.Trim();

                        string content = "Would you like to remove " + matchFolder + "? This action is irreversible.";
                        if (MessageBox.Show(content, Text, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            Debug.WriteLine("success 1");
                            barFolderName.Text = string.Empty;
                            barAddress.Text = string.Empty;
                            availableFolders.Items.Remove(matchFolder);

                            JObject configObject = JObject.Parse(Properties.Settings.Default.audioFolders);
                            JArray folderArray = (JArray?)configObject["folders"] ?? new JArray();

                            string? targetRemovalItem = matchFolder?.Trim();
                            JToken? folderRemoval = folderArray.FirstOrDefault(folder =>
                                folder is JObject obj &&
                                string.Equals((string?)obj["alias"], targetRemovalItem, StringComparison.OrdinalIgnoreCase)
                            );

                            if (folderRemoval != null && folderRemoval is JObject folderObj)
                            {
                                folderArray.Remove(folderObj);
                                Properties.Settings.Default.audioFolders = configObject.ToString(Formatting.Indented);
                                Properties.Settings.Default.Save();

                                availableFolders.Select();
                            }
                        }
                    }
                }
            }
        }

        private void availableFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? selectedFolder = availableFolders.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedFolder))
            {
                barFolderName.Text = string.Empty;
                barAddress.Text = string.Empty;
                bRemoveFolder.Visible = false;

                barAddress.Enabled = false;
                barFolderName.Enabled = false;
                bBrowseFolder.Enabled = false;
                bRemoveFolder.Enabled = false;

                return;
            }
            else
            {
                barAddress.Enabled = true;
                barFolderName.Enabled = true;
                bBrowseFolder.Enabled = true;
                bRemoveFolder.Enabled = true;

                settingsContent.Visible = true;
            }

            if (selectedFolder.ToLower().Contains("add"))
            {
                bRemoveFolder.Text = "➕ Add new folder";
                bRemoveFolder.Visible = true;

                barAddress.Select();
            }
            else
            {
                JObject contentObject = JObject.Parse(Properties.Settings.Default.audioFolders);
                if (contentObject != null)
                {
                    JArray foldersArray = (JArray?)contentObject["folders"] ?? new JArray();
                    if (foldersArray.Count > 0)
                    {
                        foreach (var folder in foldersArray)
                        {
                            if (folder is JObject obj)
                            {
                                string? alias = obj["alias"]?.ToString();
                                string? path = obj["path"]?.ToString();

                                if (!string.IsNullOrEmpty(alias) && !string.IsNullOrEmpty(path))
                                {
                                    barAddress.Text = path;
                                    barFolderName.Text = alias;
                                }
                            }
                        }
                    }
                }

                bRemoveFolder.Text = "🗑 Remove folder";
                bRemoveFolder.Visible = true;
            }
        }

        private void bDrawer_MouseEnter(object sender, EventArgs e)
        {
            bDrawer.BackgroundImage = Properties.Resources.highlightbars;
        }

        private void bDrawer_MouseLeave(object sender, EventArgs e)
        {
            bDrawer.BackgroundImage = Properties.Resources.bars;
        }

        private void bSettings_MouseEnter(object sender, EventArgs e)
        {
            bSettings.BackgroundImage = Properties.Resources.highlightaltsettings;
        }

        private void bSettings_MouseLeave(object sender, EventArgs e)
        {
            bSettings.BackgroundImage = Properties.Resources.altsettings;
        }

        private void bBrowseFolder_Click(object sender, EventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.Description = "Select an audio folder";
                dialog.UseDescriptionForTitle = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = dialog.SelectedPath;

                    bool folderExists = Directory.Exists(selectedPath);
                    if (!folderExists)
                    {
                        MessageBox.Show("The selected folder " + Path.GetFileName(selectedPath) + " does not exist.", "Folder Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        selectedPath = string.Empty;
                        return;
                    }

                    barAddress.Text = selectedPath;
                    barFolderName.Select();
                }
            }
        }

        private void browseFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            listAudioFiles();
        }

        private void currentAudioFiles_SizeChanged(object sender, EventArgs e)
        {
            int buttonWidth = currentAudioFiles.ClientSize.Width; // Adjust as needed for padding/scrollbar
            foreach (Control ctrl in currentAudioFiles.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Width = buttonWidth;
                }
            }
        }

        private void panelSeparator1_Paint(object sender, PaintEventArgs e)
        {
            int y = panelSeparator1.Height / 2;
            using (Pen pen = new Pen(Color.FromArgb(100, 100, 100), 1)) // Change color and thickness as needed
            {
                e.Graphics.DrawLine(pen, 0, y, panelSeparator1.Width, y);
            }
        }
    }
}
