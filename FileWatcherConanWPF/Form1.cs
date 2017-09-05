﻿using System;
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
            if (!File.Exists(sConanServerFile) || sConanServerInstalled == "") ValidationConanServerButton.Text = "Install Server";
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

        private void OnTimedEvent()//object source, ElapsedEventArgs e)
        {
            if (button1.Enabled == false)
            {
                if (ConfigurationManager.AppSettings["Automaticaly_Transfer_Files"] == "true")
                {
                    var value = Pro.MainPortion();
                    string text = (String.Join(Environment.NewLine, value) + "\r\n");
                    bool isEmpty = !value.Any();
                    if (isEmpty) { }
                    else SetText(text);
                    Application.DoEvents();
                }
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
            //if (button1.Enabled == false)
            //{
                string sTarget = ConfigurationManager.AppSettings["PAK_Target_Location"];
                string[] fileEntries = Directory.GetFiles(sTarget, "*.*", System.IO.SearchOption.AllDirectories);
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
            //}
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
            //System.Timers.Timer aTimer = new System.Timers.Timer();
            //System.Timers.Timer bTimer = new System.Timers.Timer();
            //System.Timers.Timer cTimer = new System.Timers.Timer();
            //aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            //aTimer.Interval = Int32.Parse(ConfigurationManager.AppSettings["Sleep_Time"]);
            //aTimer.Enabled = true;
            OnTimedEvent();
            FillCheckBoxList();
            CheckBoxItemsInModText();
            button1.Enabled = false;
            button4.Enabled = true;
            ServerStartButton.Enabled = true;
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
            button1.Enabled = true;
            button4.Enabled = false;
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
            button1.Enabled = false;
            button4.Enabled = true;
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Invoke((MethodInvoker)(() => checkedListBox1.Items.Clear()));
            button1.Enabled = true;
            button4.Enabled = false;
        }
        #endregion

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button4.Enabled = false;
            SettingsForm frm = new SettingsForm();
            frm.Show();
        }

        private void ServerStartButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dictionary = MaPro.PullValuesFromConfig();
            bool bValidate = false;
            if (dictionary["Validate_Conan"] == "true") bValidate = true;
            MaPro.SteamCMDProcess(bValidate);
            MaPro.StartServer();
        }

        private void ValidationConanServerButton_Click(object sender, EventArgs e)
        {
            bool bDidUpdate = MaPro.SteamCMDProcess(true);
            if (bDidUpdate == true) ValidationConanServerButton.Text = "Validate Server";
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
            if (button1.Enabled == false)
            {
                if (modListBox.SelectedItem.ToString() != null)
                {
                    string value = modListBox.SelectedItem.ToString() + ".pak";
                    if (e.NewValue.ToString() == "Checked") MaPro.AddingModsInText(value);
                    else MaPro.RemovingModsInText(value);
                }
            }
        }

        private void CheckBoxItemsInModText()
        {
            Dictionary<string, string> dConfigValues = MaPro.PullValuesFromConfig();
            string[] sCheckedValues = MaPro.GetTextFromTextFile(dConfigValues);
            var myOtherList = modListBox.Items.Cast<String>().ToList();
            foreach (string i in sCheckedValues)
            {
                int curIndex = -1;
                string sFileNamePak = i.Replace(".pak", "");
                curIndex = modListBox.Items.IndexOf(sFileNamePak);
                if (curIndex > -1) modListBox.SetItemChecked(curIndex, true);
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
