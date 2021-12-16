namespace MC1
{
    partial class Form_Report
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Report));
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonLeave2 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.buttonSend2 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonSend1 = new System.Windows.Forms.Button();
            this.buttonLeave1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(3, 3);
            this.textBox2.MaxLength = 490;
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(473, 313);
            this.textBox2.TabIndex = 1;
            // 
            // buttonLeave2
            // 
            this.buttonLeave2.Location = new System.Drawing.Point(400, 323);
            this.buttonLeave2.Name = "buttonLeave2";
            this.buttonLeave2.Size = new System.Drawing.Size(75, 23);
            this.buttonLeave2.TabIndex = 3;
            this.buttonLeave2.Text = "Leave";
            this.buttonLeave2.UseVisualStyleBackColor = true;
            this.buttonLeave2.Click += new System.EventHandler(this.buttonLeave2_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonSend2);
            this.tabPage2.Controls.Add(this.buttonLeave2);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(481, 351);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Suggestion/Idea";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // buttonSend2
            // 
            this.buttonSend2.Location = new System.Drawing.Point(319, 323);
            this.buttonSend2.Name = "buttonSend2";
            this.buttonSend2.Size = new System.Drawing.Size(75, 23);
            this.buttonSend2.TabIndex = 4;
            this.buttonSend2.Text = "Send";
            this.buttonSend2.UseVisualStyleBackColor = true;
            this.buttonSend2.Click += new System.EventHandler(this.buttonSend2_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonSend1);
            this.tabPage1.Controls.Add(this.buttonLeave1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(481, 351);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Bug/Error";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonSend1
            // 
            this.buttonSend1.Location = new System.Drawing.Point(319, 323);
            this.buttonSend1.Name = "buttonSend1";
            this.buttonSend1.Size = new System.Drawing.Size(75, 23);
            this.buttonSend1.TabIndex = 7;
            this.buttonSend1.Text = "Send";
            this.buttonSend1.UseVisualStyleBackColor = true;
            this.buttonSend1.Click += new System.EventHandler(this.buttonSend1_Click);
            // 
            // buttonLeave1
            // 
            this.buttonLeave1.Location = new System.Drawing.Point(400, 323);
            this.buttonLeave1.Name = "buttonLeave1";
            this.buttonLeave1.Size = new System.Drawing.Size(75, 23);
            this.buttonLeave1.TabIndex = 6;
            this.buttonLeave1.Text = "Leave";
            this.buttonLeave1.UseVisualStyleBackColor = true;
            this.buttonLeave1.Click += new System.EventHandler(this.buttonLeave1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 3);
            this.textBox1.MaxLength = 490;
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(473, 313);
            this.textBox1.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(1, 29);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(489, 377);
            this.tabControl1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "App report feedback:";
            // 
            // Form_Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(488, 405);
            this.ControlBox = false;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TestTrack Report";
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonLeave2;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonSend2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button buttonSend1;
        private System.Windows.Forms.Button buttonLeave1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label1;
    }
}