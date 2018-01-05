using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace ComputerUsageLogger
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        StreamWriter sw;
        Thread threadForLogging;

        public Form1()
        {
            InitializeComponent();
            String filePath = @"C:\Users\haris\Dropbox\computerUsageLogs\" + DateTime.Today.ToLongDateString() + ".txt";
            if (!File.Exists(filePath))
            {
                sw = File.CreateText(filePath);
            }
            else
            {
                sw = new StreamWriter(File.Open(filePath, FileMode.Append));
            }

            sw.AutoFlush = true;

            sw.WriteLine("Computer Started at " + DateTime.Now.ToLongTimeString());

            threadForLogging = new Thread(keepLogging);
            threadForLogging.Start();

           

        }

        public void keepLogging()
        {
            while (true)
            {
                IntPtr win = GetForegroundWindow();
                uint pid;
                GetWindowThreadProcessId(win, out pid);
                Process p = Process.GetProcessById((int)pid);
                String ProcessName = p.ProcessName.ToString();
                String WindowTitle = p.MainWindowTitle.ToString();
                String timeStamp = DateTime.Now.ToLongTimeString();
                ProcessName.Replace('#', '-');
                WindowTitle.Replace('#', '-');
                timeStamp.Replace('#', '-');
                // Field Seperator is #
                //Console.WriteLine(ProcessName + " # " + WindowTitle + " # " + timeStamp);
                sw.WriteLine(ProcessName + " # " + WindowTitle + " # " + timeStamp);
                // Log Every minute
                Thread.Sleep(60000);

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false;
            this.Opacity = 0.0f;
            this.WindowState = FormWindowState.Minimized;
            
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            threadForLogging.Abort();
            sw.Close();
            Application.Exit();
        }
    }
}
