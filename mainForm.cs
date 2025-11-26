using NAudio.Utils;
using NAudio.Wave;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json.Nodes;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using Timer = System.Timers.Timer;

namespace ReFrameAudio
{
    public partial class mainForm : Form
    {
        public string audioBaseConfig =
            "{" +
            "   \"folders\": []" +
            "}";
        public string currentPlayingFile = string.Empty;

        private bool isStopped = true;
        private bool isPaused = true;
        private bool isBrowserOpen = false;
        private bool isSettingsOpen = false;
        private bool isDragging = false;
        private bool shouldSwitchOnPlay = false;

        public Color listBackcolor = Color.FromArgb(255, 32, 34, 36);
        public Color listSelectedcolor = Color.FromArgb(255, 42, 44, 46);
        public Color listHovercolor = Color.FromArgb(255, 46, 48, 50);

        public Color borderColor = Color.FromArgb(255, 100, 100, 100);
        public Color borderColorActive = Color.DodgerBlue;

        private Timer playbackTimer;
        private WaveOutEvent waveOut;
        private AudioFileReader audioFileReader;

        // browser variables
        private List<string> audioFiles = new List<string>();
        private int itemHeight = 35; // customizable from interface
        private float itemFontSize = 10f; // customizable from interface
        private int hoveredIndex = -1;   // index of item under mouse
        private int selectedIndex = -1;  // optional selection

        private void attachMouseEvent()
        {
            foreach (Control control in this.Controls)
            {
                if (control is Panel pnl)
                {
                    pnl.MouseWheel += mainPanel_MouseWheel;
                }
            }
        }

