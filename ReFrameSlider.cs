using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ReFrameAudio
{
    [DefaultEvent(nameof(ValueChanged))]
    public class ReFrameSlider : Control
    {
        /*
         * after getting tired as balls of the Windows TrackBar, I decided I needed something more elegant and usable
         * an idea was born and this custom slider was written
         * it has full support for trimming, much akin to VLC, as well as timeline and thumb tack customization
         * the thumb tack can be removed by simply setting thumb tack size to 0, might look funky
         * 
         * enjoy this elegant solution to my problem, works much better than a TrackBar :^)
        */

        // maths and visual dimensions
        private long minimum = 0;
        private long maximum = 100;
        private long value = 0;
        private bool isDragging = false;
        private long loopStart = -1;
        private long loopEnd = -1;
        private bool isLoopActive = false;
        private int trackHeight = 6;
        private int thumbSize = 14;

        // styling properties for the slider
        private Color trackColor = Color.FromArgb(60, 60, 60);
        private Color progressColor = Color.FromArgb(0, 120, 215);
        private Color thumbColor = Color.White;
        private Color loopRangeColor = Color.FromArgb(60, 0, 122, 204);
        private Color markerColorA = Color.FromArgb(0, 200, 255);
        private Color markerColorB = Color.FromArgb(255, 128, 0);

        // event handlers for using the slider
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

        // will be called inside OnPaint, for trimming functionality
        private void DrawLoopMarkers(Graphics g, int margin, int usableWidth, int trackY, int trackHeight)
        {
            if (!isLoopActive || loopStart < 0 || loopEnd <= loopStart || Maximum <= Minimum) return;

            float ratioA = (float)(loopStart - Minimum) / (Maximum - Minimum);
            float ratioB = (float)(loopEnd - Minimum) / (Maximum - Minimum);

            int xA = margin + (int)(usableWidth * ratioA);
            int xB = margin + (int)(usableWidth * ratioB);

            // 1. translucent loop highlight range
            int highlightWidth = xB - xA;
            if (highlightWidth > 0)
            {
                using (var brush = new SolidBrush(loopRangeColor))
                {
                    g.FillRectangle(brush, xA, trackY, highlightWidth, trackHeight);
                }
            }

            // 2. drawing marker A
            using (var penA = new Pen(markerColorA, 2f))
            using (var brushA = new SolidBrush(markerColorA))
            {
                g.DrawLine(penA, xA, trackY - 4, xA, trackY + trackHeight + 4);
                Point[] flagA = { new Point(xA, trackY - 4), new Point(xA + 5, trackY - 4), new Point(xA, trackY) };
                g.FillPolygon(brushA, flagA);
            }

            // 3. drawing marker B (trimming loop will only start working after B is set)
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
            // enable double-buffering and smoother rendering than WinForms normally does
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

            // position window slightly above the mouse cursor in screen coordinates
            // Point location = new Point(mouseScreenPosition.X - (tooltipPopup.Width / 2), mouseScreenPosition.Y - 30);
            // tooltipPopup.Location = location;

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

            // 1. draw background
            using (var trackBrush = new SolidBrush(trackColor))
            using (var trackPath = getRoundedRectPath(new Rectangle(margin, trackY, usableWidth, trackHeight), trackHeight / 2))
            {
                g.FillPath(trackBrush, trackPath);
            }

            // 2. draw progress bar
            int progressWidth = progressX - margin;
            if (progressWidth > 0)
            {
                using (var progressBrush = new SolidBrush(progressColor))
                using (var progressPath = getRoundedRectPath(new Rectangle(margin, trackY, progressWidth, trackHeight), trackHeight / 2))
                {
                    g.FillPath(progressBrush, progressPath);
                }
            }

            // returning back to drawing the loop markers for the trimming
            DrawLoopMarkers(g, margin, usableWidth, trackY, TrackHeight);

            // 3. draw thumb tack
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
                updateValueFromMouse(e.X);
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (isDragging) updateValueFromMouse(e.X);

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
                // convert mouse position to absolute screen coords
                // Point screenPos = PointToScreen(e.Location);
                // displayFloatingTooltip(timeString, screenPos);
                */
            }
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

        private void updateValueFromMouse(int mouseX)
        {
            // update timestamp based on mouse action

            int margin = thumbSize / 2;
            int usableWidth = Width - (margin * 2);
            if (usableWidth <= 0) return;

            int relativeX = Math.Max(0, Math.Min(usableWidth, mouseX - margin));
            float ratio = (float)relativeX / usableWidth;

            Value = minimum + (long)((maximum - minimum) * ratio);
        }

        private GraphicsPath getRoundedRectPath(Rectangle bounds, int radius)
        {
            // draw the timeline slider, because you know, seeing is believing

            GraphicsPath path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            int diameter = radius * 2;
            Rectangle arc = new Rectangle(bounds.X, bounds.Y, diameter, diameter);

            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.X;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
