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
                    if (pSource.Contains("Game")) GameIniListBox.Items.Add(i);
                    if (pSource.Contains("ServerSettings"))
                    {
                        if (i.Contains("="))
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
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> dictionary = MaPro.PullValuesFromConfig();
            string sTempFile = Path.GetTempFileName();
            string sSource = dictionary["Conan_Server_Location"] + @"\ConanSandbox\Saved\Config\WindowsServer\" + "ServerSettings.ini";
            StringBuilder builder = new StringBuilder();
            int rowcount = ServerSettingsGridView.Rows.Count;
            int columncount = ServerSettingsGridView.Columns.Count;
            List<string> headerCols = new List<string>();
            for (int j = 0; j < columncount - 1; j++)
            {
                headerCols.Add(ServerSettingsGridView.Columns[j].HeaderText);
            }
            builder.AppendLine(string.Join("\t", headerCols));

            for (int i = 0; i < rowcount - 1; i++)
            {
                List<string> cols = new List<string>();
                for (int j = 0; j < columncount - 2; j++)
                {
                    cols.Add(ServerSettingsGridView.Rows[i].Cells[j].Value.ToString());
                }
                builder.AppendLine(string.Join("\t", cols.ToArray()));
            }
            System.IO.File.WriteAllText(sTempFile, builder.ToString());
            File.Delete(sSource);
            File.Move(sTempFile, sSource);
            Application.OpenForms[1].Close();
        }

        private void ServerSettingsGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
    }
}
