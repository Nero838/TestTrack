namespace MC1
{
    partial class Form_Order
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Order));
            this.dataGridViewOrder = new System.Windows.Forms.DataGridView();
            this.textBoxOrder = new System.Windows.Forms.TextBox();
            this.buttonGetData = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExit = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelOrderMain = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panelModels = new System.Windows.Forms.Panel();
            this.dataGridViewModel = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dataGridViewPO = new System.Windows.Forms.DataGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonExitPO = new System.Windows.Forms.Button();
            this.textBoxPO = new System.Windows.Forms.TextBox();
            this.buttonGetDataPO = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dataGridViewSkuStat = new System.Windows.Forms.DataGridView();
            this.textBoxSkuStat = new System.Windows.Forms.TextBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonExitTestStat = new System.Windows.Forms.Button();
            this.buttonGetSkuStatus = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrder)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelOrderMain.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panelModels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModel)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPO)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSkuStat)).BeginInit();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewOrder
            // 
            this.dataGridViewOrder.AllowUserToAddRows = false;
            this.dataGridViewOrder.AllowUserToDeleteRows = false;
            this.dataGridViewOrder.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewOrder.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewOrder.Location = new System.Drawing.Point(10, 39);
            this.dataGridViewOrder.Name = "dataGridViewOrder";
            this.dataGridViewOrder.ReadOnly = true;
            this.dataGridViewOrder.Size = new System.Drawing.Size(750, 480);
            this.dataGridViewOrder.TabIndex = 0;
            // 
            // textBoxOrder
            // 
            this.textBoxOrder.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxOrder.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOrder.Location = new System.Drawing.Point(126, 7);
            this.textBoxOrder.MaxLength = 12;
            this.textBoxOrder.Name = "textBoxOrder";
            this.textBoxOrder.Size = new System.Drawing.Size(116, 25);
            this.textBoxOrder.TabIndex = 1;
            // 
            // buttonGetData
            // 
            this.buttonGetData.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonGetData.Location = new System.Drawing.Point(248, 7);
            this.buttonGetData.Name = "buttonGetData";
            this.buttonGetData.Size = new System.Drawing.Size(98, 25);
            this.buttonGetData.TabIndex = 2;
            this.buttonGetData.Text = "Get Data";
            this.buttonGetData.UseVisualStyleBackColor = true;
            this.buttonGetData.Click += new System.EventHandler(this.buttonGetData_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Enter SKU number:";
            // 
            // buttonExit
            // 
            this.buttonExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExit.Location = new System.Drawing.Point(652, 7);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(98, 25);
            this.buttonExit.TabIndex = 3;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.buttonExit);
            this.panel1.Controls.Add(this.textBoxOrder);
            this.panel1.Controls.Add(this.buttonGetData);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(750, 39);
            this.panel1.TabIndex = 5;
            // 
            // panelOrderMain
            // 
            this.panelOrderMain.Controls.Add(this.dataGridViewOrder);
            this.panelOrderMain.Controls.Add(this.panel1);
            this.panelOrderMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrderMain.Location = new System.Drawing.Point(3, 3);
            this.panelOrderMain.Name = "panelOrderMain";
            this.panelOrderMain.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.panelOrderMain.Size = new System.Drawing.Size(770, 529);
            this.panelOrderMain.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(984, 561);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.panelModels);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(976, 535);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Models List";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panelModels
            // 
            this.panelModels.Controls.Add(this.dataGridViewModel);
            this.panelModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelModels.Location = new System.Drawing.Point(0, 0);
            this.panelModels.Name = "panelModels";
            this.panelModels.Padding = new System.Windows.Forms.Padding(3);
            this.panelModels.Size = new System.Drawing.Size(976, 535);
            this.panelModels.TabIndex = 1;
            // 
            // dataGridViewModel
            // 
            this.dataGridViewModel.AllowUserToAddRows = false;
            this.dataGridViewModel.AllowUserToDeleteRows = false;
            this.dataGridViewModel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewModel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewModel.Location = new System.Drawing.Point(3, 3);
            this.dataGridViewModel.Name = "dataGridViewModel";
            this.dataGridViewModel.ReadOnly = true;
            this.dataGridViewModel.Size = new System.Drawing.Size(970, 529);
            this.dataGridViewModel.TabIndex = 0;
            this.dataGridViewModel.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewModel_CellContentClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panelOrderMain);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(776, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SKU List";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(776, 535);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "PO List";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dataGridViewPO);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.panel2.Size = new System.Drawing.Size(770, 529);
            this.panel2.TabIndex = 7;
            // 
            // dataGridViewPO
            // 
            this.dataGridViewPO.AllowUserToAddRows = false;
            this.dataGridViewPO.AllowUserToDeleteRows = false;
            this.dataGridViewPO.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPO.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewPO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewPO.Location = new System.Drawing.Point(10, 39);
            this.dataGridViewPO.Name = "dataGridViewPO";
            this.dataGridViewPO.ReadOnly = true;
            this.dataGridViewPO.Size = new System.Drawing.Size(750, 480);
            this.dataGridViewPO.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.buttonExitPO);
            this.panel3.Controls.Add(this.textBoxPO);
            this.panel3.Controls.Add(this.buttonGetDataPO);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(10, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(750, 39);
            this.panel3.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Enter PO number:";
            // 
            // buttonExitPO
            // 
            this.buttonExitPO.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExitPO.Location = new System.Drawing.Point(652, 7);
            this.buttonExitPO.Name = "buttonExitPO";
            this.buttonExitPO.Size = new System.Drawing.Size(98, 25);
            this.buttonExitPO.TabIndex = 3;
            this.buttonExitPO.Text = "Exit";
            this.buttonExitPO.UseVisualStyleBackColor = true;
            this.buttonExitPO.Click += new System.EventHandler(this.buttonExitPO_Click);
            // 
            // textBoxPO
            // 
            this.textBoxPO.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxPO.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPO.Location = new System.Drawing.Point(126, 7);
            this.textBoxPO.MaxLength = 12;
            this.textBoxPO.Name = "textBoxPO";
            this.textBoxPO.Size = new System.Drawing.Size(116, 25);
            this.textBoxPO.TabIndex = 1;
            // 
            // buttonGetDataPO
            // 
            this.buttonGetDataPO.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonGetDataPO.Location = new System.Drawing.Point(248, 7);
            this.buttonGetDataPO.Name = "buttonGetDataPO";
            this.buttonGetDataPO.Size = new System.Drawing.Size(98, 25);
            this.buttonGetDataPO.TabIndex = 2;
            this.buttonGetDataPO.Text = "Get Data";
            this.buttonGetDataPO.UseVisualStyleBackColor = true;
            this.buttonGetDataPO.Click += new System.EventHandler(this.buttonGetDataPO_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.panel4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(776, 535);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "TT hourly SKU report";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(10, 0, 10, 10);
            this.panel4.Size = new System.Drawing.Size(770, 529);
            this.panel4.TabIndex = 8;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.dataGridViewSkuStat);
            this.panel6.Controls.Add(this.textBoxSkuStat);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(10, 39);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(750, 480);
            this.panel6.TabIndex = 6;
            // 
            // dataGridViewSkuStat
            // 
            this.dataGridViewSkuStat.AllowUserToAddRows = false;
            this.dataGridViewSkuStat.AllowUserToDeleteRows = false;
            this.dataGridViewSkuStat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewSkuStat.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            this.dataGridViewSkuStat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSkuStat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewSkuStat.Location = new System.Drawing.Point(298, 0);
            this.dataGridViewSkuStat.Name = "dataGridViewSkuStat";
            this.dataGridViewSkuStat.ReadOnly = true;
            this.dataGridViewSkuStat.Size = new System.Drawing.Size(452, 480);
            this.dataGridViewSkuStat.TabIndex = 0;
            // 
            // textBoxSkuStat
            // 
            this.textBoxSkuStat.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.textBoxSkuStat.Dock = System.Windows.Forms.DockStyle.Left;
            this.textBoxSkuStat.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSkuStat.Location = new System.Drawing.Point(0, 0);
            this.textBoxSkuStat.MaxLength = 2000;
            this.textBoxSkuStat.Multiline = true;
            this.textBoxSkuStat.Name = "textBoxSkuStat";
            this.textBoxSkuStat.Size = new System.Drawing.Size(298, 480);
            this.textBoxSkuStat.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.buttonExitTestStat);
            this.panel5.Controls.Add(this.buttonGetSkuStatus);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(10, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(750, 39);
            this.panel5.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Enter order SKUs:";
            // 
            // buttonExitTestStat
            // 
            this.buttonExitTestStat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonExitTestStat.Location = new System.Drawing.Point(652, 7);
            this.buttonExitTestStat.Name = "buttonExitTestStat";
            this.buttonExitTestStat.Size = new System.Drawing.Size(98, 25);
            this.buttonExitTestStat.TabIndex = 3;
            this.buttonExitTestStat.Text = "Exit";
            this.buttonExitTestStat.UseVisualStyleBackColor = true;
            this.buttonExitTestStat.Click += new System.EventHandler(this.buttonExitTestStat_Click);
            // 
            // buttonGetSkuStatus
            // 
            this.buttonGetSkuStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.buttonGetSkuStatus.Location = new System.Drawing.Point(248, 7);
            this.buttonGetSkuStatus.Name = "buttonGetSkuStatus";
            this.buttonGetSkuStatus.Size = new System.Drawing.Size(98, 25);
            this.buttonGetSkuStatus.TabIndex = 2;
            this.buttonGetSkuStatus.Text = "Get Data";
            this.buttonGetSkuStatus.UseVisualStyleBackColor = true;
            this.buttonGetSkuStatus.Click += new System.EventHandler(this.buttonGetSkuStatus_Click);
            // 
            // Form_Order
            // 
            this.AcceptButton = this.buttonGetData;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.buttonExit;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "Form_Order";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Order List";
            this.Load += new System.EventHandler(this.Form_Order_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrder)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelOrderMain.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.panelModels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewModel)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPO)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSkuStat)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewOrder;
        private System.Windows.Forms.TextBox textBoxOrder;
        private System.Windows.Forms.Button buttonGetData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelOrderMain;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dataGridViewPO;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonExitPO;
        private System.Windows.Forms.TextBox textBoxPO;
        private System.Windows.Forms.Button buttonGetDataPO;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataGridView dataGridViewSkuStat;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonExitTestStat;
        private System.Windows.Forms.Button buttonGetSkuStatus;
        private System.Windows.Forms.TextBox textBoxSkuStat;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView dataGridViewModel;
        private System.Windows.Forms.Panel panelModels;
    }
}