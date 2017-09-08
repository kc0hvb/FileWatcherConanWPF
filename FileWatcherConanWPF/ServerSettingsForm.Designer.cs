namespace FileWatcherConanWPF
{
    partial class ServerSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.GameIniListBox = new System.Windows.Forms.ListBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.ServerSettingsGridView = new System.Windows.Forms.DataGridView();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrueOrFalse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ServerSettingsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GameIniListBox
            // 
            this.GameIniListBox.FormattingEnabled = true;
            this.GameIniListBox.ItemHeight = 20;
            this.GameIniListBox.Location = new System.Drawing.Point(20, 20);
            this.GameIniListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GameIniListBox.Name = "GameIniListBox";
            this.GameIniListBox.Size = new System.Drawing.Size(452, 324);
            this.GameIniListBox.TabIndex = 0;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(1351, 310);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 34);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ServerSettingsGridView
            // 
            this.ServerSettingsGridView.AllowUserToOrderColumns = true;
            this.ServerSettingsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ServerSettingsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Description,
            this.Value,
            this.TrueOrFalse});
            this.ServerSettingsGridView.Location = new System.Drawing.Point(480, 20);
            this.ServerSettingsGridView.Name = "ServerSettingsGridView";
            this.ServerSettingsGridView.RowTemplate.Height = 28;
            this.ServerSettingsGridView.Size = new System.Drawing.Size(865, 323);
            this.ServerSettingsGridView.TabIndex = 3;
            this.ServerSettingsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ServerSettingsGridView_CellContentClick);
            this.ServerSettingsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridViewempl_CellClick);
            // 
            // Description
            // 
            this.Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 125;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // TrueOrFalse
            // 
            this.TrueOrFalse.HeaderText = "True Or False";
            this.TrueOrFalse.Name = "TrueOrFalse";
            // 
            // ServerSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1438, 363);
            this.ControlBox = false;
            this.Controls.Add(this.ServerSettingsGridView);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.GameIniListBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ServerSettingsForm";
            this.Text = "ServerSettingsForm";
            ((System.ComponentModel.ISupportInitialize)(this.ServerSettingsGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox GameIniListBox;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridView ServerSettingsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TrueOrFalse;
    }
}