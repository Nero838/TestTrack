namespace MC1
{
    partial class Form_SwitchloggerReset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SwitchloggerReset));
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.comboBoxServers = new System.Windows.Forms.ComboBox();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.labelLH1 = new System.Windows.Forms.Label();
            this.labelLH2 = new System.Windows.Forms.Label();
            this.labelLH3 = new System.Windows.Forms.Label();
            this.labelLH4 = new System.Windows.Forms.Label();
            this.labelLH5 = new System.Windows.Forms.Label();
            this.labelLH6 = new System.Windows.Forms.Label();
            this.labelL6Var = new System.Windows.Forms.Label();
            this.labelL5Var = new System.Windows.Forms.Label();
            this.labelL4Var = new System.Windows.Forms.Label();
            this.labelL3Var = new System.Windows.Forms.Label();
            this.labelL2Var = new System.Windows.Forms.Label();
            this.labelL1Var = new System.Windows.Forms.Label();
            this.labelTimer = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelStatus
            // 
            this.labelStatus.Location = new System.Drawing.Point(28, 23);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(261, 39);
            this.labelStatus.TabIndex = 1;
            this.labelStatus.Text = "Select line and click Reset...";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(113, 110);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(102, 23);
            this.buttonReset.TabIndex = 2;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // comboBoxServers
            // 
            this.comboBoxServers.FormattingEnabled = true;
            this.comboBoxServers.Items.AddRange(new object[] {
            "All",
            "Linehead 1 (old line 4)",
            "Linehead 2 (old line 5)",
            "Linehead 3 (old line 6)",
            "Linehead 4 (old line 7)",
            "Linehead 5 (old line 8, DMR, OOBA)",
            "Linehead 6 (old line 2,3)"});
            this.comboBoxServers.Location = new System.Drawing.Point(66, 83);
            this.comboBoxServers.Name = "comboBoxServers";
            this.comboBoxServers.Size = new System.Drawing.Size(198, 21);
            this.comboBoxServers.TabIndex = 3;
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.Image = global::TestTrack.Properties.Resources.loading1;
            this.pictureBoxLoading.Location = new System.Drawing.Point(137, 198);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(50, 50);
            this.pictureBoxLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLoading.TabIndex = 5;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // labelLH1
            // 
            this.labelLH1.AutoSize = true;
            this.labelLH1.Location = new System.Drawing.Point(4, 25);
            this.labelLH1.Name = "labelLH1";
            this.labelLH1.Size = new System.Drawing.Size(66, 13);
            this.labelLH1.TabIndex = 6;
            this.labelLH1.Text = "Linehead 1:";
            // 
            // labelLH2
            // 
            this.labelLH2.AutoSize = true;
            this.labelLH2.Location = new System.Drawing.Point(4, 43);
            this.labelLH2.Name = "labelLH2";
            this.labelLH2.Size = new System.Drawing.Size(66, 13);
            this.labelLH2.TabIndex = 7;
            this.labelLH2.Text = "Linehead 2:";
            // 
            // labelLH3
            // 
            this.labelLH3.AutoSize = true;
            this.labelLH3.Location = new System.Drawing.Point(4, 61);
            this.labelLH3.Name = "labelLH3";
            this.labelLH3.Size = new System.Drawing.Size(66, 13);
            this.labelLH3.TabIndex = 8;
            this.labelLH3.Text = "Linehead 3:";
            // 
            // labelLH4
            // 
            this.labelLH4.AutoSize = true;
            this.labelLH4.Location = new System.Drawing.Point(4, 79);
            this.labelLH4.Name = "labelLH4";
            this.labelLH4.Size = new System.Drawing.Size(66, 13);
            this.labelLH4.TabIndex = 9;
            this.labelLH4.Text = "Linehead 4:";
            // 
            // labelLH5
            // 
            this.labelLH5.AutoSize = true;
            this.labelLH5.Location = new System.Drawing.Point(4, 97);
            this.labelLH5.Name = "labelLH5";
            this.labelLH5.Size = new System.Drawing.Size(66, 13);
            this.labelLH5.TabIndex = 10;
            this.labelLH5.Text = "Linehead 5:";
            // 
            // labelLH6
            // 
            this.labelLH6.AutoSize = true;
            this.labelLH6.Location = new System.Drawing.Point(4, 115);
            this.labelLH6.Name = "labelLH6";
            this.labelLH6.Size = new System.Drawing.Size(66, 13);
            this.labelLH6.TabIndex = 11;
            this.labelLH6.Text = "Linehead 6:";
            // 
            // labelL6Var
            // 
            this.labelL6Var.Location = new System.Drawing.Point(73, 115);
            this.labelL6Var.Name = "labelL6Var";
            this.labelL6Var.Size = new System.Drawing.Size(219, 13);
            this.labelL6Var.TabIndex = 17;
            this.labelL6Var.Text = "No request";
            // 
            // labelL5Var
            // 
            this.labelL5Var.Location = new System.Drawing.Point(73, 97);
            this.labelL5Var.Name = "labelL5Var";
            this.labelL5Var.Size = new System.Drawing.Size(219, 13);
            this.labelL5Var.TabIndex = 16;
            this.labelL5Var.Text = "No request";
            // 
            // labelL4Var
            // 
            this.labelL4Var.Location = new System.Drawing.Point(73, 79);
            this.labelL4Var.Name = "labelL4Var";
            this.labelL4Var.Size = new System.Drawing.Size(219, 13);
            this.labelL4Var.TabIndex = 15;
            this.labelL4Var.Text = "No request";
            // 
            // labelL3Var
            // 
            this.labelL3Var.Location = new System.Drawing.Point(73, 61);
            this.labelL3Var.Name = "labelL3Var";
            this.labelL3Var.Size = new System.Drawing.Size(219, 13);
            this.labelL3Var.TabIndex = 14;
            this.labelL3Var.Text = "No request";
            // 
            // labelL2Var
            // 
            this.labelL2Var.Location = new System.Drawing.Point(73, 43);
            this.labelL2Var.Name = "labelL2Var";
            this.labelL2Var.Size = new System.Drawing.Size(219, 13);
            this.labelL2Var.TabIndex = 13;
            this.labelL2Var.Text = "No request";
            // 
            // labelL1Var
            // 
            this.labelL1Var.Location = new System.Drawing.Point(73, 25);
            this.labelL1Var.Name = "labelL1Var";
            this.labelL1Var.Size = new System.Drawing.Size(219, 13);
            this.labelL1Var.TabIndex = 12;
            this.labelL1Var.Text = "No request";
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTimer.Location = new System.Drawing.Point(117, 158);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(95, 45);
            this.labelTimer.TabIndex = 18;
            this.labelTimer.Text = "00:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(128, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Reset Timer:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelLH1);
            this.groupBox1.Controls.Add(this.labelLH2);
            this.groupBox1.Controls.Add(this.labelLH3);
            this.groupBox1.Controls.Add(this.labelL6Var);
            this.groupBox1.Controls.Add(this.labelLH4);
            this.groupBox1.Controls.Add(this.labelL5Var);
            this.groupBox1.Controls.Add(this.labelLH5);
            this.groupBox1.Controls.Add(this.labelL4Var);
            this.groupBox1.Controls.Add(this.labelLH6);
            this.groupBox1.Controls.Add(this.labelL3Var);
            this.groupBox1.Controls.Add(this.labelL1Var);
            this.groupBox1.Controls.Add(this.labelL2Var);
            this.groupBox1.Location = new System.Drawing.Point(6, 244);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(310, 143);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Reset status";
            // 
            // Form_SwitchloggerReset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(322, 392);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelTimer);
            this.Controls.Add(this.comboBoxServers);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.pictureBoxLoading);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(400, 500);
            this.MinimumSize = new System.Drawing.Size(338, 431);
            this.Name = "Form_SwitchloggerReset";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reset Switchlogger";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ComboBox comboBoxServers;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
        private System.Windows.Forms.Label labelLH1;
        private System.Windows.Forms.Label labelLH2;
        private System.Windows.Forms.Label labelLH3;
        private System.Windows.Forms.Label labelLH4;
        private System.Windows.Forms.Label labelLH5;
        private System.Windows.Forms.Label labelLH6;
        private System.Windows.Forms.Label labelL6Var;
        private System.Windows.Forms.Label labelL5Var;
        private System.Windows.Forms.Label labelL4Var;
        private System.Windows.Forms.Label labelL3Var;
        private System.Windows.Forms.Label labelL2Var;
        private System.Windows.Forms.Label labelL1Var;
        private System.Windows.Forms.Label labelTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}