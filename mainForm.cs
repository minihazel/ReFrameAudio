using NAudio.Wave;

namespace ReFrameAudio
{
    public partial class mainForm : Form
    {
        private bool isPaused = true;
        private WaveOutEvent waveOut;
        private AudioFileReader audioFileReader;

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
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

            /*
            if (!string.IsNullOrEmpty(Properties.Settings.Default.currentFileName) &&
                !string.IsNullOrEmpty(Properties.Settings.Default.currentFilePath))
            {
                string filePath = Properties.Settings.Default.currentFilePath;
                string fileName = Properties.Settings.Default.currentFileName;

                bool doesFileExist = File.Exists(filePath);
                if (doesFileExist && isValidAudio(filePath))
                {
                    playAudio(filePath);
                    Text = "ReFrame" + " - " + fileName;
                }
            }
            */
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
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }

            if (audioFileReader != null)
            {
                audioFileReader.Dispose();
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

            waveOut.Play();
            isPaused = false;

            Text = "ReFrame" + " - " + Path.GetFileName(filePath);
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
            if (Properties.Settings.Default.audioRepeat && args.Exception == null)
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

            stopAudio();
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
    }
}
