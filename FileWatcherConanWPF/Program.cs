using System;
using System.Timers;
using System.Configuration;
using System.IO;
using System.Collections.ObjectModel;
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

    public ObservableCollection<string> MainPortion()
        {
            MainProgram MP = new MainProgram();
            try
            {                
                var value = MP.ProcessFileWatcher();
                return value;
            }
            catch (Exception ex)
            {                
                MP.ErrorLogCreation(ex);
                var lTextBox = new ObservableCollection<string>();
                return lTextBox;
            }
        }
    }

    public class MainProgram
    {
        private string sSource = ConfigurationManager.AppSettings["Mod_File_Location"];
        private ObservableCollection<string> collection = new ObservableCollection<string>();
        private string sPakSource = ConfigurationManager.AppSettings["PAK_Location"];
        private string sPakTarget = ConfigurationManager.AppSettings["PAK_Target_Location"];

        public ObservableCollection<string> ProcessFileWatcher()
        {
            ConanModWatcher CMW = new ConanModWatcher();
            if (!File.Exists(sSource))
            {
                File.Create(sSource);
            }

            string[] fileEntries = Directory.GetFiles(sPakSource, "*.*", System.IO.SearchOption.AllDirectories);

            if (!Directory.Exists(sPakTarget))
            {
                Directory.CreateDirectory(sPakTarget);
            }

            foreach (string fileName in fileEntries)
            {
                if (fileName.Contains(".pak"))
                {
                    string sFileName = Path.GetFileName(fileName);
                    string sFileNameDest = sPakTarget + '\\' + sFileName;
                    bool sFileNameDestExist = File.Exists(sFileNameDest);
                    if (sFileNameDestExist)
                    {
                        FileInfo fFileInfoSource = new FileInfo(fileName);
                        FileInfo fFileInfoDest = new FileInfo(sFileNameDest);
                        if (fFileInfoSource.LastWriteTimeUtc > fFileInfoDest.LastWriteTimeUtc)
                        {
                            File.Copy(fileName, sFileNameDest, true);
                            collection.Add($"File: {sFileName} was updated.");
                        }
                    }
                    else
                    {
                        File.Copy(fileName, sFileNameDest, true);
                        string sFileWithExt = Path.GetFileNameWithoutExtension(fileName);
                        collection.Add($"File: {sFileName} did not exist but exists now.");

                    }
                }
            }
            CMW.GetCheckedItems();
            return collection;
        }

        public ObservableCollection<string> GetTextFromTextFile()
        {
            ObservableCollection<string> ocRet = new ObservableCollection<string>();
            if (File.Exists(sSource))
            {
                string lines = System.IO.File.ReadAllText(sSource);
                ocRet.Add(lines);
            }
            return ocRet;
        }

        public void SettingUpModsInText()//object source, ElapsedEventArgs e)
        {
            ConanModWatcher CMW = new ConanModWatcher();
            ObservableCollection<string> collection = CMW.GetCheckedItems();
            ObservableCollection<string> collection2 = GetTextFromTextFile();
            File.WriteAllText(sSource, String.Empty);
            foreach (var id in collection)
            {
                if (File.Exists(sSource)) File.WriteAllText(sSource, id);
            }
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
