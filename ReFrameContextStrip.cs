using System.Drawing.Text;

namespace ReFrameAudio
{
    public class ReFrameContextStrip : ToolStripProfessionalRenderer
    {
        /*
         * again, the default context strip is hideously ugly and outdated
         * this is another custom solution specifically tailored to this app
         * whether it works in another project is.... well, I cannot guarantee success
         * please tailor it to your use case or make it global, it's open source after all
         * 
         * it works great for this project, and it's an elegant solution that didn't
         * require whole rewriting or writing a whole new component
        */

        public ReFrameContextStrip() : base(new customColorTable()) {}

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected && e.Item.Enabled)
            {
                // 2px inset rectangle without any edge rounding
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

            // full width padding, slightly inset for alignment
            Rectangle textBounds = new Rectangle(16, 0, e.Item.Width - 20, e.Item.Height + 4);

            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Near; // horizontal alignment, as Intellisense says
                sf.LineAlignment = StringAlignment.Center; // vertical alignment, adjust to your liking

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
        public override Color ToolStripDropDownBackground => Color.FromArgb(30, 30, 30); // main bg
        public override Color ImageMarginGradientBegin => Color.FromArgb(30, 30, 30);     // remove left-hand side margin bar
        public override Color ImageMarginGradientMiddle => Color.FromArgb(30, 30, 30);
        public override Color ImageMarginGradientEnd => Color.FromArgb(30, 30, 30);
        public override Color MenuBorder => Color.FromArgb(60, 60, 60);
        public override Color SeparatorDark => Color.FromArgb(50, 50, 50);
    }
}
