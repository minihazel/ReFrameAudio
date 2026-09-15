using System.Runtime.InteropServices;

namespace ReFrameAudio
{
    public class ScrollHelperClass : NativeWindow
    {
        /*
         * a simple helper class to allow for hiding the scrollbar that Windows enforces
         * in the browser of the app when browsing for audio tracks
         * didn't feel like rewriting the whole browser and writing a whole new component for it
         * so this is the best I could come up with bc I'm lazy
        */

        private const int WM_NCCALCSIZE = 0x0083;
        private const int SB_VERT = 1;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_FRAMECHANGED = 0x0020;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool setWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public bool hideScrollbar { get; set; } = true;

        public ScrollHelperClass(Control control)
        {
            this.AssignHandle(control.Handle);
        }

        public void UpdateFrame()
        {
            if (this.Handle != IntPtr.Zero)
            {
                // forces Windows to WM_NCCALCSIZE again
                setWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0,
                    SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (hideScrollbar && m.Msg == WM_NCCALCSIZE && m.WParam != IntPtr.Zero)
            {
                ShowScrollBar(this.Handle, SB_VERT, false);
            }

            base.WndProc(ref m);
        }
    }
}
