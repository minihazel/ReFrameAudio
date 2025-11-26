using System.IO.Pipes;

namespace ReFrameAudio
{
    internal static class Program
    {
        private const string appGuid = "3c36c4b2-e1d5-4a57-89e4-c7a8716b149b";
        private const string pipeName = "ReFrameAudioPipe";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ApplicationConfiguration.Initialize();

            bool createdNew;
            using (Mutex mutex = new Mutex(true, appGuid, out createdNew))
            {
                string filePath = (args.Length > 0 && File.Exists(args[0])) ? args[0] : null;

                if (createdNew)
                {
                    Application.Run(new mainForm(filePath));
                    mutex.ReleaseMutex();
                }
                else
                {
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        // Send the file path to the running instance.
                        sendToRunningInstance(filePath);
                    }
                }
            }
        }

        private static void sendToRunningInstance(string filePath)
        {
            try
            {
                using (var client = new NamedPipeClientStream(".", pipeName, PipeDirection.Out))
                {
                    client.Connect(500);
                    using (var writer = new StreamWriter(client))
                    {
                        writer.WriteLine(filePath);
                        writer.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        /*
        private static void sendToInstance(string filePath)
        {
            IntPtr mainFormHandle = FindWindow(null, "ReFrameAudio");
            if (mainFormHandle != IntPtr.Zero)
            {
                const int WM_USER = 0x0400;
                const int WM_LOAD_FILE = WM_USER + 1;
            }
        }
        */

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
    }
}