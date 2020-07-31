namespace FirewallWidget.Presentation.ChildForms.CreateRule
{
    partial class CreateFirewallRuleForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tboxPorts = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboxProtocol = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tboxRuleName = new System.Windows.Forms.TextBox();
            this.btnRemoveIps = new System.Windows.Forms.Button();
            this.btnAddIp = new System.Windows.Forms.Button();
            this.lboxIpAddresses = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboxProcessNames = new System.Windows.Forms.ComboBox();
            this.btnRefreshProcesses = new System.Windows.Forms.Button();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboxLoadFromProcess = new System.Windows.Forms.CheckBox();
            this.tboxProgramPath = new System.Windows.Forms.TextBox();
            this.cboxCreateEnabled = new System.Windows.Forms.CheckBox();
            this.cboxDirection = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboxProfile = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnCreateRule = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(489, 420);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 33);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(487, 358);
            this.panel2.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tboxPorts);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cboxProtocol);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tboxRuleName);
            this.groupBox1.Controls.Add(this.btnRemoveIps);
            this.groupBox1.Controls.Add(this.btnAddIp);
            this.groupBox1.Controls.Add(this.lboxIpAddresses);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cboxProcessNames);
            this.groupBox1.Controls.Add(this.btnRefreshProcesses);
            this.groupBox1.Controls.Add(this.btnSelectPath);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cboxLoadFromProcess);
            this.groupBox1.Controls.Add(this.tboxProgramPath);
            this.groupBox1.Controls.Add(this.cboxCreateEnabled);
            this.groupBox1.Controls.Add(this.cboxDirection);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboxProfile);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(477, 348);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rule Details";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(281, 324);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(157, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Example: 80,443,5000-5010";
            // 
            // tboxPorts
            // 
            this.tboxPorts.Enabled = false;
            this.tboxPorts.Location = new System.Drawing.Point(284, 299);
            this.tboxPorts.Name = "tboxPorts";
            this.tboxPorts.Size = new System.Drawing.Size(184, 22);
            this.tboxPorts.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(235, 302);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 14);
            this.label9.TabIndex = 24;
            this.label9.Text = "Ports";
            // 
            // cboxProtocol
            // 
            this.cboxProtocol.FormattingEnabled = true;
            this.cboxProtocol.Location = new System.Drawing.Point(79, 299);
            this.cboxProtocol.Name = "cboxProtocol";
            this.cboxProtocol.Size = new System.Drawing.Size(150, 22);
            this.cboxProtocol.TabIndex = 10;
            this.cboxProtocol.SelectedIndexChanged += new System.EventHandler(this.CboxProtocol_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 302);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 14);
            this.label7.TabIndex = 21;
            this.label7.Text = "Protocol";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 19;
            this.label6.Text = "Rule name";
            // 
            // tboxRuleName
            // 
            this.tboxRuleName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tboxRuleName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tboxRuleName.Location = new System.Drawing.Point(13, 92);
            this.tboxRuleName.Name = "tboxRuleName";
            this.tboxRuleName.Size = new System.Drawing.Size(455, 22);
            this.tboxRuleName.TabIndex = 1;
            // 
            // btnRemoveIps
            // 
            this.btnRemoveIps.Location = new System.Drawing.Point(432, 227);
            this.btnRemoveIps.Name = "btnRemoveIps";
            this.btnRemoveIps.Size = new System.Drawing.Size(36, 24);
            this.btnRemoveIps.TabIndex = 9;
            this.btnRemoveIps.Text = "-";
            this.btnRemoveIps.UseVisualStyleBackColor = true;
            this.btnRemoveIps.Click += new System.EventHandler(this.BtnRemoveIps_Click);
            // 
            // btnAddIp
            // 
            this.btnAddIp.Location = new System.Drawing.Point(432, 204);
            this.btnAddIp.Name = "btnAddIp";
            this.btnAddIp.Size = new System.Drawing.Size(36, 24);
            this.btnAddIp.TabIndex = 8;
            this.btnAddIp.Text = "+";
            this.btnAddIp.UseVisualStyleBackColor = true;
            this.btnAddIp.Click += new System.EventHandler(this.BtnAddIp_Click);
            // 
            // lboxIpAddresses
            // 
            this.lboxIpAddresses.FormattingEnabled = true;
            this.lboxIpAddresses.ItemHeight = 14;
            this.lboxIpAddresses.Location = new System.Drawing.Point(13, 205);
            this.lboxIpAddresses.Name = "lboxIpAddresses";
            this.lboxIpAddresses.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lboxIpAddresses.Size = new System.Drawing.Size(419, 88);
            this.lboxIpAddresses.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 14);
            this.label5.TabIndex = 14;
            this.label5.Text = "IP Addresses";
            // 
            // cboxProcessNames
            // 
            this.cboxProcessNames.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboxProcessNames.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cboxProcessNames.Enabled = false;
            this.cboxProcessNames.FormattingEnabled = true;
            this.cboxProcessNames.Location = new System.Drawing.Point(118, 158);
            this.cboxProcessNames.Name = "cboxProcessNames";
            this.cboxProcessNames.Size = new System.Drawing.Size(314, 22);
            this.cboxProcessNames.TabIndex = 5;
            this.cboxProcessNames.SelectedIndexChanged += new System.EventHandler(this.CboxProcessNames_SelectedIndexChanged);
            this.cboxProcessNames.TextChanged += new System.EventHandler(this.CboxProcessNames_TextChanged);
            // 
            // btnRefreshProcesses
            // 
            this.btnRefreshProcesses.Enabled = false;
            this.btnRefreshProcesses.Image = global::FirewallWidget.Presentation.Properties.Resources.refresh;
            this.btnRefreshProcesses.Location = new System.Drawing.Point(432, 157);
            this.btnRefreshProcesses.Name = "btnRefreshProcesses";
            this.btnRefreshProcesses.Size = new System.Drawing.Size(36, 24);
            this.btnRefreshProcesses.TabIndex = 6;
            this.btnRefreshProcesses.UseVisualStyleBackColor = true;
            this.btnRefreshProcesses.Click += new System.EventHandler(this.BtnRefreshProcesses_Click);
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(432, 134);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(36, 24);
            this.btnSelectPath.TabIndex = 3;
            this.btnSelectPath.Text = "...";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 14);
            this.label4.TabIndex = 10;
            this.label4.Text = "Program path";
            // 
            // cboxLoadFromProcess
            // 
            this.cboxLoadFromProcess.AutoSize = true;
            this.cboxLoadFromProcess.Location = new System.Drawing.Point(13, 161);
            this.cboxLoadFromProcess.Name = "cboxLoadFromProcess";
            this.cboxLoadFromProcess.Size = new System.Drawing.Size(110, 18);
            this.cboxLoadFromProcess.TabIndex = 4;
            this.cboxLoadFromProcess.Text = "From process";
            this.cboxLoadFromProcess.UseVisualStyleBackColor = true;
            this.cboxLoadFromProcess.CheckedChanged += new System.EventHandler(this.CboxLoadFromProcess_CheckedChanged);
            // 
            // tboxProgramPath
            // 
            this.tboxProgramPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tboxProgramPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.tboxProgramPath.Location = new System.Drawing.Point(13, 135);
            this.tboxProgramPath.Name = "tboxProgramPath";
            this.tboxProgramPath.Size = new System.Drawing.Size(419, 22);
            this.tboxProgramPath.TabIndex = 2;
            // 
            // cboxCreateEnabled
            // 
            this.cboxCreateEnabled.AutoSize = true;
            this.cboxCreateEnabled.Checked = true;
            this.cboxCreateEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxCreateEnabled.Location = new System.Drawing.Point(13, 48);
            this.cboxCreateEnabled.Name = "cboxCreateEnabled";
            this.cboxCreateEnabled.Size = new System.Drawing.Size(159, 18);
            this.cboxCreateEnabled.TabIndex = 14;
            this.cboxCreateEnabled.Text = "Create rule enabled";
            this.cboxCreateEnabled.UseVisualStyleBackColor = true;
            // 
            // cboxDirection
            // 
            this.cboxDirection.FormattingEnabled = true;
            this.cboxDirection.Location = new System.Drawing.Point(311, 20);
            this.cboxDirection.Name = "cboxDirection";
            this.cboxDirection.Size = new System.Drawing.Size(157, 22);
            this.cboxDirection.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(235, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 2;
            this.label3.Text = "Direction";
            // 
            // cboxProfile
            // 
            this.cboxProfile.FormattingEnabled = true;
            this.cboxProfile.Location = new System.Drawing.Point(72, 20);
            this.cboxProfile.Name = "cboxProfile";
            this.cboxProfile.Size = new System.Drawing.Size(157, 22);
            this.cboxProfile.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 0;
            this.label2.Text = "Profile";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnCreateRule);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 391);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(0, 0, 3, 3);
            this.panel3.Size = new System.Drawing.Size(487, 27);
            this.panel3.TabIndex = 2;
            // 
            // btnCreateRule
            // 
            this.btnCreateRule.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnCreateRule.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCreateRule.Location = new System.Drawing.Point(320, 0);
            this.btnCreateRule.Name = "btnCreateRule";
            this.btnCreateRule.Size = new System.Drawing.Size(75, 24);
            this.btnCreateRule.TabIndex = 0;
            this.btnCreateRule.Text = "Create";
            this.btnCreateRule.UseVisualStyleBackColor = true;
            this.btnCreateRule.Click += new System.EventHandler(this.BtnCreateRule_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Location = new System.Drawing.Point(395, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(89, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(487, 33);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create Firewall Rule";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CreateFirewallRuleForm
            // 
            this.AcceptButton = this.btnCreateRule;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 420);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CreateFirewallRuleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CreateFirewallRuleForm";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cboxCreateEnabled;
        private System.Windows.Forms.ComboBox cboxDirection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboxProfile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cboxLoadFromProcess;
        private System.Windows.Forms.TextBox tboxProgramPath;
        private System.Windows.Forms.Button btnRefreshProcesses;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboxProcessNames;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnRemoveIps;
        private System.Windows.Forms.Button btnAddIp;
        private System.Windows.Forms.ListBox lboxIpAddresses;
        private System.Windows.Forms.Button btnCreateRule;
        private System.Windows.Forms.ComboBox cboxProtocol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tboxRuleName;
        private System.Windows.Forms.TextBox tboxPorts;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
    }
}