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
            this.ServerSettingListBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // GameIniListBox
            // 
            this.GameIniListBox.FormattingEnabled = true;
            this.GameIniListBox.Location = new System.Drawing.Point(13, 13);
            this.GameIniListBox.Name = "GameIniListBox";
            this.GameIniListBox.Size = new System.Drawing.Size(303, 212);
            this.GameIniListBox.TabIndex = 0;
            // 
            // ServerSettingListBox
            // 
            this.ServerSettingListBox.FormattingEnabled = true;
            this.ServerSettingListBox.Location = new System.Drawing.Point(322, 13);
            this.ServerSettingListBox.Name = "ServerSettingListBox";
            this.ServerSettingListBox.Size = new System.Drawing.Size(303, 212);
            this.ServerSettingListBox.TabIndex = 1;
            // 
            // ServerSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(715, 236);
            this.ControlBox = false;
            this.Controls.Add(this.ServerSettingListBox);
            this.Controls.Add(this.GameIniListBox);
            this.Name = "ServerSettingsForm";
            this.Text = "ServerSettingsForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox GameIniListBox;
        private System.Windows.Forms.ListBox ServerSettingListBox;
    }
}