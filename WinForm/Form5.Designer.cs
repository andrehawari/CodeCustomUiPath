namespace AHMFARPA018
{
    partial class Form5
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
            this.components = new System.ComponentModel.Container();
            this.button99 = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.txtPeriod = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.lblStatus_Description = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chkTimer = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.treeView1 = new AHMFARPA018.NoClickTree();
            this.BtnReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button99
            // 
            this.button99.Location = new System.Drawing.Point(485, 663);
            this.button99.Margin = new System.Windows.Forms.Padding(4);
            this.button99.Name = "button99";
            this.button99.Size = new System.Drawing.Size(100, 28);
            this.button99.TabIndex = 101;
            this.button99.Text = "E&xit";
            this.button99.UseVisualStyleBackColor = true;
            this.button99.Click += new System.EventHandler(this.button99_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(17, 55);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(127, 28);
            this.btnRefresh.TabIndex = 113;
            this.btnRefresh.Text = "R&efresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtPeriod
            // 
            this.txtPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPeriod.Location = new System.Drawing.Point(103, 6);
            this.txtPeriod.Margin = new System.Windows.Forms.Padding(4);
            this.txtPeriod.Name = "txtPeriod";
            this.txtPeriod.Size = new System.Drawing.Size(132, 26);
            this.txtPeriod.TabIndex = 111;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 20);
            this.label1.TabIndex = 110;
            this.label1.Text = "Period";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(350, 662);
            this.btnOk.Margin = new System.Windows.Forms.Padding(4);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(127, 28);
            this.btnOk.TabIndex = 114;
            this.btnOk.Text = "Execute";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(443, 64);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(152, 21);
            this.checkBox1.TabIndex = 115;
            this.checkBox1.Text = "Check / Uncheck All";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // lblStatus_Description
            // 
            this.lblStatus_Description.AutoSize = true;
            this.lblStatus_Description.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus_Description.Location = new System.Drawing.Point(349, 638);
            this.lblStatus_Description.Name = "lblStatus_Description";
            this.lblStatus_Description.Size = new System.Drawing.Size(54, 16);
            this.lblStatus_Description.TabIndex = 118;
            this.lblStatus_Description.Text = "Status : ";
            this.lblStatus_Description.Click += new System.EventHandler(this.lblStatus_Description_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(402, 638);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 16);
            this.lblStatus.TabIndex = 119;
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // chkTimer
            // 
            this.chkTimer.AutoSize = true;
            this.chkTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkTimer.Location = new System.Drawing.Point(443, 37);
            this.chkTimer.Name = "chkTimer";
            this.chkTimer.Size = new System.Drawing.Size(110, 21);
            this.chkTimer.TabIndex = 120;
            this.chkTimer.Text = "Auto Refresh";
            this.chkTimer.UseVisualStyleBackColor = true;
            this.chkTimer.CheckedChanged += new System.EventHandler(this.chkTimer_CheckedChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(77, 662);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 28);
            this.button3.TabIndex = 121;
            this.button3.Text = "View &Log";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // treeView1
            // 
            this.treeView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(17, 91);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(571, 544);
            this.treeView1.TabIndex = 0;
            this.treeView1.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeView1_BeforeCheck);
            this.treeView1.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterCheck);
            // 
            // BtnReset
            // 
            this.BtnReset.Location = new System.Drawing.Point(215, 662);
            this.BtnReset.Margin = new System.Windows.Forms.Padding(4);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(127, 28);
            this.BtnReset.TabIndex = 122;
            this.BtnReset.Text = "Reset DB Job";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 669);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 16);
            this.label2.TabIndex = 123;
            this.label2.Text = "v1.0.0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 638);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 124;
            this.label3.Text = "AHMFARPA018";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(242, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 16);
            this.label4.TabIndex = 125;
            this.label4.Text = "YYYYMM";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Form5
            // 
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(623, 698);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnReset);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.chkTimer);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblStatus_Description);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.txtPeriod);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button99);
            this.Controls.Add(this.treeView1);
            this.Name = "Form5";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Inventory Closing Robot";
            this.Load += new System.EventHandler(this.Form5_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button99;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TextBox txtPeriod;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lblStatus_Description;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox chkTimer;
        private NoClickTree treeView1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
