using System;
using System.IO;
using System.Timers;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Reflection;
using System.Deployment.Application;

namespace FileWatcherConanWPF
{
    public partial class ConanModWatcher : Form
    {
        private Program Pro = new Program();
        private MainProgram MaPro = new MainProgram();
        private ObservableCollection<string> ocCheckedItems = new ObservableCollection<string>();

        public ConanModWatcher()
        {
            InitializeComponent();
            Dictionary<string, string> dConfigValues = MaPro.PullValuesFromConfig();
            string sConanServerInstalled = dConfigValues["Conan_Server_Location"];
            string sConanServerFile = sConanServerInstalled + @"\ConanSandboxServer.exe";
            if (dConfigValues["Arguements_Server_Start"] != "") ArgumentTextBox.Text = dConfigValues["Arguements_Server_Start"];
            else ArgumentTextBox.Text = "-log -QueryPort=27016";
            System.Timers.Timer tButtonTimer = new System.Timers.Timer();
            tButtonTimer.Elapsed += new ElapsedEventHandler(SetInstallVerifyServer);
            tButtonTimer.Interval = Int32.Parse("5000");
            tButtonTimer.Enabled = true;
            stopToolStripMenuItem.Enabled = false;
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            versionNumberToolStripMenuItem.Text = String.Format("Version {0}", version);
            if (!File.Exists(sConanServerFile) || sConanServerInstalled == "")
            {
                ValidationConanServerButton.Text = "Install/Setup Server";
                StartButton.Enabled = true;
                severSettingsToolStripMenuItem.Enabled = false;
                ArgumentTextBox.Enabled = false;
            }
        }

        private void SetInstallVerifyServer(object source, ElapsedEventArgs e)
        {
            Dictionary<string, string> dConfigValues = MaPro.PullValuesFromConfig();
            if (dConfigValues != null)
            {
                if (dConfigValues["Conan_Server_Location"] != "" && File.Exists(dConfigValues["Conan_Server_Location"] + @"\ConanSandboxServer.exe"))
                {
                    ValidationConanServerButton.Invoke(new MethodInvoker(() => { ValidationConanServerButton.Text = "Verify Server"; ServerStartButton.Enabled = true; ArgumentTextBox.Enabled = true; severSettingsToolStripMenuItem.Enabled = true; }));
                    Application.DoEvents();
                }
                else ValidationConanServerButton.Invoke(new MethodInvoker(() => { ValidationConanServerButton.Text = "Install/Setup Server"; severSettingsToolStripMenuItem.Enabled = false; }));
                Application.DoEvents();
                if (StartButton.Enabled == false) startToolStripMenuItem.Enabled = false;
                else startToolStripMenuItem.Enabled = true;
                if (StopButton.Enabled == false) stopToolStripMenuItem.Enabled = false;
                else stopToolStripMenuItem.Enabled = true;
            }
        }

        delegate void SetTextCallback(string text);

