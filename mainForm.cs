using NAudio.Wave;

namespace ReFrameAudio
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            volumeStatus.Text = volumeSlider.Value.ToString() + "%";
        }

        private void volumeSlider_Scroll(object sender, EventArgs e)
        {
            volumeStatus.Text = volumeSlider.Value.ToString() + "%";
        }
    }
}
