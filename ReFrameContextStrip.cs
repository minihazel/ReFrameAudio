using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ReFrameAudio
{
    public class ReFrameContextStrip : ToolStripProfessionalRenderer
    {
        public ReFrameContextStrip() : base(new customColorTable()) {}

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected && e.Item.Enabled)
            {
                // Clean 2px inset rectangle with zero corner rounding
                Rectangle rect = new Rectangle(2, 0, e.Item.Width, e.Item.Height);

                using (SolidBrush hoverBrush = new SolidBrush(Color.FromArgb(48, 48, 54)))
                {
                    e.Graphics.FillRectangle(hoverBrush, rect);
                }
            }
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // Use full width of the padded item, inset slightly for clean alignment
            Rectangle textBounds = new Rectangle(16, 0, e.Item.Width - 20, e.Item.Height + 4);

            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center; // Vertically centers inside the taller padded item

                Color fontColor = e.Item.Enabled ? Color.FromArgb(220, 220, 225) : Color.Gray;

                using (SolidBrush textBrush = new SolidBrush(fontColor))
                {
                    e.Graphics.DrawString(e.Text, e.Item.Font, textBrush, textBounds, sf);
                }
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height - 1);
            using (Pen borderPen = new Pen(Color.FromArgb(55, 55, 60)))
            {
                e.Graphics.DrawRectangle(borderPen, rect);
            }
        }
    }

    public class customColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground => Color.FromArgb(30, 30, 30); // Main menu background
        public override Color ImageMarginGradientBegin => Color.FromArgb(30, 30, 30);     // Removes light left margin bar
        public override Color ImageMarginGradientMiddle => Color.FromArgb(30, 30, 30);
        public override Color ImageMarginGradientEnd => Color.FromArgb(30, 30, 30);
        public override Color MenuBorder => Color.FromArgb(60, 60, 60);
        public override Color SeparatorDark => Color.FromArgb(50, 50, 50);                // Dark menu divider lines
    }
}
