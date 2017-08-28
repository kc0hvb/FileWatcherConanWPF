using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace FileWatcherConanWPF
{
    public partial class ConanModWatcher : Form
    {

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

        private void button1_Click_1(object sender, EventArgs e)
        {
            button1.Enabled = false;
            var worker = new BackgroundWorker();
            worker.WorkerReportsProgress = false;
            worker.WorkerSupportsCancellation = false;
            worker.DoWork += backgroundWorker1_DoWork;
            worker.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if (button1.Enabled == false)
            {
                while (true)
                {
                    Program Pro = new Program();
                    var value = Pro.MainPortion();
                    string text = (String.Join(Environment.NewLine, value) + "\r\n");
                    bool isEmpty = !value.Any();
                    if (isEmpty) { }
                    else SetText(text);
                    Application.DoEvents();
                    Thread.Sleep(Int32.Parse(ConfigurationManager.AppSettings["Sleep_Time"]));
                }
            }
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
    }
}
