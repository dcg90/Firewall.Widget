namespace FirewallWidget.Presentation.ChildForms.CreateRule
{
    partial class CreateIpForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tboxIpIntervalTo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tboxIpIntervalFrom = new System.Windows.Forms.TextBox();
            this.rbtnIpInterval = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tboxIpOrSubnet = new System.Windows.Forms.TextBox();
            this.rbtnIpOrSubnet = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tboxIpIntervalTo);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tboxIpIntervalFrom);
            this.panel1.Controls.Add(this.rbtnIpInterval);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tboxIpOrSubnet);
            this.panel1.Controls.Add(this.rbtnIpOrSubnet);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3);
            this.panel1.Size = new System.Drawing.Size(363, 200);
            this.panel1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(280, 171);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(199, 171);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "To";
            // 
            // tboxIpIntervalTo
            // 
            this.tboxIpIntervalTo.Enabled = false;
            this.tboxIpIntervalTo.Location = new System.Drawing.Point(65, 135);
            this.tboxIpIntervalTo.Name = "tboxIpIntervalTo";
            this.tboxIpIntervalTo.Size = new System.Drawing.Size(290, 22);
            this.tboxIpIntervalTo.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "From";
            // 
            // tboxIpIntervalFrom
            // 
            this.tboxIpIntervalFrom.Enabled = false;
            this.tboxIpIntervalFrom.Location = new System.Drawing.Point(65, 107);
            this.tboxIpIntervalFrom.Name = "tboxIpIntervalFrom";
            this.tboxIpIntervalFrom.Size = new System.Drawing.Size(290, 22);
            this.tboxIpIntervalFrom.TabIndex = 4;
            // 
            // rbtnIpInterval
            // 
            this.rbtnIpInterval.AutoSize = true;
            this.rbtnIpInterval.Location = new System.Drawing.Point(6, 84);
            this.rbtnIpInterval.Name = "rbtnIpInterval";
            this.rbtnIpInterval.Size = new System.Drawing.Size(102, 18);
            this.rbtnIpInterval.TabIndex = 3;
            this.rbtnIpInterval.Text = "Ip interval";
            this.rbtnIpInterval.UseVisualStyleBackColor = true;
            this.rbtnIpInterval.CheckedChanged += new System.EventHandler(this.RBtn_ToggleEnable);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(151, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Examples: 192.168.1.10\r\n          192.168.1.0/24";
            // 
            // tboxIpOrSubnet
            // 
            this.tboxIpOrSubnet.Location = new System.Drawing.Point(25, 24);
            this.tboxIpOrSubnet.Name = "tboxIpOrSubnet";
            this.tboxIpOrSubnet.Size = new System.Drawing.Size(330, 22);
            this.tboxIpOrSubnet.TabIndex = 1;
            // 
            // rbtnIpOrSubnet
            // 
            this.rbtnIpOrSubnet.AutoSize = true;
            this.rbtnIpOrSubnet.Checked = true;
            this.rbtnIpOrSubnet.Location = new System.Drawing.Point(6, 6);
            this.rbtnIpOrSubnet.Name = "rbtnIpOrSubnet";
            this.rbtnIpOrSubnet.Size = new System.Drawing.Size(109, 18);
            this.rbtnIpOrSubnet.TabIndex = 0;
            this.rbtnIpOrSubnet.TabStop = true;
            this.rbtnIpOrSubnet.Text = "Ip or subnet";
            this.rbtnIpOrSubnet.UseVisualStyleBackColor = true;
            this.rbtnIpOrSubnet.CheckedChanged += new System.EventHandler(this.RBtn_ToggleEnable);
            // 
            // CreateIp
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(363, 200);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreateIp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateIp";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tboxIpIntervalTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tboxIpIntervalFrom;
        private System.Windows.Forms.RadioButton rbtnIpInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tboxIpOrSubnet;
        private System.Windows.Forms.RadioButton rbtnIpOrSubnet;
    }
}