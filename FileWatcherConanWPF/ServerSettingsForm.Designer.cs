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
            this.SaveButton = new System.Windows.Forms.Button();
            this.ServerSettingsGridView = new System.Windows.Forms.DataGridView();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrueOrFalse = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GameIniTextBox = new System.Windows.Forms.TextBox();
            this.ServerSettingCancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ServerSettingsGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // SaveButton
            // 
            this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveButton.Location = new System.Drawing.Point(1187, 349);
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
            this.ServerSettingsGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerSettingsGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.ServerSettingsGridView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ServerSettingsGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ServerSettingsGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Description,
            this.Value,
            this.TrueOrFalse});
            this.ServerSettingsGridView.Location = new System.Drawing.Point(480, 13);
            this.ServerSettingsGridView.Name = "ServerSettingsGridView";
            this.ServerSettingsGridView.RowHeadersVisible = false;
            this.ServerSettingsGridView.RowTemplate.Height = 28;
            this.ServerSettingsGridView.ShowEditingIcon = false;
            this.ServerSettingsGridView.Size = new System.Drawing.Size(865, 330);
            this.ServerSettingsGridView.TabIndex = 3;
            this.ServerSettingsGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewempl_CellClick);
            this.ServerSettingsGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ServerSettingsGridView_CellContentClick);
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
            this.Value.Width = 86;
            // 
            // TrueOrFalse
            // 
            this.TrueOrFalse.HeaderText = "True Or False";
            this.TrueOrFalse.Name = "TrueOrFalse";
            this.TrueOrFalse.Width = 111;
            // 
            // GameIniTextBox
            // 
            this.GameIniTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GameIniTextBox.Location = new System.Drawing.Point(13, 13);
            this.GameIniTextBox.Multiline = true;
            this.GameIniTextBox.Name = "GameIniTextBox";
            this.GameIniTextBox.Size = new System.Drawing.Size(461, 331);
            this.GameIniTextBox.TabIndex = 4;
            // 
            // ServerSettingCancelButton
            // 
            this.ServerSettingCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerSettingCancelButton.Location = new System.Drawing.Point(1268, 349);
            this.ServerSettingCancelButton.Name = "ServerSettingCancelButton";
            this.ServerSettingCancelButton.Size = new System.Drawing.Size(75, 34);
            this.ServerSettingCancelButton.TabIndex = 5;
            this.ServerSettingCancelButton.Text = "Cancel";
            this.ServerSettingCancelButton.UseVisualStyleBackColor = true;
            this.ServerSettingCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ServerSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1355, 385);
            this.ControlBox = false;
            this.Controls.Add(this.ServerSettingCancelButton);
            this.Controls.Add(this.GameIniTextBox);
            this.Controls.Add(this.ServerSettingsGridView);
            this.Controls.Add(this.SaveButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ServerSettingsForm";
            this.Text = "ServerSettingsForm";
            ((System.ComponentModel.ISupportInitialize)(this.ServerSettingsGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.DataGridView ServerSettingsGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewCheckBoxColumn TrueOrFalse;
        private System.Windows.Forms.TextBox GameIniTextBox;
        private System.Windows.Forms.Button ServerSettingCancelButton;
    }
}