        private void SetText(string text)
        {
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.AppendText(text);
            }
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (StartButton.Enabled == false)
            {
                var value = Pro.MainPortion();
                string text = (String.Join(Environment.NewLine, value) + "\r\n");
                bool isEmpty = !value.Any();
                if (isEmpty) { }
                else SetText(text);
                Application.DoEvents();
                Dictionary<string, string> dictionary = MaPro.PullValuesFromConfig();
                if (dictionary != null)
                    if (dictionary["Conan_Server_Location"] != "") FillCheckBoxList();
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        public void FillCheckBoxList()//object source, ElapsedEventArgs e)
        {
            if (ConfigurationManager.AppSettings["PAK_Target_Location"] != "")
            {
                string sTarget = ConfigurationManager.AppSettings["PAK_Target_Location"];
                string[] fileEntries = Directory.GetFiles(sTarget, "*.*", SearchOption.AllDirectories);
                foreach (string fileName in fileEntries)
                {
                    if (fileName.Contains(".pak"))
                    {
                        string fFileWithExt = Path.GetFileNameWithoutExtension(fileName);
                        if (!modListBox.Items.Contains(fFileWithExt))
                        {
                            this.Invoke((MethodInvoker)(() => modListBox.Items.Add(fFileWithExt)));
                        }
                    }
                }
            }
        }

        public ObservableCollection<string> GetCheckedItemsOld()
        {
            List<string> values = new List<string>();
            int iSelectIndex = modListBox.SelectedIndex;
            var test = modListBox.SelectedItem;
            //foreach (object o in checkedListBox1.SelectedItems) values.Add(o.ToString());
            foreach (string i in values)
            {
                ocCheckedItems.Add(i);
            }

            return ocCheckedItems;
        }

        #region Button Code
        private void button1_Click_1(object sender, EventArgs e)
        {
            StartProgram();
            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Hide();
            notifyIcon1.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //this.Invoke((MethodInvoker)(() => checkedListBox1.Items.Clear()));
            StartButton.Enabled = true;
            StopButton.Enabled = false;
            stopToolStripMenuItem.Enabled = false;
            startToolStripMenuItem.Enabled = true;
            ServerStartButton.Enabled = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MaPro.AddingModsInText(null);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FillCheckBoxList();
        }
        #endregion

        #region Menu drop down fields
        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Hide();
            notifyIcon1.Visible = true;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartProgram();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Invoke((MethodInvoker)(() => checkedListBox1.Items.Clear()));
            StartButton.Enabled = true;
            StopButton.Enabled = false;
        }
        #endregion

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm frm = new SettingsForm();
            frm.ShowDialog();
        }

