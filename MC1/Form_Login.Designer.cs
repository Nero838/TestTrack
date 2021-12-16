namespace MC1
{
    partial class Form_Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Login));
            this.textBoxPW = new System.Windows.Forms.TextBox();
            this.buttonGuest = new System.Windows.Forms.Button();
            this.buttonTE = new System.Windows.Forms.Button();
            this.buttonTT = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labelLoginVar = new System.Windows.Forms.Label();
            this.labelStatusLogin = new System.Windows.Forms.Label();
            this.labelVerFix = new System.Windows.Forms.Label();
            this.labelVerVar = new System.Windows.Forms.Label();
            this.labelVerStat = new System.Windows.Forms.Label();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.labelVerStatFix = new System.Windows.Forms.Label();
            this.labelConnectionStatFix = new System.Windows.Forms.Label();
            this.labelAuthStatFix = new System.Windows.Forms.Label();
            this.labelConnectionStat = new System.Windows.Forms.Label();
            this.labelAuthStat = new System.Windows.Forms.Label();
            this.labelSqlConnectionStat = new System.Windows.Forms.Label();
            this.labelSqlConnectionStatFix = new System.Windows.Forms.Label();
            this.buttonTest = new System.Windows.Forms.Button();
            this.pictureBoxLoadingGif = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoadingGif)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxPW
            // 
            this.textBoxPW.Location = new System.Drawing.Point(3, 120);
            this.textBoxPW.Name = "textBoxPW";
            this.textBoxPW.PasswordChar = '*';
            this.textBoxPW.Size = new System.Drawing.Size(117, 22);
            this.textBoxPW.TabIndex = 1;
            // 
            // buttonGuest
            // 
            this.buttonGuest.Location = new System.Drawing.Point(3, 3);
            this.buttonGuest.Name = "buttonGuest";
            this.buttonGuest.Size = new System.Drawing.Size(117, 33);
            this.buttonGuest.TabIndex = 2;
            this.buttonGuest.Text = "Guest";
            this.buttonGuest.UseVisualStyleBackColor = true;
            this.buttonGuest.Click += new System.EventHandler(this.buttonGuest_Click);
            // 
            // buttonTE
            // 
            this.buttonTE.Location = new System.Drawing.Point(3, 42);
            this.buttonTE.Name = "buttonTE";
            this.buttonTE.Size = new System.Drawing.Size(117, 33);
            this.buttonTE.TabIndex = 3;
            this.buttonTE.Text = "Engineer";
            this.buttonTE.UseVisualStyleBackColor = true;
            this.buttonTE.Click += new System.EventHandler(this.buttonTE_Click);
            // 
            // buttonTT
            // 
            this.buttonTT.Location = new System.Drawing.Point(3, 81);
            this.buttonTT.Name = "buttonTT";
            this.buttonTT.Size = new System.Drawing.Size(117, 33);
            this.buttonTT.TabIndex = 4;
            this.buttonTT.Text = "Technician";
            this.buttonTT.UseVisualStyleBackColor = true;
            this.buttonTT.Click += new System.EventHandler(this.buttonTT_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonGuest);
            this.panel1.Controls.Add(this.buttonTT);
            this.panel1.Controls.Add(this.textBoxPW);
            this.panel1.Controls.Add(this.buttonTE);
            this.panel1.Location = new System.Drawing.Point(91, 200);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 147);
            this.panel1.TabIndex = 5;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 11);
            this.label1.TabIndex = 11;
            this.label1.Text = "TT password:";
            // 
            // labelLoginVar
            // 
            this.labelLoginVar.AutoSize = true;
            this.labelLoginVar.Location = new System.Drawing.Point(91, 184);
            this.labelLoginVar.Name = "labelLoginVar";
            this.labelLoginVar.Size = new System.Drawing.Size(52, 13);
            this.labelLoginVar.TabIndex = 6;
            this.labelLoginVar.Text = "Enter As:";
            this.labelLoginVar.Click += new System.EventHandler(this.labelLoginVar_Click);
            // 
            // labelStatusLogin
            // 
            this.labelStatusLogin.Location = new System.Drawing.Point(35, 353);
            this.labelStatusLogin.Name = "labelStatusLogin";
            this.labelStatusLogin.Size = new System.Drawing.Size(228, 18);
            this.labelStatusLogin.TabIndex = 7;
            this.labelStatusLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelVerFix
            // 
            this.labelVerFix.AutoSize = true;
            this.labelVerFix.Location = new System.Drawing.Point(12, 631);
            this.labelVerFix.Name = "labelVerFix";
            this.labelVerFix.Size = new System.Drawing.Size(48, 13);
            this.labelVerFix.TabIndex = 8;
            this.labelVerFix.Text = "Version:";
            // 
            // labelVerVar
            // 
            this.labelVerVar.AutoSize = true;
            this.labelVerVar.Location = new System.Drawing.Point(66, 631);
            this.labelVerVar.Name = "labelVerVar";
            this.labelVerVar.Size = new System.Drawing.Size(0, 13);
            this.labelVerVar.TabIndex = 9;
            // 
            // labelVerStat
            // 
            this.labelVerStat.AutoSize = true;
            this.labelVerStat.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVerStat.Location = new System.Drawing.Point(228, 569);
            this.labelVerStat.Name = "labelVerStat";
            this.labelVerStat.Size = new System.Drawing.Size(0, 11);
            this.labelVerStat.TabIndex = 10;
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(194, 627);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(98, 21);
            this.buttonUpdate.TabIndex = 5;
            this.buttonUpdate.Text = "Manual Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Visible = false;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // labelVerStatFix
            // 
            this.labelVerStatFix.AutoSize = true;
            this.labelVerStatFix.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVerStatFix.Location = new System.Drawing.Point(49, 569);
            this.labelVerStatFix.Name = "labelVerStatFix";
            this.labelVerStatFix.Size = new System.Drawing.Size(173, 11);
            this.labelVerStatFix.TabIndex = 12;
            this.labelVerStatFix.Text = "Requested version.......";
            this.labelVerStatFix.Visible = false;
            // 
            // labelConnectionStatFix
            // 
            this.labelConnectionStatFix.AutoSize = true;
            this.labelConnectionStatFix.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectionStatFix.Location = new System.Drawing.Point(49, 549);
            this.labelConnectionStatFix.Name = "labelConnectionStatFix";
            this.labelConnectionStatFix.Size = new System.Drawing.Size(173, 11);
            this.labelConnectionStatFix.TabIndex = 13;
            this.labelConnectionStatFix.Text = "Server connection.......";
            this.labelConnectionStatFix.Visible = false;
            // 
            // labelAuthStatFix
            // 
            this.labelAuthStatFix.AutoSize = true;
            this.labelAuthStatFix.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAuthStatFix.Location = new System.Drawing.Point(49, 509);
            this.labelAuthStatFix.Name = "labelAuthStatFix";
            this.labelAuthStatFix.Size = new System.Drawing.Size(173, 11);
            this.labelAuthStatFix.TabIndex = 14;
            this.labelAuthStatFix.Text = "Authentification........";
            this.labelAuthStatFix.Visible = false;
            // 
            // labelConnectionStat
            // 
            this.labelConnectionStat.AutoSize = true;
            this.labelConnectionStat.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelConnectionStat.Location = new System.Drawing.Point(228, 549);
            this.labelConnectionStat.Name = "labelConnectionStat";
            this.labelConnectionStat.Size = new System.Drawing.Size(0, 11);
            this.labelConnectionStat.TabIndex = 16;
            // 
            // labelAuthStat
            // 
            this.labelAuthStat.AutoSize = true;
            this.labelAuthStat.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAuthStat.Location = new System.Drawing.Point(228, 509);
            this.labelAuthStat.Name = "labelAuthStat";
            this.labelAuthStat.Size = new System.Drawing.Size(0, 11);
            this.labelAuthStat.TabIndex = 17;
            // 
            // labelSqlConnectionStat
            // 
            this.labelSqlConnectionStat.AutoSize = true;
            this.labelSqlConnectionStat.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSqlConnectionStat.Location = new System.Drawing.Point(228, 529);
            this.labelSqlConnectionStat.Name = "labelSqlConnectionStat";
            this.labelSqlConnectionStat.Size = new System.Drawing.Size(0, 11);
            this.labelSqlConnectionStat.TabIndex = 19;
            // 
            // labelSqlConnectionStatFix
            // 
            this.labelSqlConnectionStatFix.AutoSize = true;
            this.labelSqlConnectionStatFix.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSqlConnectionStatFix.Location = new System.Drawing.Point(49, 529);
            this.labelSqlConnectionStatFix.Name = "labelSqlConnectionStatFix";
            this.labelSqlConnectionStatFix.Size = new System.Drawing.Size(173, 11);
            this.labelSqlConnectionStatFix.TabIndex = 18;
            this.labelSqlConnectionStatFix.Text = "Database Connection.....";
            this.labelSqlConnectionStatFix.Visible = false;
            // 
            // buttonTest
            // 
            this.buttonTest.Location = new System.Drawing.Point(265, 598);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(27, 23);
            this.buttonTest.TabIndex = 20;
            this.buttonTest.Text = "?";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Visible = false;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            // 
            // pictureBoxLoadingGif
            // 
            this.pictureBoxLoadingGif.Image = global::TestTrack.Properties.Resources.loadingBW;
            this.pictureBoxLoadingGif.Location = new System.Drawing.Point(111, 394);
            this.pictureBoxLoadingGif.Name = "pictureBoxLoadingGif";
            this.pictureBoxLoadingGif.Size = new System.Drawing.Size(80, 80);
            this.pictureBoxLoadingGif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLoadingGif.TabIndex = 11;
            this.pictureBoxLoadingGif.TabStop = false;
            this.pictureBoxLoadingGif.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::TestTrack.Properties.Resources.LOGOnew2;
            this.pictureBox2.Location = new System.Drawing.Point(65, 52);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(174, 79);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // Form_Login
            // 
            this.AcceptButton = this.buttonTT;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(304, 655);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.labelSqlConnectionStat);
            this.Controls.Add(this.labelSqlConnectionStatFix);
            this.Controls.Add(this.labelAuthStat);
            this.Controls.Add(this.labelConnectionStat);
            this.Controls.Add(this.labelAuthStatFix);
            this.Controls.Add(this.labelConnectionStatFix);
            this.Controls.Add(this.labelVerStatFix);
            this.Controls.Add(this.pictureBoxLoadingGif);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.labelVerStat);
            this.Controls.Add(this.labelVerVar);
            this.Controls.Add(this.labelVerFix);
            this.Controls.Add(this.labelStatusLogin);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.labelLoginVar);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestTrack";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoadingGif)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPW;
        private System.Windows.Forms.Button buttonGuest;
        private System.Windows.Forms.Button buttonTE;
        private System.Windows.Forms.Button buttonTT;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelLoginVar;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label labelStatusLogin;
        private System.Windows.Forms.Label labelVerFix;
        private System.Windows.Forms.Label labelVerVar;
        private System.Windows.Forms.Label labelVerStat;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBoxLoadingGif;
        private System.Windows.Forms.Label labelVerStatFix;
        private System.Windows.Forms.Label labelConnectionStatFix;
        private System.Windows.Forms.Label labelAuthStatFix;
        private System.Windows.Forms.Label labelConnectionStat;
        private System.Windows.Forms.Label labelAuthStat;
        private System.Windows.Forms.Label labelSqlConnectionStat;
        private System.Windows.Forms.Label labelSqlConnectionStatFix;
        private System.Windows.Forms.Button buttonTest;
    }
}