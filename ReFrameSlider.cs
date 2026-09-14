using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ReFrameAudio
{
    [DefaultEvent(nameof(ValueChanged))]
    public class ReFrameSlider : Control
    {
        private long minimum = 0;
        private long maximum = 100;
        private long value = 0;
        private bool isDragging = false;
        private long loopStart = -1;
        private long loopEnd = -1;
        private bool isLoopActive = false;
        private int trackHeight = 6;
        private int thumbSize = 14;

        // Custom Styling Properties
        private Color trackColor = Color.FromArgb(60, 60, 60);
        private Color progressColor = Color.FromArgb(0, 120, 215);
        private Color thumbColor = Color.White;
        private Color loopRangeColor = Color.FromArgb(60, 0, 122, 204);
        private Color markerColorA = Color.FromArgb(0, 200, 255);
        private Color markerColorB = Color.FromArgb(255, 128, 0);

        public event EventHandler? ValueChanged;
        public event EventHandler? SeekStarted;
        public event EventHandler? SeekFinished;

        #region Loop Category Properties

        [Category("Slider Loop")]
        public long LoopStart
        {
            get => loopStart;
            set { loopStart = value; Invalidate(); }
        }

        [Category("Slider Loop")]
        public long LoopEnd
        {
            get => loopEnd;
            set { loopEnd = value; Invalidate(); }
        }

        [Category("Slider Loop")]
        public bool IsLoopActive
        {
            get => isLoopActive;
            set { isLoopActive = value; Invalidate(); }
        }

        #endregion

        // Call this method inside your ReFrameSlider's OnPaint
        private void DrawLoopMarkers(Graphics g, int margin, int usableWidth, int trackY, int trackHeight)
        {
            if (!isLoopActive || loopStart < 0 || loopEnd <= loopStart || Maximum <= Minimum) return;

            float ratioA = (float)(loopStart - Minimum) / (Maximum - Minimum);
            float ratioB = (float)(loopEnd - Minimum) / (Maximum - Minimum);

            int xA = margin + (int)(usableWidth * ratioA);
            int xB = margin + (int)(usableWidth * ratioB);

            // 1. Draw Translucent Loop Highlight Range
            int highlightWidth = xB - xA;
            if (highlightWidth > 0)
            {
                using (var brush = new SolidBrush(loopRangeColor))
                {
                    g.FillRectangle(brush, xA, trackY, highlightWidth, trackHeight);
                }
            }

            // 2. Draw Marker A (Cyan Flag)
            using (var penA = new Pen(markerColorA, 2f))
            using (var brushA = new SolidBrush(markerColorA))
            {
                g.DrawLine(penA, xA, trackY - 4, xA, trackY + trackHeight + 4);
                Point[] flagA = { new Point(xA, trackY - 4), new Point(xA + 5, trackY - 4), new Point(xA, trackY) };
                g.FillPolygon(brushA, flagA);
            }

            // 3. Draw Marker B (Orange Flag)
            using (var penB = new Pen(markerColorB, 2f))
            using (var brushB = new SolidBrush(markerColorB))
            {
                g.DrawLine(penB, xB, trackY - 4, xB, trackY + trackHeight + 4);
                Point[] flagB = { new Point(xB, trackY - 4), new Point(xB - 5, trackY - 4), new Point(xB, trackY) };
                g.FillPolygon(brushB, flagB);
            }
        }

        [Category("Timeline Properties")]
        public long Minimum
        {
            get => minimum;
            set { minimum = value; Invalidate(); }
        }

        [Category("Timeline Properties")]
        public long Maximum
        {
            get => maximum;
            set { maximum = Math.Max(1, value); Invalidate(); }
        }

        [Category("Timeline Properties")]
        public long Value
        {
            get => value;
            set
            {
                long clamped = Math.Max(minimum, Math.Min(maximum, value));
                if (this.value != clamped)
                {
                    this.value = clamped;
                    Invalidate();
                    ValueChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        [Category("Timeline Appearance")]
        public Color TrackColor { get => trackColor; set { trackColor = value; Invalidate(); } }

        [Category("Timeline Appearance")]
        public Color ProgressColor { get => progressColor; set { progressColor = value; Invalidate(); } }

        [Category("Timeline Appearance")]
        public Color ThumbColor { get => thumbColor; set { thumbColor = value; Invalidate(); } }

        [Category("Timeline Appearance")]
        public int TrackHeight { get => trackHeight; set { trackHeight = value; Invalidate(); } }

        [Category("Timeline Appearance")]
        public int ThumbSize { get => thumbSize; set { thumbSize = value; Invalidate(); } }

        public bool IsDragging => isDragging;
        private Form? tooltipPopup;
        private Label? tooltipLabel;

        public ReFrameSlider()
        {
            // Enable double buffering and smooth anti-aliased rendering
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            Height = 24;
            Cursor = Cursors.Hand;
        }

        private void displayFloatingTooltip(string text, Point mouseLocalPosition)
        {
            if (tooltipPopup == null || tooltipPopup.IsDisposed)
            {
                tooltipPopup = new Form
                {
                    FormBorderStyle = FormBorderStyle.None,
                    StartPosition = FormStartPosition.Manual,
                    ShowInTaskbar = false,
                    TopMost = true,
                    BackColor = Color.White,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    Padding = new Padding(0, 1, 0, 1)
                };

                tooltipLabel = new Label
                {
                    AutoSize = true,
                    Font = new Font("Bahnschrift Light", 9f, FontStyle.Regular),
                    ForeColor = Color.Black,
                    Margin = Padding.Empty
                };

                tooltipPopup.Controls.Add(tooltipLabel);
            }

            tooltipLabel!.Text = text;

            Point ctrlTopScreen = PointToScreen(new Point(mouseLocalPosition.X, 0));

            int tooltipX = ctrlTopScreen.X - (tooltipPopup.Width / 2);
            int tooltipY = ctrlTopScreen.Y - tooltipPopup.Height - 5; // 5 pixels above the control

            tooltipPopup.Location = new Point(tooltipX, tooltipY);

            /*
            // position window slightly above the mouse cursor in screen coordinates
            Point location = new Point(mouseScreenPosition.X - (tooltipPopup.Width / 2), mouseScreenPosition.Y - 30);
            tooltipPopup.Location = location;
            */

            if (!tooltipPopup.Visible)
            {
                tooltipPopup.Show();
            }
        }

        private void hideFloatingTooltip()
        {
            if (tooltipPopup != null && tooltipPopup.Visible)
            {
                tooltipPopup.Hide();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int margin = thumbSize / 2;
            int usableWidth = Width - (margin * 2);
            if (usableWidth <= 0) return;

            float ratio = (float)(value - minimum) / (maximum - minimum);
            int progressX = margin + (int)(usableWidth * ratio);
            int trackY = (Height - trackHeight) / 2;

            // 1. Draw Background Track
            using (var trackBrush = new SolidBrush(trackColor))
            using (var trackPath = GetRoundedRectPath(new Rectangle(margin, trackY, usableWidth, trackHeight), trackHeight / 2))
            {
                g.FillPath(trackBrush, trackPath);
            }

            // 2. Draw Progress Bar
            int progressWidth = progressX - margin;
            if (progressWidth > 0)
            {
                using (var progressBrush = new SolidBrush(progressColor))
                using (var progressPath = GetRoundedRectPath(new Rectangle(margin, trackY, progressWidth, trackHeight), trackHeight / 2))
                {
                    g.FillPath(progressBrush, progressPath);
                }
            }

            DrawLoopMarkers(g, margin, usableWidth, trackY, TrackHeight);

            // 3. Draw Thumb
            int thumbX = progressX - (thumbSize / 2);
            int thumbY = (Height - thumbSize) / 2;
            using (var thumbBrush = new SolidBrush(thumbColor))
            {
                g.FillEllipse(thumbBrush, thumbX, thumbY, thumbSize, thumbSize);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                SeekStarted?.Invoke(this, EventArgs.Empty);
                UpdateValueFromMouse(e.X);
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (isDragging) UpdateValueFromMouse(e.X);

            int margin = thumbSize / 2;
            int usableWidth = Width - (margin * 2);

            if (usableWidth > 0 && maximum > minimum)
            {
                int relativeX = Math.Max(0, Math.Min(usableWidth, e.X - margin));
                float ratio = (float)relativeX / usableWidth;
                long hoveredMs = minimum + (long)((maximum - minimum) * ratio);

                TimeSpan time = TimeSpan.FromMilliseconds(hoveredMs);
                string timeString = time.TotalHours >= 1
                    ? time.ToString(@"hh\:mm\:ss")
                    : time.ToString(@"mm\:ss");

                displayFloatingTooltip(timeString, e.Location);
                /*
                // Convert mouse position to absolute Screen Coordinates
                Point screenPos = PointToScreen(e.Location);
                displayFloatingTooltip(timeString, screenPos);
                */
            }



            /*
            if (isDragging)
            {
                UpdateValueFromMouse(e.X);
            }
            base.OnMouseMove(e);
            */
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            hideFloatingTooltip();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (isDragging && e.Button == MouseButtons.Left)
            {
                isDragging = false;
                SeekFinished?.Invoke(this, EventArgs.Empty);
            }
            base.OnMouseUp(e);
        }

        private void UpdateValueFromMouse(int mouseX)
        {
            int margin = thumbSize / 2;
            int usableWidth = Width - (margin * 2);
            if (usableWidth <= 0) return;

            int relativeX = Math.Max(0, Math.Min(usableWidth, mouseX - margin));
            float ratio = (float)relativeX / usableWidth;

            Value = minimum + (long)((maximum - minimum) * ratio);
        }

        private GraphicsPath GetRoundedRectPath(Rectangle bounds, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = radius * 2;
            Rectangle arc = new Rectangle(bounds.X, bounds.Y, diameter, diameter);

            path.AddArc(arc, 180, 90); // Top Left
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90); // Top Right
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90); // Bottom Right
            arc.X = bounds.X;
            path.AddArc(arc, 90, 90); // Bottom Left
            path.CloseFigure();

            return path;
        }
    }
}
