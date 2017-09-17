﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Configuration;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Linq.Expressions;

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

    public class UpdatingUI
    {
        
    }
    public class MainProgram
    {
        #region Static Functions.
        private ObservableCollection<string> collection = new ObservableCollection<string>();

        private static ConanModWatcher CMW = new ConanModWatcher();
        
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        #region Getting information from Configuration file.
        public Dictionary<string, string> PullValuesFromConfig()
        {
            try
            {
                Dictionary<string, string> dValuesFromConfig = new Dictionary<string, string>();
                string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                string configFile = Path.Combine(appPath, "Conan Exiles Server Admin.exe.config");
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = configFile;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                dValuesFromConfig.Add("PAK_Location", config.AppSettings.Settings["PAK_Location"].Value);
                dValuesFromConfig.Add("PAK_Target_Location", config.AppSettings.Settings["PAK_Target_Location"].Value);
                dValuesFromConfig.Add("Sleep_Time", config.AppSettings.Settings["Sleep_Time"].Value);
                dValuesFromConfig.Add("Automaticaly_Transfer_Files", config.AppSettings.Settings["Automaticaly_Transfer_Files"].Value);
                dValuesFromConfig.Add("Conan_Server_Location", config.AppSettings.Settings["Conan_Server_Location"].Value);
                dValuesFromConfig.Add("SteamCmd_Location", config.AppSettings.Settings["SteamCmd_Location"].Value);
                dValuesFromConfig.Add("Validate_Conan", config.AppSettings.Settings["Validate_Conan"].Value);
                dValuesFromConfig.Add("Batch_Location", config.AppSettings.Settings["Batch_Location"].Value);
                dValuesFromConfig.Add("Arguements_Server_Start", config.AppSettings.Settings["Arguements_Server_Start"].Value);
                return dValuesFromConfig;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
            
        }
        #endregion

        #region The Actual file watching and copying of Mod files.
        public ObservableCollection<string> ProcessFileWatcher()
        {
            try
            {
                ConanModWatcher CMW = new ConanModWatcher();

                Dictionary<string, string> dConfigValue = PullValuesFromConfig();
                string sSource = dConfigValue["Conan_Server_Location"] + @"\ConanSandbox\Mods\modlist.txt";
                string sPakSource = dConfigValue["PAK_Location"];
                string sPakTarget = dConfigValue["PAK_Target_Location"];

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
                        if (dConfigValue["Automaticaly_Transfer_Files"] == "true")
                        {
                            if (File.Exists(sFileNameDest))
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
                        else
                        {
                            if (File.Exists(sFileNameDest)) collection.Add($"File: {fileName} has been updated on source. Needs to be move to target.");
                            else collection.Add($"File: {fileName} does not currently exist in target location.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return collection;
        }
        #endregion

        #region Getting Adding and Removing information from Text.
        public string[] GetTextFromTextFile(Dictionary<string, string> pSource)
        {

            string sSource = pSource["Conan_Server_Location"] + @"\ConanSandbox\Mods\modlist.txt";
            string sSourceFolder = pSource["Conan_Server_Location"] + @"\ConanSandbox\Mods\";
            string[] lines = null;
            if (pSource["Conan_Server_Location"] != "")
            {
                if (!Directory.Exists(sSourceFolder)) Directory.CreateDirectory(sSourceFolder);
                if (!File.Exists(sSource))
                {
                    using (StreamWriter sw = new StreamWriter(sSource, true))
                    {
                        sw.Close();
                    }
                    
                }
                else lines = File.ReadAllLines(sSource);
            }
            return lines;

        }

        public void AddingModsInText(string pCheckedValue)
        {
            Dictionary<string, string> dConfigValue = PullValuesFromConfig();
            string sSource = dConfigValue["Conan_Server_Location"] + @"\ConanSandbox\Mods\modlist.txt";
            
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
            string sSource = dConfigValue["Conan_Server_Location"] + @"\ConanSandbox\Mods\modlist.txt";

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
        #endregion

        #region Server Starting and Stopping
        public bool SteamCMDProcess(bool bValidationEnable)
        {
            bool bInstalled = false;
            Dictionary<string, string> dConfigValue = new Dictionary<string, string>();
            dConfigValue = PullValuesFromConfig();
            Process process = new Process();
            process.StartInfo.CreateNoWindow = true;
            try
            {
                if (dConfigValue["SteamCmd_Location"] != "")
                {
                    process.StartInfo.FileName = (dConfigValue["SteamCmd_Location"]);
                    string sConanServerLocation = dConfigValue["Conan_Server_Location"];
                    if (dConfigValue["Conan_Server_Location"] == "")
                    {
                        string sMessage = "Please Setup the location of the server in settings.";
                        process.StartInfo.Arguments = "";
                        MessageBox.Show(sMessage);
                        SettingsForm form = new SettingsForm();
                        form.Show();
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
                }
                else MessageBox.Show("Please setup location of SteamCMD before preceeding.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return bInstalled;
        }

        public bool SteamCMDProcess(bool bValidationEnable, string pSteamPath, string pServerLocation)
        {
            bool bInstalled = false;
            Process process = new Process();
            process.StartInfo.CreateNoWindow = true;
            try
            {
                if (pSteamPath != null || pSteamPath != "")
                {
                    process.StartInfo.FileName = pSteamPath;
                    string sConanServerLocation = pServerLocation;
                    if (sConanServerLocation == "")
                    {
                        string sMessage = "Please select a location for the server.";
                        process.StartInfo.Arguments = "";
                        MessageBox.Show(sMessage);
                        SettingsForm form = new SettingsForm();
                        form.Show();
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return bInstalled;
        }

        public void StartServer(string pArguments)
        {
            Dictionary<string, string> dictionary = PullValuesFromConfig();
            Process process = new Process();
            if (dictionary["Conan_Server_Location"] != "")
            {
                process.StartInfo.FileName = dictionary["Conan_Server_Location"] + @"\ConanSandboxServer.exe";
                process.StartInfo.Arguments = pArguments; //"-log -MULTIHOME=192.168.0.103 -QueryPort=27016";
                process.StartInfo.WorkingDirectory = Path.GetDirectoryName(dictionary["Conan_Server_Location"]);
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
            }
        }

        public void StopServer()
        {
            SendCommandToServer();
        }
        
        public bool ServerIsRunning()
        {
            bool bRet = false;
            Process p = Process.GetProcessesByName("ConanSandboxServer-Win64-Test").FirstOrDefault();
            if (p != null) bRet = true;
            return bRet;
        }

        public void SendCommandToServer()
        {
            Process p = Process.GetProcessesByName("ConanSandboxServer-Win64-Test").FirstOrDefault();
            if (p != null)
            {
                //p.CloseMainWindow();
                //p.Close();
                IntPtr h = p.MainWindowHandle;
                SetForegroundWindow(h);
                SendKeys.SendWait("^(C)");
                p.WaitForExit();
            }
        }
        #endregion

        public void ErrorLogCreation(Exception ex)
        {
            string sErrorFilePath = AppDomain.CurrentDomain.BaseDirectory + $"Error Log {DateTime.Today.Millisecond}.txt";
            using (StreamWriter file = new StreamWriter(sErrorFilePath))
            {
                file.WriteLine(ex);
            }            
        }
        public void UpdatingButtonText(Button button, string text)
        {
            button.Text = text.ToString();
        }

    }

}