        private void ServerStartButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dictionary = MaPro.PullValuesFromConfig();
            bool bValidate = false;
            if (dictionary["Validate_Conan"] == "true") bValidate = true;
            if (ServerStartButton.Text == "Start Server")
            {
                MaPro.SteamCMDProcess(bValidate);
                MaPro.StartServer(ArgumentTextBox.Text.ToString());
                ServerStartButton.Text = "Stop Server";
            }
            else if (ServerStartButton.Text == "Stop Server")
            {
                MaPro.StopServer();
                ServerStartButton.Text = "Start Server";
            }            
        }

        private void ValidationConanServerButton_Click(object sender, EventArgs e)
        {
            if (ValidationConanServerButton.Text.ToString() == "Install/Setup Server")
            {
                string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string configFile = Path.Combine(appPath, "Conan Exiles Server Admin.exe.config");
                ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
                configFileMap.ExeConfigFilename = configFile;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

                string sSteamCmd = "";
                string sServerLoc = "";
                MessageBox.Show("Please select the location of SteamCmd File.");
                OpenFileDialog fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    sSteamCmd = fileDialog.FileName;
                }
                MessageBox.Show("Please select the location of the Server");
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        sServerLoc = fbd.SelectedPath;                        
                    }
                }
                if (sServerLoc.ToString() != "") config.AppSettings.Settings["Conan_Server_Location"].Value = sServerLoc.ToString();
                if (sSteamCmd.ToString() != "") config.AppSettings.Settings["SteamCmd_Location"].Value = sSteamCmd.ToString();
                config.Save();
                bool bInstalled = MaPro.SteamCMDProcess(false, sSteamCmd, sServerLoc);
                if (bInstalled == true)
                {
                    StartButton.Enabled = true;
                    ArgumentTextBox.Enabled = true;
                }
            }
            else MaPro.SteamCMDProcess(true);
        }

        public List<string> GetCheckedItems()
        {
            List<string> list = new List<string>();
            if (modListBox.CheckedItems.Count > 0)
            {
                foreach (string i in modListBox.CheckedItems)
                    list.Add(i);
            }
            return list;
        }

        private void modListBox_ItemCheck(Object sender, ItemCheckEventArgs e)
        {
            if (StartButton.Enabled == false && modListBox.SelectedItem.ToString() != null)
            {
                string value = modListBox.SelectedItem.ToString() + ".pak";
                if (e.NewValue.ToString() == "Checked") MaPro.AddingModsInText(value);
                else MaPro.RemovingModsInText(value);
            }
        }

        private void CheckBoxItemsInModText()
        {
            Dictionary<string, string> dConfigValues = MaPro.PullValuesFromConfig();
            string[] sCheckedValues = MaPro.GetTextFromTextFile(dConfigValues);
            var myOtherList = modListBox.Items.Cast<String>().ToList();
            if (sCheckedValues != null)
            {
                foreach (string i in sCheckedValues)
                {
                    int curIndex = -1;
                    string sFileNamePak = i.Replace(".pak", "");
                    curIndex = modListBox.Items.IndexOf(sFileNamePak);
                    if (curIndex > -1) modListBox.SetItemChecked(curIndex, true);
                }
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void severSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dictionary = MaPro.PullValuesFromConfig();
            if (File.Exists(dictionary["Conan_Server_Location"] + @"\ConanSandbox\Saved\Config\WindowsServer\Game.ini"))
            {

                bool bIsRunning = MaPro.ServerIsRunning();
                if (bIsRunning == false && dictionary["Conan_Server_Location"] != "")
                {
                    ServerSettingsForm form = new ServerSettingsForm();
                    form.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please stop the server before editing.");
                }
            }
            else
            {
                MessageBox.Show("Please wait a moment while we create the files for the server settings.");
                MaPro.StartServer(ArgumentTextBox.Text.ToString());
                Thread.Sleep(30000);
                MaPro.StopServer();
                Thread.Sleep(5000);
                ServerSettingsForm form = new ServerSettingsForm();
                form.Show();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string configFile = Path.Combine(appPath, "Conan Exiles Server Admin.exe.config");
            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configFile;
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
            config.AppSettings.Settings["Arguements_Server_Start"].Value = ArgumentTextBox.Text.ToString();
            config.Save();
        }

        private void StartProgram()
        {
            try
            {
                Dictionary<string, string> dictionary = MaPro.PullValuesFromConfig();
                if (dictionary["Conan_Server_Location"] != "" && !Directory.Exists(dictionary["Conan_Server_Location"] + @"\ConanSandbox\Mods\"))
                {
                    Directory.CreateDirectory(dictionary["Conan_Server_Location"] + @"\ConanSandbox\Mods\");
                    string sSource = dictionary["Conan_Server_Location"] + @"\ConanSandbox\Mods\modlist.txt";
                    if (!File.Exists(sSource))
                    {
                        StreamWriter sw = new StreamWriter(sSource);
                        sw.Close();
                    }
                }
                if (dictionary["Sleep_Time"] != "" && dictionary["PAK_Location"] != "" && dictionary["PAK_Target_Location"] != "")
                {
                    System.Timers.Timer aTimer = new System.Timers.Timer();
                    aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    aTimer.Interval = Int32.Parse(dictionary["Sleep_Time"]);
                    aTimer.Enabled = true;
                    if (dictionary["Conan_Server_Location"] != "")
                    {
                        FillCheckBoxList();
                        CheckBoxItemsInModText();
                    }
                    StartButton.Enabled = false;
                    StopButton.Enabled = true;
                    ServerStartButton.Enabled = true;
                }
                else
                {
                    StartButton.Enabled = true;
                    StopButton.Enabled = false;
                    MessageBox.Show("Please choose the source and target location for the PAK files. Also the time interval in the settings.\r\nNote: Target location could mean the server location or a shared folder location.");
                    SettingsForm settingsForm = new SettingsForm();
                    settingsForm.Show();
                }

            }
            catch
            {
                MessageBox.Show("Please Setup the conifguration first.");
                SettingsForm form = new SettingsForm();
                form.Show();
            }
        }
    }
}
