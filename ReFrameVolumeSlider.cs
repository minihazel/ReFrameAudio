using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReFrameAudio
{
    public class ReFrameVolumeSlider : Control
    {
        // VALUES

        private float value = 1.0f; // 0.0f to 1.0f
        private float minimum = 0.0f;
        private float maximum = 100.0f;
        private bool isDragging = false;
        private int trackHeight = 4;
        private int thumbSize = 12;

        // COLORS
        private Color trackColor = Color.FromArgb(60, 60, 60);
        private Color progressColor = Color.FromArgb(72, 210, 72);
        private Color thumbColor = Color.White;

        public event EventHandler? ValueChanged;

        #region Category Properties (Designer)
        [Category("Volumeline Appearance")]
        public int TrackHeight { get => trackHeight; set { trackHeight = value; Invalidate(); } }

        [Category("Volumeline Appearance")]
        public int ThumbSize { get => thumbSize; set { thumbSize = value; Invalidate(); } }

        [Category("Volume Appearance")]
        public Color TrackColor
        {
            get => trackColor;
            set { trackColor = value; Invalidate(); }
        }

        [Category("Volume Appearance")]
        public Color ProgressColor
        {
            get => progressColor;
            set { progressColor = value; Invalidate(); }
        }

        [Category("Volume Appearance")]
        public Color ThumbColor
        {
            get => thumbColor;
            set { thumbColor = value; Invalidate(); }
        }

        [Category("Volume Appearance")]
        [DefaultValue(true)]
        public bool ShowVolumeText { get; set; } = true;

        [Category("Volume Properties")]
        [DefaultValue(0.0f)]
        public float Minimum
        {
            get => minimum;
            set
            {
                minimum = value;
                if (this.value < minimum) Value = minimum;
                Invalidate();
            }
        }

        [Category("Volume Properties")]
        [DefaultValue(100.0f)]
        public float Maximum
        {
            get => maximum;
            set
            {
                maximum = value;
                if (this.value > maximum) Value = maximum;
                Invalidate();
            }
        }

        [Category("Volume Properties")]
        [DefaultValue(100.0f)]
        public float Value
        {
            get => value;
            set
            {
                float clamped = Math.Max(minimum, Math.Min(maximum, value));
                if (Math.Abs(this.value - clamped) > 0.01f)
                {
                    this.value = clamped;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        #endregion

        public ReFrameVolumeSlider()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            Height = 20;
            Cursor = Cursors.Arrow;
        }

        #region Mouse Events

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                updateMouseValue(e.X);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isDragging)
            {
                updateMouseValue(e.X);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            isDragging = false;
        }

        private void updateMouseValue(int mouseX)
        {
            int margin = thumbSize / 2;
            int usableWidth = Width - (margin * 2);
            if (usableWidth <= 0) return;

            int relativeX = Math.Max(0, Math.Min(usableWidth, mouseX - margin));
            float ratio = (float)relativeX / usableWidth;
            Value = minimum + (ratio * (maximum - minimum));
        }

        #endregion

        #region Rendering

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int margin = thumbSize / 2;
            int usableWidth = Width - (margin * 2);
            if (usableWidth <= 0 || maximum <= minimum) return;

            int trackY = (Height - trackHeight) / 2;
            float ratio = (value - minimum) / (maximum - minimum);
            int progressWidth = (int)(usableWidth * ratio);

            // draw Background Track
            using (var brush = new SolidBrush(trackColor))
            {
                g.FillRectangle(brush, margin, trackY, usableWidth, trackHeight);
            }

            // draw active volume progress bar
            if (progressWidth > 0)
            {
                using (var brush = new SolidBrush(progressColor))
                {
                    g.FillRectangle(brush, margin, trackY, progressWidth, trackHeight);
                }
            }

            // draw volume value text
            if (ShowVolumeText)
            {
                string percentageText = Math.Round(value) + "%";
                using (var font = new Font("Bahnschrift SemiLight", 7.5f, FontStyle.Regular))
                {
                    Color textColor = Color.FromArgb(100, 100, 100);
                    Size textSize = TextRenderer.MeasureText(g, percentageText, font, Size.Empty, TextFormatFlags.NoPadding);
                    int textX = margin /*(int)(margin + (progressWidth / 2f) - (textSize.Width / 2f))*/;
                    textX = Math.Max(margin, Math.Min(Width - margin - textSize.Width, textX));

                    int textY = trackY - textSize.Height - 4;

                    TextRenderer.DrawText(
                        g,
                        percentageText,
                        font,
                        new Point(textX, textY),
                        textColor,
                        /* TextFormatFlags.NoPadding | */ TextFormatFlags.NoClipping
                    );
                }
            }

            // draw thumbtack
            int thumbX = margin + progressWidth - (thumbSize / 2);
            int thumbY = (Height - thumbSize) / 2;

            using (var brush = new SolidBrush(thumbColor))
            {
                g.FillEllipse(brush, thumbX, thumbY, thumbSize, thumbSize);
            }
        }

        #endregion
    }
}
