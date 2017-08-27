using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileWatcherConanWPF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            try
            {
                while (true)
                {
                    MainProgram.processFileWatcher();
                }
            }
            catch (Exception ex)
            {
                MainProgram.errorLogCreation(ex);
            }
        }
    }
    public class MainProgram
    { 
        public static void MinimizeToTray()
        {

            NotifyIcon trayIcon = new NotifyIcon();
            trayIcon.Text = "My application";
            trayIcon.Icon = 

                // Add menu to tray icon and show it.
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;

            Visible = false; // Hide form window.
            ShowInTaskbar = false; // Remove from taskbar.
        }
        private void Form1_Resize(object sender, System.EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void notifyIcon1_DoubleClick(object sender,
                                     System.EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        public static void processFileWatcher()
        {

            string sSource = ConfigurationManager.AppSettings["PAK_Location"];
            string sTarget = ConfigurationManager.AppSettings["PAK_Target_Location"];
            //string[] files = System.IO.Directory.GetFiles(sSource.ToString, "*.pak", SearchOption.AllDirectories);

            string[] fileEntries = System.IO.Directory.GetFiles(sSource, "*.*", System.IO.SearchOption.AllDirectories);

            if (!Directory.Exists(sTarget))
            {
                Directory.CreateDirectory(sTarget);
            }

            foreach (string fileName in fileEntries)
            {
                if (fileName.Contains(".pak"))
                {
                    string sFileName = Path.GetFileName(fileName);
                    string sFileNameDest = sTarget + '\\' + sFileName;
                    bool sFileNameDestExist = File.Exists(sFileNameDest);
                    if (sFileNameDestExist)
                    {
                        FileInfo fFileInfoSource = new FileInfo(fileName);
                        FileInfo fFileInfoDest = new FileInfo(sFileNameDest);
                        if (fFileInfoSource.LastWriteTimeUtc > fFileInfoDest.LastWriteTimeUtc)
                        {
                            File.Copy(fileName, sFileNameDest, true);
                            Console.WriteLine($"File: {sFileName} was updated.");
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        File.Copy(fileName, sFileNameDest, true);
                        Console.WriteLine($"File: {sFileName} did not exist but exists now.");
                    }
                }
            }

            Thread.Sleep(Int32.Parse(ConfigurationManager.AppSettings["Sleep_Time"]));
            return;
        }


        public static void errorLogCreation(Exception ex)
        {
            string sErrorFilePath = AppDomain.CurrentDomain.BaseDirectory + $"Error Log {DateTime.Today.Millisecond}.txt";
            using (StreamWriter file =
            new StreamWriter(sErrorFilePath))
            {
                file.WriteLine(ex);
            }
        }


    }

}