        private void attachKeyboardEvent()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Panel pnl)
                {
                    pnl.KeyDown += (s, e) =>
                    {
                        if (e.KeyCode == Keys.Space)
                        {
                            bPlayback_Click(s, e);
                            e.Handled = true;
                        }
                    };
                }
            }
        }

        public mainForm()
        {
            InitializeComponent();
            this.MouseWheel += mainPanel_MouseWheel;
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
            panelBrowser.GetType()
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                ?.SetValue(panelBrowser, true, null);

            if (string.IsNullOrEmpty(Properties.Settings.Default.audioFolders))
            {
                Properties.Settings.Default.audioFolders = audioBaseConfig;
                Properties.Settings.Default.Save();
            }

            if (Properties.Settings.Default.audioFolders != audioBaseConfig && !string.IsNullOrEmpty(Properties.Settings.Default.audioFolders))
            {
                populateDropdowns();
            }

            if (Properties.Settings.Default.audioVolume > 0)
            {
                float initialVolume = Properties.Settings.Default.audioVolume;
                int volume_rounded = (int)Math.Round(initialVolume);
                float multipliedVolume = multiplyVolume(volume_rounded);
                volumeStatus.Text = volume_rounded.ToString() + "%";
                volumeSlider.Value = volume_rounded;
            }
            else
            {
                float volume = 15.0f;
                int volume_rounded = (int)Math.Round(volume);
                float multipliedVolume = multiplyVolume(volume);
                volumeStatus.Text = volume.ToString() + "%";
                volumeSlider.Value = volume_rounded;
            }

            if (Properties.Settings.Default.appLocationX != 0 && Properties.Settings.Default.appLocationY != 0)
            {
                this.Location = new Point(Properties.Settings.Default.appLocationX, Properties.Settings.Default.appLocationY);
            }

            if (Properties.Settings.Default.appSizeWidth != 0 && Properties.Settings.Default.appSizeHeight != 0)
            {
                this.Size = new Size(Properties.Settings.Default.appSizeWidth, Properties.Settings.Default.appSizeHeight);
            }

            if (Properties.Settings.Default.audioRepeat)
            {
                bRepeat.BackgroundImage = Properties.Resources.loop;
            }
            else
            {
                bRepeat.BackgroundImage = Properties.Resources.nonloop;
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.lastFile))
            {
                mainPanel.Tag = Properties.Settings.Default.lastFile;
            }

            if (Properties.Settings.Default.switchPage)
            {
                bSwitchPageOnPlay.BackgroundImage = Properties.Resources.flip_selected;
            }
            else
            {
                bSwitchPageOnPlay.BackgroundImage = Properties.Resources.flip;
            }

            shouldSwitchOnPlay = Properties.Settings.Default.switchPage;
            notice.DragDrop += mainPanel_DragDrop;
            notice.DragEnter += mainPanel_DragEnter;

            bRemoveFolder.Visible = false;
            mainPanel.BringToFront();

            panelBrowser.AutoScroll = true;
            panelBrowser.Paint += panelBrowser_Paint;
            panelBrowser.MouseMove += panelBrowser_MouseMove;
            panelBrowser.MouseLeave += panelBrowser_MouseLeave;
            panelBrowser.MouseClick += panelBrowser_MouseClick;
            panelBrowser.MouseDoubleClick += panelBrowser_MouseDoubleClick;
        }

        private void customizeItem(int height, float fontSize)
        {
            itemHeight = height;
            itemFontSize = fontSize;

            panelBrowser.AutoScrollMinSize = new Size(0, audioFiles.Count * itemHeight);
            panelBrowser.Invalidate();
        }

        private void panelBrowser_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Color.FromArgb(28, 30, 32));
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            int startIndex = panelBrowser.VerticalScroll.Value / itemHeight;
            int visibleCount = panelBrowser.ClientSize.Height / itemHeight + 2;

            string formTitle = Text;

            for (int i = startIndex; i < startIndex + visibleCount && i < audioFiles.Count; i++)
            {
                Rectangle itemRect = new Rectangle(0, i * itemHeight - panelBrowser.VerticalScroll.Value,
                                                    panelBrowser.Width, itemHeight);

                bool isHovered = (i == hoveredIndex);
                bool isSelected = (i == selectedIndex);

                string filename = Path.GetFileName(audioFiles[i]);
                bool isCurrentFile = formTitle.Contains(filename);

                // 1. Background hover/selection
                Color backColor = isSelected
                    ? Color.FromArgb(40, 42, 44)    // selected (slightly lighter)
                    : isHovered
                        ? Color.FromArgb(34, 36, 38) // hovered
                        : Color.FromArgb(28, 30, 32); // normal

                if (isCurrentFile)
                {
                    backColor = Color.FromArgb(40, 42, 44);
                }

                // 2 Fill background
                using (Brush backBrush = new SolidBrush(backColor))
                {
                    e.Graphics.FillRectangle(backBrush, itemRect);
                }

                // 3 Draw text
                Color foreColor = isSelected ? Color.DodgerBlue : Color.Silver;

                if (isCurrentFile)
                {
                    foreColor = Color.DodgerBlue;
                }

                using (Brush textBrush = new SolidBrush(foreColor))
                using (Font font = new Font("Bahnschrift Light", itemFontSize, FontStyle.Regular))
                {
                    StringFormat sf = new StringFormat
                    {
                        LineAlignment = StringAlignment.Center,
                        Trimming = StringTrimming.None,
                        FormatFlags = StringFormatFlags.NoWrap
                    };

                    e.Graphics.DrawString(Path.GetFileName(audioFiles[i]), font, textBrush,
                                            new RectangleF(itemRect.X + 26, itemRect.Y + 2, itemRect.Width - 10, itemRect.Height), sf);
                }
            }
        }

        private void panelBrowser_MouseMove(object sender, MouseEventArgs e)
        {
            int index = (e.Y + panelBrowser.VerticalScroll.Value) / itemHeight;
            if (index != hoveredIndex && index >= 0 && index < audioFiles.Count)
            {
                hoveredIndex = index;
                panelBrowser.Cursor = Cursors.Hand;
                panelBrowser.Invalidate();

                videoToolTip.Show(audioFiles[index], this, e.Location.X + 10);
            }
            else
            {
                videoToolTip.Hide(this);
            }
        }

        private void panelBrowser_MouseLeave(object sender, EventArgs e)
        {
            hoveredIndex = -1;
            panelBrowser.Invalidate();
        }

        private void panelBrowser_MouseClick(object sender, MouseEventArgs e)
        {
            /*
            int index = (e.Y + panelBrowser.VerticalScroll.Value) / itemHeight;
            if (index >= 0 && index < audioFiles.Count)
            {
                selectedIndex = index;
                string selectedFile = audioFiles[selectedIndex];
                mainPanel.Tag = selectedFile;
                stopAudio();
                playAudio(selectedFile);
                bPlayback.BackgroundImage = Properties.Resources.pause;
                isPaused = false;
                isStopped = false;
                panelBrowser.Invalidate();
            }
            */
        }

        private void panelBrowser_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = (e.Y + panelBrowser.VerticalScroll.Value) / itemHeight;
            if (index >= 0 && index < audioFiles.Count)
            {
                initializeBrowserDoubleClick(index);
            }
        }

        private void initializeBrowserDoubleClick(int index)
        {
            playViaTrack(index);

            if (Properties.Settings.Default.switchPage)
            {
                mainPanel.BringToFront();
                isBrowserOpen = false;
                isSettingsOpen = false;
            }
        }

        private void playViaTrack(int index)
        {
            selectedIndex = index;
            string selectedFile = audioFiles[selectedIndex];
            mainPanel.Tag = selectedFile;
            stopAudio();
            playAudio(selectedFile);
            bPlayback.BackgroundImage = Properties.Resources.pause;
            isPaused = false;
            isStopped = false;
            panelBrowser.Invalidate();
        }

        private void playSelection(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }
            if (!File.Exists(fileName))
            {
                MessageBox.Show("The selected audio file does not exist.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int index = audioFiles.IndexOf(fileName);

            if (index == -1) return;

            selectedIndex = index;
            currentPlayingFile = fileName;

            mainPanel.Tag = fileName;
            stopAudio();
            playAudio(fileName);
            bPlayback.BackgroundImage = Properties.Resources.pause;
            isPaused = false;
            isStopped = false;
            panelBrowser.Invalidate();
        }

        private async Task listAudioFiles(string[] files)
        {
            audioFiles.Clear();
            panelBrowser.Controls.Clear();

            selectedIndex = -1;
            audioFiles = files.ToList();
            panelBrowser.AutoScrollMinSize = new Size(0, audioFiles.Count * itemHeight);

            /*
            Button[] allButtons = await Task.Run(() =>
            {
                List<Button> list = new List<Button>();

                for (int i = 0; i < files.Length; i++)
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
                    newFile.DoubleClick += new EventHandler(btn_DoubleClick);
                    newFile.FlatStyle = FlatStyle.Flat;
                    newFile.TextAlign = ContentAlignment.MiddleLeft;
                    newFile.Margin = new Padding(0, 1, 0, 0);
                    newFile.Padding = new Padding(10, 0, 0, 0);
                    newFile.Size = new Size(panelBrowser.Size.Width, 32);
                    newFile.Cursor = Cursors.Hand;

                    list.Add(newFile);
                }

                return list.ToArray();
            });

            panelBrowser.Controls.AddRange(allButtons);
            */
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

        private void btn_DoubleClick(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            if (btn.Text != "")
            {
                Debug.WriteLine("double click");
            }
        }

        private void btn_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)sender;
            if (btn.Text != "")
            {
                string? filePath = btn.Tag?.ToString();
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

                mainPanel.Tag = filePath;
                stopAudio();
                playAudio(filePath);
                bPlayback.BackgroundImage = Properties.Resources.pause;
                isPaused = false;
                isStopped = false;

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

        private async Task browseAudioFiles()
        {
            if (browseFolders.SelectedItem != null)
            {
                string? selectedFolder = browseFolders.SelectedItem?.ToString();
                JObject contentObject = JObject.Parse(Properties.Settings.Default.audioFolders);
                JArray foldersArray = (JArray?)contentObject["folders"] ?? new JArray();

                for (int i = 0; i < foldersArray.Count; i++)
                {
                    if (foldersArray[i] is JObject obj)
                    {
                        string? alias = obj["alias"]?.ToString();
                        string? path = obj["path"]?.ToString();

                        if (string.IsNullOrEmpty(alias) || string.IsNullOrEmpty(path) || !Directory.Exists(path))
                        {
                            continue;
                        }

                        if (alias == selectedFolder)
                        {
                            string[] extensions = new[]
                            { "*.wav",
                              "*.mp3",
                              "*.ogg", 
                              "*.flac", 
                              "*.mp4", 
                              "*.mkv", 
                              "*.mov" };

                            var audioFiles = extensions
                                .SelectMany(ext => Directory.GetFiles(path, ext, SearchOption.TopDirectoryOnly))
                                .Where(file => isValidAudio(file))
                                .OrderByDescending(file => File.GetLastWriteTime(file))
                                .ToArray();

                            await listAudioFiles(audioFiles);
                            break;
                        }
                    }
                }
            }
        }

        private void mainPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (volumeSlider.Value < volumeSlider.Maximum)
                {
                    volumeSlider.Value = Math.Min(volumeSlider.Value + 5, volumeSlider.Maximum);
                }
            }
            else if (e.Delta < 0)
            {
                if (volumeSlider.Value > volumeSlider.Minimum)
                {
                    volumeSlider.Value = Math.Max(volumeSlider.Value - 5, volumeSlider.Minimum);
                }
            }

            updateVolume();
        }

        private void updateVolume()
        {
            float volume = volumeSlider.Value;
            volumeStatus.Text = volume.ToString() + "%";

            if (waveOut != null && isPaused == false)
            {
                waveOut.Volume = multiplyVolume(volume); // Your function already handles 0%
            }
        }

        public static float multiplyVolume(float volumePercent)
        {
            if (volumePercent <= 0.0f) return 0.0f;

            float normalized = volumePercent / 100.0f;
            float mindB = -40.0f;
            float maxdB = 0.0f;

            float dB = mindB + (maxdB - mindB) * normalized;
            return (float)Math.Pow(10.0f, dB / 20.0f);
        }

        private void volumeSlider_Scroll(object sender, EventArgs e)
        {
            updateVolume();
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

            currentTime.Text = "00:00";
            endTime.Text = "00:00";
            timestamp.Maximum = 100;
            timestamp.Value = 0;
        }

        private void playAudio(string filePath)
        {
            Properties.Settings.Default.currentFileName = Path.GetFileName(filePath);
            Properties.Settings.Default.currentFilePath = filePath;
            Properties.Settings.Default.Save();

            audioFileReader = new AudioFileReader(filePath);
            waveOut = new WaveOutEvent();
            waveOut.Init(audioFileReader);
            waveOut.PlaybackStopped += WaveOut_PlaybackStopped;
            waveOut.Volume = multiplyVolume((float)volumeSlider.Value);
            volumeStatus.Text = volumeSlider.Value.ToString() + "%";

            timestamp.Maximum = (int)audioFileReader.TotalTime.TotalMilliseconds;
            timestamp.TickFrequency = timestamp.Maximum / 100;
            timestamp.Value = 0;

            endTime.Text = audioFileReader.TotalTime.ToString(@"mm\:ss");

            playbackTimer = new Timer();
            playbackTimer.Interval = 30;
            playbackTimer.Elapsed += PlaybackTimer_Elapsed;

            playbackTimer.Start();
            waveOut.Play();

            isPaused = false;
            Text = Path.GetFileName(filePath) + " - ReFrame";
        }

        private void PlaybackTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (waveOut != null && waveOut.PlaybackState == PlaybackState.Playing)
                {
                    if (!this.IsDisposed && !this.Disposing)
                    {
                        if (audioFileReader != null)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                int newValue = (int)audioFileReader.CurrentTime.TotalMilliseconds;
                                timestamp.Value = Math.Max(timestamp.Minimum, Math.Min(timestamp.Maximum, newValue));

                                // timestamp.Value = Math.Min((int)audioFileReader.CurrentTime.TotalSeconds, timestamp.Maximum);
                                currentTime.Text = audioFileReader.CurrentTime.ToString(@"mm\:ss");
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
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
                        mainPanel.Tag = audioFilePath;

                        stopAudio();
                        playAudio(audioFilePath);
                    }
                }
            }
        }

        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs args)
        {
            string? currentFile = mainPanel.Tag?.ToString();

            if (Properties.Settings.Default.audioRepeat)
            {
                if (audioFileReader != null)
                {
                    if (string.IsNullOrEmpty(currentFile)) return;
                    stopAudio();
                    playAudio(currentFile);
                }
            }
            else
            {
                bPlayback.BackgroundImage = Properties.Resources.paused_play;
                isStopped = true;
            }
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (volumeSlider.Value > 0)
            {
                Properties.Settings.Default.audioVolume = float.Parse(volumeStatus.Text.Replace("%", string.Empty));
                Properties.Settings.Default.Save();
            }

            Properties.Settings.Default.appLocationX = this.Location.X;
            Properties.Settings.Default.appLocationY = this.Location.Y;
            Properties.Settings.Default.appSizeWidth = this.Width;
            Properties.Settings.Default.appSizeHeight = this.Height;

            string? currentFile = mainPanel.Tag?.ToString();
            if (string.IsNullOrEmpty(currentFile)) return;
            Properties.Settings.Default.lastFile = currentFile;
            Properties.Settings.Default.Save();

            stopAudio();
        }

        private void bPlayback_Click(object sender, EventArgs e)
        {
            /*
            if (mainPanel.Tag == null)
            {
                OpenFileDialog ofd = new OpenFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Title = "Select an audio file",
                    Filter = "Audio Files|*.mp3;*.wav;*.ogg;*.flac;*.mp4",
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = ofd.FileName;
                    if (string.IsNullOrEmpty(selectedFile))
                    {
                        return;
                    }
                    if (!File.Exists(selectedFile))
                    {
                        MessageBox.Show("The selected audio file does not exist.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    mainPanel.Tag = selectedFile;
                    stopAudio();
                    playAudio(selectedFile);
                    bPlayback.BackgroundImage = Properties.Resources.pause;
                    isPaused = false;
                }
                else
                    return;
            }
            */

            if (isStopped)
            {
                string? currentFile = mainPanel.Tag?.ToString();
                if (currentFile == null) return;

                stopAudio();
                playAudio(currentFile);

                bPlayback.BackgroundImage = Properties.Resources.pause;
                isPaused = false;
                isStopped = false;
                return;
            }

            if (!isPaused)
            {
                if (waveOut != null)
                    waveOut.Pause();

                bPlayback.BackgroundImage = Properties.Resources.paused_play;
                isPaused = true;
            }
            else
            {
                if (waveOut != null)
                    waveOut.Play();

                bPlayback.BackgroundImage = Properties.Resources.pause;
                isPaused = false;
            }

            /*
            if (isStopped)
            {
                if (waveOut != null)
                {
                    try
                    {
                        Debug.WriteLine($"waveOut.PlaybackState: {waveOut.PlaybackState}");

                        if (waveOut.PlaybackState == PlaybackState.Stopped)
                        {
                            string? currentAudio = mainPanel.Tag?.ToString();
                            if (!string.IsNullOrEmpty(currentAudio))
                            {
                                stopAudio();
                                playAudio(currentAudio);
                            }
                        }
                        else
                        {
                            waveOut.Play();
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Error playing audio: {ex.Message}");

                        string? currentAudio = mainPanel.Tag?.ToString();
                        if (!string.IsNullOrEmpty(currentAudio))
                        {
                            stopAudio();
                            playAudio(currentAudio);
                        }
                    }
                }
            }
            */
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
            // ???????????????
            if (audioFileReader != null)
            {
                audioFileReader.CurrentTime = TimeSpan.FromMilliseconds(timestamp.Value);
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
                    barFolder = Path.GetFileName(addressPath);
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

                browseFolders.Select();
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

                                try
                                {
                                    browseFolders.Items.Remove(matchFolder);
                                }
                                catch (Exception ex) { }

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

        private async void browseFolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            await browseAudioFiles();
        }

        private void panelBrowser_SizeChanged(object sender, EventArgs e)
        {
            int buttonWidth = panelBrowser.ClientSize.Width; // Adjust as needed for padding/scrollbar
            foreach (Control ctrl in panelBrowser.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Width = buttonWidth;
                }
            }
        }

        private void bStopAudio_Click(object sender, EventArgs e)
        {
            stopAudio();
            Text = "ReFrame";
        }

        private void volumeSlider_ValueChanged(object sender, EventArgs e)
        {

        }

        private void mainPanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string? currentFile = mainPanel.Tag?.ToString();
            if (currentFile == null)
            {
                OpenFileDialog ofd = new OpenFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Title = "Select an audio file",
                    Filter = "Audio Files|*.mp3;*.wav;*.ogg;*.flac;*.mp4",
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = ofd.FileName;
                    playSelection(selectedFile);
                }
            }
            else
            {
                string parentFolderOfTag = Path.GetDirectoryName(currentFile);
                OpenFileDialog ofd = new OpenFileDialog()
                {
                    InitialDirectory = parentFolderOfTag,
                    Title = "Select an audio file",
                    Filter = "Audio Files|*.mp3;*.wav;*.ogg;*.flac;*.mp4",
                };
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedFile = ofd.FileName;
                    playSelection(selectedFile);
                }
            }
        }

        private void notice_DragDrop(object sender, DragEventArgs e)
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
                        mainPanel.Tag = audioFilePath;

                        stopAudio();
                        playAudio(audioFilePath);
                    }
                }
            }
        }

        private void notice_DragEnter(object sender, DragEventArgs e)
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

        private void timestamp_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            updateTimestamp(e.X);
        }

        private void updateTimestamp(int mouseX)
        {
            float percent = (float)mouseX / timestamp.Width;
            int newValue = timestamp.Minimum + (int)((timestamp.Maximum - timestamp.Minimum) * percent);
            newValue = Math.Max(timestamp.Minimum, Math.Min(timestamp.Maximum, newValue));

            timestamp.Value = newValue;
            if (audioFileReader != null)
            {
                audioFileReader.CurrentTime = TimeSpan.FromMilliseconds(timestamp.Value);
            }
        }

        private void timestamp_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging) updateTimestamp(e.X);
        }

        private void timestamp_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                bPlayback.PerformClick();
                e.Handled = true;
            }
        }

        private void bChangeItem_Click(object sender, EventArgs e)
        {
        }

        private void bSwitchPageOnPlay_Click(object sender, EventArgs e)
        {
            if (shouldSwitchOnPlay)
            {
                bSwitchPageOnPlay.BackgroundImage = Properties.Resources.flip;
                Properties.Settings.Default.switchPage = false;
                shouldSwitchOnPlay = false;
            }
            else
            {
                bSwitchPageOnPlay.BackgroundImage = Properties.Resources.flip_selected;
                Properties.Settings.Default.switchPage = true;
                shouldSwitchOnPlay = true;
            }
        }

        private void barFolderName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bRemoveFolder.PerformClick();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void panelBrowser_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data == null) return;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void panelBrowser_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data == null) return;

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length > 0)
            {
                string fileName = files[0];
                bool fileNameExists = File.Exists(fileName);
                if (fileNameExists)
                {
                    playSelection(fileName);

                    // ?
                    if (Properties.Settings.Default.switchPage)
                    {
                        mainPanel.BringToFront();
                        isBrowserOpen = false;
                        isSettingsOpen = false;
                    }
                }
            }
        }
    }
}
