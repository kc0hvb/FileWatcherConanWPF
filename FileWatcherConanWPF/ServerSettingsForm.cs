using System;
using System.IO;
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
    public partial class ServerSettingsForm : Form
    {
        private MainProgram MaPro = new MainProgram();

        public ServerSettingsForm()
        {
            Dictionary<string, string> dictionary = MaPro.PullValuesFromConfig();
            if (dictionary["Conan_Server_Location"] != "")
            {
                InitializeComponent();
                string sServerSource = dictionary["Conan_Server_Location"] + @"\ConanSandbox\Saved\Config\WindowsServer\";
                string sGameIniFile = sServerSource + "Game.ini";
                string sServerSettings = sServerSource + "ServerSettings.ini";
                AddingToList(sGameIniFile);
            }
        }
        private void AddingToList (string pSource)
        {
            if (File.Exists(pSource))
            {
                string [] lines = File.ReadAllLines(pSource);
                foreach (string i in lines)
                {
                    GameIniListBox.Items.Add(i);
                }
            }
        }
    }
}
