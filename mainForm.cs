using NAudio.Wave;
using System.Text.Json.Nodes;
using System.Timers;
using Timer = System.Timers.Timer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

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

        private Timer playbackTimer;
        private WaveOutEvent waveOut;
        private AudioFileReader audioFileReader;

        public mainForm()
        {
            InitializeComponent();
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
                            }
                        }
                    }
                }
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
            if (volumeSlider.Value > 0)
            {
                Properties.Settings.Default.audioVolume = volumeSlider.Value;
            }

            Properties.Settings.Default.appLocationX = this.Location.X;
            Properties.Settings.Default.appLocationY = this.Location.Y;
            Properties.Settings.Default.appSizeWidth = this.Width;
            Properties.Settings.Default.appSizeHeight = this.Height;

            Properties.Settings.Default.Save();

            if (playbackTimer != null)
            {
                playbackTimer.Elapsed -= PlaybackTimer_Elapsed;
                playbackTimer.Stop();
                playbackTimer.Dispose();
                playbackTimer = null;
            }
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

                        if (matchFolder == barFolder)
                        {
                            string content = "Would you like to remove " + Path.GetFileName(barFolderName.Text) + "? This action is irreversible.";
                            if (MessageBox.Show(content, Text) == DialogResult.Yes)
                            {
                                barFolderName.Text = string.Empty;
                                barAddress.Text = string.Empty;
                                availableFolders.Items.Remove(matchFolder);
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
    }
}
