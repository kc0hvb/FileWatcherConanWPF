using System;
using System.Diagnostics;
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
        private ObservableCollection<string> collection = new ObservableCollection<string>();
        
        private static ConanModWatcher CMW = new ConanModWatcher();

        public Dictionary<string, string> PullValuesFromConfig()
        {
            Dictionary<string, string> dValuesFromConfig = new Dictionary<string, string>();
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string configFile = Path.Combine(appPath, "FileWatcherConanWPF.exe.config");
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            dValuesFromConfig.Add("PAK_Location", config.AppSettings.Settings["PAK_Location"].Value);
            dValuesFromConfig.Add("Sleep_Time", config.AppSettings.Settings["Sleep_Time"].Value);
            dValuesFromConfig.Add("PAK_Target_Location", config.AppSettings.Settings["PAK_Target_Location"].Value);
            dValuesFromConfig.Add("Mod_File_Location", config.AppSettings.Settings["Mod_File_Location"].Value);
            dValuesFromConfig.Add("Automaticaly_Transfer_Files", config.AppSettings.Settings["Automaticaly_Transfer_Files"].Value);
            dValuesFromConfig.Add("Conan_Server_Location", config.AppSettings.Settings["Conan_Server_Location"].Value);
            dValuesFromConfig.Add("SteamCmd_Location", config.AppSettings.Settings["SteamCmd_Location"].Value);
            dValuesFromConfig.Add("Validate_Conan", config.AppSettings.Settings["Validate_Conan"].Value);
            dValuesFromConfig.Add("Batch_Location", config.AppSettings.Settings["Batch_Location"].Value);

            return dValuesFromConfig;
        }

        public ObservableCollection<string> ProcessFileWatcher()
        {
            ConanModWatcher CMW = new ConanModWatcher();

            Dictionary<string,string> dConfigValue = PullValuesFromConfig();
            string sSource = dConfigValue["Mod_File_Location"];
            string sPakSource = dConfigValue["PAK_Location"];
            string sPakTarget = dConfigValue["PAK_Target_Location"];
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
            return collection;
        }

        public string[] GetTextFromTextFile(Dictionary<string, string> pSource)
        {

            string sSource = pSource["Mod_File_Location"];
            string[] lines = null;
            if (File.Exists(sSource))
            {
                lines = File.ReadAllLines(sSource);
            }
                return lines;
            
        }

        public void AddingModsInText(string pCheckedValue)
        {
            Dictionary<string, string> dConfigValue = PullValuesFromConfig();
            string sSource = dConfigValue["Mod_File_Location"];
            
            //File.WriteAllText(sSource, String.Empty);
            string checkedItems = string.Empty;
            if (!File.Exists(sSource)) File.Create(sSource).Close();
            if (File.Exists(sSource))
            {
                string[] collection2 = GetTextFromTextFile(dConfigValue);
                if (!collection2.Contains(pCheckedValue))
                {
                    StreamWriter tw = File.AppendText(sSource);
                    tw.WriteLine(pCheckedValue);
                    tw.Close();
                }
                
            }
        }

        public void RemovingModsInText(string pCheckedValue)
        {
            Dictionary<string, string> dConfigValue = PullValuesFromConfig();
            string sSource = dConfigValue["Mod_File_Location"];

            var tempFile = Path.GetTempFileName();
            string[] linesToKeep = File.ReadAllLines(sSource);
            StreamWriter writer = new StreamWriter(tempFile);
            foreach (string line in linesToKeep)
            {
                if (!line.Contains(pCheckedValue))
                {
                    writer.WriteLine(line);
                }
            }
            writer.Close();
            File.Delete(sSource);
            File.Move(tempFile, sSource);
        }
        
        public bool SteamCMDProcess(bool bValidationEnable)
        {
            bool bInstalled = false;
            Dictionary<string, string> dConfigValue = new Dictionary<string, string>();
            dConfigValue = PullValuesFromConfig();
            Process process = new Process();
            process.StartInfo.FileName = (dConfigValue["SteamCmd_Location"]) + @"\steamcmd.exe";
            string sConanServerLocation = dConfigValue["Conan_Server_Location"];
            if (dConfigValue["Conan_Server_Location"] == "")
            {
                string sMessage = "Please Setup the location of the server in settings.";
                process.StartInfo.Arguments = "";
                MessageBox.Show(sMessage);
            }
            else
            {
                if (bValidationEnable == true) process.StartInfo.Arguments = $" +login anonymous +force_install_dir {sConanServerLocation} +app_update 443030 validate +exit";
                else process.StartInfo.Arguments = $" +login anonymous +force_install_dir {sConanServerLocation} +app_update 443030 +exit";
            }
            if (process.StartInfo.Arguments != "")
            {
                process.Start();
                process.WaitForExit();
                bInstalled = true;
            }
            return bInstalled;
        }

        public void StartServer()
        {
            Dictionary<string, string> dictionary = PullValuesFromConfig();
            Process process = new Process();
            process.StartInfo.FileName = dictionary["Batch_Location"];
            process.StartInfo.WorkingDirectory = Path.GetDirectoryName(dictionary["Batch_Location"]);
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
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
