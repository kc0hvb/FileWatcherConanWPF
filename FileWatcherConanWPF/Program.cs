﻿using System;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileWatcherConanWPF
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ConanModWatcher());
        }

        public List<string> MainPortion()
        {
            MainProgram MP = new MainProgram();
            try
            {                
                List<string> value = MP.ProcessFileWatcher();
                return value;
            }
            catch (Exception ex)
            {                
                MP.ErrorLogCreation(ex);
                var lTextBox = new List<string>();
                return lTextBox;
            }
        }
    }
    public class MainProgram
    { 
        public List<string> ProcessFileWatcher()
        {

            string sSource = ConfigurationManager.AppSettings["PAK_Location"];
            string sTarget = ConfigurationManager.AppSettings["PAK_Target_Location"];
            //string[] files = System.IO.Directory.GetFiles(sSource.ToString, "*.pak", SearchOption.AllDirectories);

            string[] fileEntries = System.IO.Directory.GetFiles(sSource, "*.*", System.IO.SearchOption.AllDirectories);

            var lTextBox = new List<string>();

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
                            //Console.WriteLine($"File: {sFileName} was updated.");
                            lTextBox.Add($"File: {sFileName} was updated.");
                        }
                    }
                    else
                    {
                        File.Copy(fileName, sFileNameDest, true);
                        //Console.WriteLine($"File: {sFileName} did not exist but exists now.");
                        lTextBox.Add($"File: {sFileName} did not exist but exists now.");
                    }
                }
            }
            return lTextBox;
        }


        public void ErrorLogCreation(Exception ex)
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
