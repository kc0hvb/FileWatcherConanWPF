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
                Form form = Application.OpenForms["SettingsForm"];
                if (form != null) form.Close();
                InitializeComponent();
                string sServerSource = dictionary["Conan_Server_Location"] + @"\ConanSandbox\Saved\Config\WindowsServer\";
                string sGameIniFile = sServerSource + "Game.ini";
                string sServerSettings = sServerSource + "ServerSettings.ini";
                AddingToList(sGameIniFile);
                AddingToList(sServerSettings);
            }
            
        }
        private void AddingToList(string pSource)
        {
            if (File.Exists(pSource))
            {
                string[] lines = File.ReadAllLines(pSource);
                foreach (string i in lines)
                {
                    if (pSource.Contains("Game")) GameIniTextBox.Text += i + "\r\n";
                    if (pSource.Contains("ServerSettings") && i.Contains("="))
                    {
                        string sDescription = i.Substring(0, i.IndexOf('='));
                        string sValue = i.Substring(i.LastIndexOf('=') + 1);
                        if (sValue.Contains("True"))
                            ServerSettingsGridView.Rows.Add(sDescription, sValue, true);
                        else if (sValue.Contains("False"))
                            ServerSettingsGridView.Rows.Add(sDescription, sValue, false);
                        else
                            ServerSettingsGridView.Rows.Add(sDescription, sValue, false);
                    }
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SavingGameIni();
            SavingServerSettingsIni();
            Application.OpenForms[1].Close();
        }

        private void ServerSettingsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void SavingGameIni()
        {
            Dictionary<string, string> dictionary = MaPro.PullValuesFromConfig();
            string sTempFile = Path.GetTempFileName();
            string sSourceGameIni = dictionary["Conan_Server_Location"] + @"\ConanSandbox\Saved\Config\WindowsServer\" + "Game.ini";
            File.WriteAllText(sTempFile, GameIniTextBox.Text);
            File.Delete(sSourceGameIni);
            File.Move(sTempFile, sSourceGameIni);
        }

        private void SavingServerSettingsIni()
        {
            Dictionary<string, string> dictionary = MaPro.PullValuesFromConfig();
            string sTempFile = Path.GetTempFileName();
            string sSourceServerIni = dictionary["Conan_Server_Location"] + @"\ConanSandbox\Saved\Config\WindowsServer\" + "ServerSettings.ini";
            List<string> sb = new List<string>();
            sb.Add("[ServerSettings]");
            using (StreamWriter sw = new StreamWriter(sTempFile))
            {
                try
                {
                    //Building a list to insert into a text document.
                    foreach (DataGridViewRow row in ServerSettingsGridView.Rows)
                    {
                        if (row.Cells["Description"].Value != null)
                        {
                            if (row.Cells["Value"].Value.ToString() != "" && row.Cells["Value"].Value.ToString() != null)
                                sb.Add(string.Format("{0}{1}{2}", row.Cells["Description"].Value.ToString(), ("=").ToString(), row.Cells["Value"].Value.ToString()));
                            else
                                sb.Add(string.Format("{0}{1}", row.Cells["Description"].Value.ToString(), ("=").ToString()));
                        }
                    }
                    //Inserting into the text document from list.
                    foreach (string i in sb)
                        sw.WriteLine(i);
                    sw.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            }

            File.Delete(sSourceServerIni);
            File.Move(sTempFile, sSourceServerIni);
        }

        private void dataGridViewempl_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ServerSettingsGridView.CurrentCell.ColumnIndex == 2)
            {
                if (ServerSettingsGridView.CurrentCell.Value != null)
                {
                    if (ServerSettingsGridView.CurrentRow.Cells[1].Value.ToString() == "True" || ServerSettingsGridView.CurrentRow.Cells[1].Value.ToString() == "False")
                    {
                        bool checkstate = (bool)ServerSettingsGridView.CurrentCell.GetEditedFormattedValue(e.RowIndex, DataGridViewDataErrorContexts.Commit);
                        if (checkstate == false)
                        {
                            ServerSettingsGridView.CurrentCell.Value = true;
                            ServerSettingsGridView.CurrentRow.Cells[1].Value = "True";
                        }

                        else
                        {
                            ServerSettingsGridView.CurrentCell.Value = false;
                            ServerSettingsGridView.CurrentRow.Cells[1].Value = "False";
                        }
                    }
                    else
                        ServerSettingsGridView.CurrentCell.Value = true;
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Application.OpenForms[1].Close();
        }
    }
}
