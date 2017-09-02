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
            if (button1.Enabled == false)
            {
                    var value = Pro.MainPortion();
                    string text = (String.Join(Environment.NewLine, value) + "\r\n");
                    bool isEmpty = !value.Any();
                    if (isEmpty) { }
                    else SetText(text);
                    Application.DoEvents();
                    Thread.Sleep(Int32.Parse(ConfigurationManager.AppSettings["Sleep_Time"]));
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

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            bool bIsChecked = false;
            List<string> values = new List<string>();
                foreach (object o in checkedListBox1.SelectedItems)
                    values.Add(o.ToString());
            int iSelectIndex = checkedListBox1.SelectedIndex;
            if (iSelectIndex >= 0)
            {
                foreach (string i in values)
                {
                    ocCheckedItems.Add(i);
                    MaPro.SettingUpModsInText(i);
                }
                bIsChecked = checkedListBox1.GetItemChecked(iSelectIndex);
                if (bIsChecked == false) checkedListBox1.SetItemChecked(iSelectIndex, true);
                else checkedListBox1.SetItemChecked(iSelectIndex, false);
            }
        }
        
        public void FillCheckBoxList()//object source, ElapsedEventArgs e)
        {
            if (button1.Enabled == false)
            {
                //this.Invoke((MethodInvoker)(() => checkedListBox1.Items.Clear()));
                string sTarget = ConfigurationManager.AppSettings["PAK_Target_Location"];
                string[] fileEntries = Directory.GetFiles(sTarget, "*.*", System.IO.SearchOption.AllDirectories);
                foreach (string fileName in fileEntries)
                {
                    if (fileName.Contains(".pak"))
                    {
                        string fFileWithExt = Path.GetFileNameWithoutExtension(fileName);
                        if (!checkedListBox1.Items.Contains(fFileWithExt))
                        {
                            this.Invoke((MethodInvoker)(() => checkedListBox1.Items.Add(fFileWithExt)));
                        }
                    }
                }
            }
        }

        public ObservableCollection<string> GetCheckedItems()
        {
            List<string> values = new List<string>();
            int iSelectIndex = checkedListBox1.SelectedIndex;
            var test = checkedListBox1.SelectedItem;
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
            button1.Enabled = false;
            button4.Enabled = true;
            System.Timers.Timer aTimer = new System.Timers.Timer();
            System.Timers.Timer bTimer = new System.Timers.Timer();
            System.Timers.Timer cTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = Int32.Parse(ConfigurationManager.AppSettings["Sleep_Time"]);
            aTimer.Enabled = true;
            //bTimer.Elapsed += new ElapsedEventHandler(FillCheckBoxList);
            //bTimer.Elapsed += new ElapsedEventHandler(MaPro.SettingUpModsInText);
            bTimer.Interval = 5000;
            bTimer.Enabled = true;
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
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MaPro.SettingUpModsInText(null);
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
            System.Timers.Timer aTimer = new System.Timers.Timer();
            System.Timers.Timer bTimer = new System.Timers.Timer();
            System.Timers.Timer cTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = Int32.Parse(ConfigurationManager.AppSettings["Sleep_Time"]);
            aTimer.Enabled = true;
            //bTimer.Elapsed += new ElapsedEventHandler(FillCheckBoxList);
            //bTimer.Elapsed += new ElapsedEventHandler(MaPro.SettingUpModsInText);
            bTimer.Interval = 5000;
            bTimer.Enabled = true;
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //this.Invoke((MethodInvoker)(() => checkedListBox1.Items.Clear()));
            button1.Enabled = true;
            button4.Enabled = false;
        }
        #endregion
    }
}
