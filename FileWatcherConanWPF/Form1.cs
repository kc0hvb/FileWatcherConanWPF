using System;
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
    public partial class ConanModWatcher : Form
    {
        
        public ConanModWatcher()
        {
            InitializeComponent();
        }

        private void Form1_Resize(object sender, System.EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }
        
        public void TextBox1(List<string> value)
        {
            string value1 = String.Join(Environment.NewLine, value);
            textBox1.AppendText(value1);
        }

        private void notifyIcon1_DoubleClick(object sender, System.EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Program Pro = new Program();
            Pro.MainPortion();
        }
    }
}
