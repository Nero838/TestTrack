
namespace MC1
{
    partial class Form_BsnGenerator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_BsnGenerator));
            this.textBoxUnitSN = new System.Windows.Forms.TextBox();
            this.textBoxMbPn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMbSn = new System.Windows.Forms.TextBox();
            this.buttonSetMbPn = new System.Windows.Forms.Button();
            this.buttonSetUnitSn = new System.Windows.Forms.Button();
            this.buttonSetMbSn = new System.Windows.Forms.Button();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxUnitSN
            // 
            this.textBoxUnitSN.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxUnitSN.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxUnitSN.ForeColor = System.Drawing.Color.Black;
            this.textBoxUnitSN.Location = new System.Drawing.Point(30, 26);
            this.textBoxUnitSN.MaxLength = 10;
            this.textBoxUnitSN.Name = "textBoxUnitSN";
            this.textBoxUnitSN.Size = new System.Drawing.Size(119, 25);
            this.textBoxUnitSN.TabIndex = 0;
            this.textBoxUnitSN.Click += new System.EventHandler(this.textBoxUnitSN_Click);
            // 
            // textBoxMbPn
            // 
            this.textBoxMbPn.Enabled = false;
            this.textBoxMbPn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMbPn.ForeColor = System.Drawing.Color.Black;
            this.textBoxMbPn.Location = new System.Drawing.Point(30, 74);
            this.textBoxMbPn.MaxLength = 20;
            this.textBoxMbPn.Name = "textBoxMbPn";
            this.textBoxMbPn.Size = new System.Drawing.Size(119, 25);
            this.textBoxMbPn.TabIndex = 1;
            this.textBoxMbPn.Click += new System.EventHandler(this.textBoxMbPn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Unit SN:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Board PN:";
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(144, 224);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(69, 24);
            this.buttonReset.TabIndex = 4;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "New Board SN:";
            // 
            // textBoxMbSn
            // 
            this.textBoxMbSn.Enabled = false;
            this.textBoxMbSn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxMbSn.ForeColor = System.Drawing.Color.Black;
            this.textBoxMbSn.Location = new System.Drawing.Point(30, 124);
            this.textBoxMbSn.MaxLength = 8;
            this.textBoxMbSn.Name = "textBoxMbSn";
            this.textBoxMbSn.Size = new System.Drawing.Size(119, 25);
            this.textBoxMbSn.TabIndex = 5;
            this.textBoxMbSn.Click += new System.EventHandler(this.textBoxMbSn_Click);
            // 
            // buttonSetMbPn
            // 
            this.buttonSetMbPn.Enabled = false;
            this.buttonSetMbPn.Location = new System.Drawing.Point(155, 74);
            this.buttonSetMbPn.Name = "buttonSetMbPn";
            this.buttonSetMbPn.Size = new System.Drawing.Size(36, 25);
            this.buttonSetMbPn.TabIndex = 7;
            this.buttonSetMbPn.Text = "Set";
            this.buttonSetMbPn.UseVisualStyleBackColor = true;
            this.buttonSetMbPn.Click += new System.EventHandler(this.buttonSetMbPn_Click);
            // 
            // buttonSetUnitSn
            // 
            this.buttonSetUnitSn.Location = new System.Drawing.Point(155, 26);
            this.buttonSetUnitSn.Name = "buttonSetUnitSn";
            this.buttonSetUnitSn.Size = new System.Drawing.Size(36, 25);
            this.buttonSetUnitSn.TabIndex = 8;
            this.buttonSetUnitSn.Text = "Set";
            this.buttonSetUnitSn.UseVisualStyleBackColor = true;
            this.buttonSetUnitSn.Click += new System.EventHandler(this.buttonSetUnitSn_Click);
            // 
            // buttonSetMbSn
            // 
            this.buttonSetMbSn.Enabled = false;
            this.buttonSetMbSn.Location = new System.Drawing.Point(155, 124);
            this.buttonSetMbSn.Name = "buttonSetMbSn";
            this.buttonSetMbSn.Size = new System.Drawing.Size(36, 25);
            this.buttonSetMbSn.TabIndex = 9;
            this.buttonSetMbSn.Text = "Set";
            this.buttonSetMbSn.UseVisualStyleBackColor = true;
            this.buttonSetMbSn.Click += new System.EventHandler(this.buttonSetMbSn_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Enabled = false;
            this.buttonGenerate.Location = new System.Drawing.Point(52, 224);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(69, 24);
            this.buttonGenerate.TabIndex = 10;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 197);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.textBoxUnitSN);
            this.panel1.Controls.Add(this.textBoxMbPn);
            this.panel1.Controls.Add(this.buttonSetMbSn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonSetUnitSn);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.buttonSetMbPn);
            this.panel1.Controls.Add(this.textBoxMbSn);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(235, 176);
            this.panel1.TabIndex = 10;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelStatus);
            this.groupBox2.Location = new System.Drawing.Point(12, 265);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(241, 113);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // labelStatus
            // 
            this.labelStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStatus.Location = new System.Drawing.Point(3, 18);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(235, 92);
            this.labelStatus.TabIndex = 0;
            this.labelStatus.Text = "Enter unit serial number.";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form_BsnGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(267, 394);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(283, 433);
            this.MinimumSize = new System.Drawing.Size(283, 433);
            this.Name = "Form_BsnGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrderDataBSN Generator";
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxUnitSN;
        private System.Windows.Forms.TextBox textBoxMbPn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMbSn;
        private System.Windows.Forms.Button buttonSetMbPn;
        private System.Windows.Forms.Button buttonSetUnitSn;
        private System.Windows.Forms.Button buttonSetMbSn;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Panel panel1;
    }
}