﻿using System;
using System.Configuration;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileWatcherConanWPF
{
    public class SettingsFormProperties
    {
        //dValuesFromConfig.Add("PAK_Location", config.AppSettings.Settings["PAK_Location"].Value);
        //dValuesFromConfig.Add("PAK_Target_Location", config.AppSettings.Settings["PAK_Target_Location"].Value);
        //dValuesFromConfig.Add("Sleep_Time", config.AppSettings.Settings["Sleep_Time"].Value);
        //dValuesFromConfig.Add("Automaticaly_Transfer_Files", config.AppSettings.Settings["Automaticaly_Transfer_Files"].Value);
        //dValuesFromConfig.Add("Conan_Server_Location", config.AppSettings.Settings["Conan_Server_Location"].Value);
        //dValuesFromConfig.Add("SteamCmd_Location", config.AppSettings.Settings["SteamCmd_Location"].Value);
        //dValuesFromConfig.Add("Validate_Conan", config.AppSettings.Settings["Validate_Conan"].Value);
        //dValuesFromConfig.Add("Batch_Location", config.AppSettings.Settings["Batch_Location"].Value);
        //dValuesFromConfig.Add("Arguements_Server_Start", config.AppSettings.Settings["Arguements_Server_Start"].Value);
        
    }
    public partial class SettingsForm : Form
    {
        public string PakLocation
        {
            get { return workshopPakLocationText.Text; }
            set { workshopPakLocationText.Text = value; }
        }

        public string PakTargetLocation
        {
            get { return modTargetLocationText.Text; }
            set { modTargetLocationText.Text = value; }
        }

        public int SleepTime
        {
            get { return Convert.ToInt32(sleepTimeInt.Value); }
            set { sleepTimeInt.Value = Convert.ToInt32(value); }
        }

        public SettingsForm()
        {
            InitializeComponent();
            settingValues();
        }

        private void settingValues()
        {
            try
            {
                string appPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string configFile = System.IO.Path.Combine(appPath, "Conan Exiles Server Admin.exe.config");
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = configFile;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                workshopPakLocationText.Text = config.AppSettings.Settings["PAK_Location"].Value;
                if (config.AppSettings.Settings["Sleep_Time"].Value != ("").ToString() && config.AppSettings.Settings["Sleep_Time"].Value != ("0").ToString())
                {
                    int sSleepTime = (System.Int32.Parse(config.AppSettings.Settings["Sleep_Time"].Value) / 1000);
                    sleepTimeInt.Value = sSleepTime;
                }
                modTargetLocationText.Text = config.AppSettings.Settings["PAK_Target_Location"].Value;
                if (config.AppSettings.Settings["Automaticaly_Transfer_Files"].Value == "true") transferFilesCheck.Checked = true;
                else transferFilesCheck.Checked = false;
                ConanServerLocationText.Text = config.AppSettings.Settings["Conan_Server_Location"].Value;
                SteamCmdLocationText.Text = config.AppSettings.Settings["SteamCmd_Location"].Value;
                if (config.AppSettings.Settings["Validate_Conan"].Value == "true") ValidationCheckBox.Checked = true;
                else ValidationCheckBox.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string appPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configFile = System.IO.Path.Combine(appPath, "Conan Exiles Server Admin.exe.config");
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            
            if (workshopPakLocationText.Text != "") config.AppSettings.Settings["PAK_Location"].Value = workshopPakLocationText.Text.ToString();
            if (modTargetLocationText.Text != "") config.AppSettings.Settings["PAK_Target_Location"].Value = modTargetLocationText.Text.ToString();
            decimal iSleepTime = (sleepTimeInt.Value * 1000);
            if (sleepTimeInt.Value >= 0) config.AppSettings.Settings["Sleep_Time"].Value = iSleepTime.ToString();
            if (transferFilesCheck.Checked == true) config.AppSettings.Settings["Automaticaly_Transfer_Files"].Value = "true";
            else config.AppSettings.Settings["Automaticaly_Transfer_Files"].Value = "false";
            if (ConanServerLocationText.Text != "") config.AppSettings.Settings["Conan_Server_Location"].Value = ConanServerLocationText.Text.ToString();
            if (SteamCmdLocationText.Text != "") config.AppSettings.Settings["SteamCmd_Location"].Value = SteamCmdLocationText.Text.ToString();
            if (ValidationCheckBox.Checked == true) config.AppSettings.Settings["Validate_Conan"].Value = "true";
            else config.AppSettings.Settings["Validate_Conan"].Value = "false";
            config.Save();
            GettingSettings Settings = new GettingSettings();
            Settings.SettingValuesFromConfig();

            Application.OpenForms[1].Close();
        }

        private void ModTextFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Mod File|modlist.txt";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string sModTextFile = fileDialog.FileName;
            }
        }

        private void PakLocationButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.SelectedPath = @"C:\";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string sPakLocation = folderBrowser.SelectedPath;
                workshopPakLocationText.Text = sPakLocation;
            }
        }

        private void ModTargetButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.SelectedPath = @"C:\";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string sPakTargetLocation = folderBrowser.SelectedPath;
                modTargetLocationText.Text = sPakTargetLocation;
            }
        }

        private void ServerLocationButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.SelectedPath = @"C:\";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string sConanServerLocation = folderBrowser.SelectedPath;
                ConanServerLocationText.Text = sConanServerLocation;
            }
        }

        private void SteamCmdLocationButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "steamcmd|*.exe";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string sSteamcCmdLocation = fileDialog.FileName;
                SteamCmdLocationText.Text = sSteamcCmdLocation;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Application.OpenForms[1].Close();
        }
    }
}
