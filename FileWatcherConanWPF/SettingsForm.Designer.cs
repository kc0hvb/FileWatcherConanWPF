namespace FileWatcherConanWPF
{
    partial class SettingsForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.workshopPakLocationText = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.modTargetLocationText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.transferFilesCheck = new System.Windows.Forms.CheckBox();
            this.sleepTimeInt = new System.Windows.Forms.NumericUpDown();
            this.PakLocationButton = new System.Windows.Forms.Button();
            this.ModTargetButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ServerLocationButton = new System.Windows.Forms.Button();
            this.SteamCmdLocationButton = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.ConanServerLocationText = new System.Windows.Forms.TextBox();
            this.SteamCmdLocationText = new System.Windows.Forms.TextBox();
            this.ValidationCheckBox = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SettingsCancelButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sleepTimeInt)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Workshop/Source Pak Location";
            // 
            // workshopPakLocationText
            // 
            this.workshopPakLocationText.Location = new System.Drawing.Point(292, 57);
            this.workshopPakLocationText.Name = "workshopPakLocationText";
            this.workshopPakLocationText.Size = new System.Drawing.Size(218, 26);
            this.workshopPakLocationText.TabIndex = 3;
            // 
            // saveButton
            // 
            this.saveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveButton.Location = new System.Drawing.Point(468, 378);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 29);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // modTargetLocationText
            // 
            this.modTargetLocationText.Location = new System.Drawing.Point(292, 97);
            this.modTargetLocationText.Name = "modTargetLocationText";
            this.modTargetLocationText.Size = new System.Drawing.Size(220, 26);
            this.modTargetLocationText.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Sever Mod/Target Location";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(231, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Sleep Time for Comparing (sec)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(238, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Automatically Transfer PAK Files";
            // 
            // transferFilesCheck
            // 
            this.transferFilesCheck.AutoSize = true;
            this.transferFilesCheck.Location = new System.Drawing.Point(603, 166);
            this.transferFilesCheck.Name = "transferFilesCheck";
            this.transferFilesCheck.Size = new System.Drawing.Size(22, 21);
            this.transferFilesCheck.TabIndex = 10;
            this.transferFilesCheck.UseVisualStyleBackColor = true;
            // 
            // sleepTimeInt
            // 
            this.sleepTimeInt.Location = new System.Drawing.Point(504, 134);
            this.sleepTimeInt.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.sleepTimeInt.Name = "sleepTimeInt";
            this.sleepTimeInt.Size = new System.Drawing.Size(120, 26);
            this.sleepTimeInt.TabIndex = 11;
            // 
            // PakLocationButton
            // 
            this.PakLocationButton.Location = new System.Drawing.Point(518, 54);
            this.PakLocationButton.Name = "PakLocationButton";
            this.PakLocationButton.Size = new System.Drawing.Size(106, 32);
            this.PakLocationButton.TabIndex = 13;
            this.PakLocationButton.Text = "Browse";
            this.PakLocationButton.UseVisualStyleBackColor = true;
            this.PakLocationButton.Click += new System.EventHandler(this.PakLocationButton_Click);
            // 
            // ModTargetButton
            // 
            this.ModTargetButton.Location = new System.Drawing.Point(518, 92);
            this.ModTargetButton.Name = "ModTargetButton";
            this.ModTargetButton.Size = new System.Drawing.Size(106, 35);
            this.ModTargetButton.TabIndex = 14;
            this.ModTargetButton.Text = "Browse";
            this.ModTargetButton.UseVisualStyleBackColor = true;
            this.ModTargetButton.Click += new System.EventHandler(this.ModTargetButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 200);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(171, 20);
            this.label6.TabIndex = 15;
            this.label6.Text = "Conan Server Location";
            // 
            // ServerLocationButton
            // 
            this.ServerLocationButton.Location = new System.Drawing.Point(518, 192);
            this.ServerLocationButton.Name = "ServerLocationButton";
            this.ServerLocationButton.Size = new System.Drawing.Size(106, 34);
            this.ServerLocationButton.TabIndex = 16;
            this.ServerLocationButton.Text = "Browse";
            this.ServerLocationButton.UseVisualStyleBackColor = true;
            this.ServerLocationButton.Click += new System.EventHandler(this.ServerLocationButton_Click);
            // 
            // SteamCmdLocationButton
            // 
            this.SteamCmdLocationButton.Location = new System.Drawing.Point(518, 232);
            this.SteamCmdLocationButton.Name = "SteamCmdLocationButton";
            this.SteamCmdLocationButton.Size = new System.Drawing.Size(106, 34);
            this.SteamCmdLocationButton.TabIndex = 17;
            this.SteamCmdLocationButton.Text = "Browse";
            this.SteamCmdLocationButton.UseVisualStyleBackColor = true;
            this.SteamCmdLocationButton.Click += new System.EventHandler(this.SteamCmdLocationButton_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 240);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(154, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "SteamCmd Location";
            // 
            // ConanServerLocationText
            // 
            this.ConanServerLocationText.Location = new System.Drawing.Point(292, 197);
            this.ConanServerLocationText.Name = "ConanServerLocationText";
            this.ConanServerLocationText.Size = new System.Drawing.Size(218, 26);
            this.ConanServerLocationText.TabIndex = 19;
            // 
            // SteamCmdLocationText
            // 
            this.SteamCmdLocationText.Location = new System.Drawing.Point(292, 237);
            this.SteamCmdLocationText.Name = "SteamCmdLocationText";
            this.SteamCmdLocationText.Size = new System.Drawing.Size(218, 26);
            this.SteamCmdLocationText.TabIndex = 20;
            // 
            // ValidationCheckBox
            // 
            this.ValidationCheckBox.AutoSize = true;
            this.ValidationCheckBox.Location = new System.Drawing.Point(603, 272);
            this.ValidationCheckBox.Name = "ValidationCheckBox";
            this.ValidationCheckBox.Size = new System.Drawing.Size(22, 21);
            this.ValidationCheckBox.TabIndex = 21;
            this.ValidationCheckBox.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 272);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(210, 20);
            this.label8.TabIndex = 22;
            this.label8.Text = "Validate Program on Startup";
            // 
            // SettingsCancelButton
            // 
            this.SettingsCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SettingsCancelButton.Location = new System.Drawing.Point(549, 378);
            this.SettingsCancelButton.Name = "SettingsCancelButton";
            this.SettingsCancelButton.Size = new System.Drawing.Size(75, 29);
            this.SettingsCancelButton.TabIndex = 26;
            this.SettingsCancelButton.Text = "Cancel";
            this.SettingsCancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.SettingsCancelButton.UseVisualStyleBackColor = true;
            this.SettingsCancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 420);
            this.ControlBox = false;
            this.Controls.Add(this.SettingsCancelButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ValidationCheckBox);
            this.Controls.Add(this.SteamCmdLocationText);
            this.Controls.Add(this.ConanServerLocationText);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SteamCmdLocationButton);
            this.Controls.Add(this.ServerLocationButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ModTargetButton);
            this.Controls.Add(this.PakLocationButton);
            this.Controls.Add(this.sleepTimeInt);
            this.Controls.Add(this.transferFilesCheck);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.modTargetLocationText);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.workshopPakLocationText);
            this.Controls.Add(this.label2);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.sleepTimeInt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox workshopPakLocationText;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox modTargetLocationText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox transferFilesCheck;
        private System.Windows.Forms.NumericUpDown sleepTimeInt;
        private System.Windows.Forms.Button PakLocationButton;
        private System.Windows.Forms.Button ModTargetButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button ServerLocationButton;
        private System.Windows.Forms.Button SteamCmdLocationButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ConanServerLocationText;
        private System.Windows.Forms.TextBox SteamCmdLocationText;
        private System.Windows.Forms.CheckBox ValidationCheckBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button SettingsCancelButton;
    }
}