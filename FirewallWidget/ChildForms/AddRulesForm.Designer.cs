namespace FirewallWidget.ChildForms
{
    partial class AddRulesForm
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
            this.cboxProfiles = new System.Windows.Forms.ComboBox();
            this.lboxRules = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cboxDirections = new System.Windows.Forms.ComboBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.infoToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.addRuleMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.addRuleMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboxProfiles
            // 
            this.cboxProfiles.FormattingEnabled = true;
            this.cboxProfiles.Location = new System.Drawing.Point(62, 2);
            this.cboxProfiles.Name = "cboxProfiles";
            this.cboxProfiles.Size = new System.Drawing.Size(170, 22);
            this.cboxProfiles.TabIndex = 0;
            this.cboxProfiles.SelectedValueChanged += new System.EventHandler(this.Cbox_SelectedIndexChanged);
            // 
            // lboxRules
            // 
            this.lboxRules.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lboxRules.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lboxRules.FormattingEnabled = true;
            this.lboxRules.HorizontalScrollbar = true;
            this.lboxRules.ItemHeight = 15;
            this.lboxRules.Location = new System.Drawing.Point(3, 29);
            this.lboxRules.Name = "lboxRules";
            this.lboxRules.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lboxRules.Size = new System.Drawing.Size(487, 347);
            this.lboxRules.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnMenu);
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(2);
            this.panel2.Size = new System.Drawing.Size(495, 28);
            this.panel2.TabIndex = 3;
            // 
            // btnMenu
            // 
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnMenu.Image = global::FirewallWidget.Presentation.Properties.Resources.down_arrow;
            this.btnMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMenu.Location = new System.Drawing.Point(2, 2);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(65, 22);
            this.btnMenu.TabIndex = 3;
            this.btnMenu.Text = "Menu";
            this.btnMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.BtnMenu_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Brown;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(461, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(30, 22);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboxDirections);
            this.panel1.Controls.Add(this.btnOk);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lboxRules);
            this.panel1.Controls.Add(this.cboxProfiles);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(495, 405);
            this.panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 5;
            this.label2.Text = "Direction";
            // 
            // cboxDirections
            // 
            this.cboxDirections.FormattingEnabled = true;
            this.cboxDirections.Location = new System.Drawing.Point(314, 2);
            this.cboxDirections.Name = "cboxDirections";
            this.cboxDirections.Size = new System.Drawing.Size(176, 22);
            this.cboxDirections.TabIndex = 4;
            this.cboxDirections.SelectedValueChanged += new System.EventHandler(this.Cbox_SelectedIndexChanged);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.Location = new System.Drawing.Point(326, 378);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(84, 24);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Add";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Profile";
            // 
            // infoToolTip
            // 
            this.infoToolTip.AutoPopDelay = 10000;
            this.infoToolTip.InitialDelay = 500;
            this.infoToolTip.ReshowDelay = 100;
            // 
            // addRuleMenu
            // 
            this.addRuleMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshListToolStripMenuItem,
            this.createRuleToolStripMenuItem});
            this.addRuleMenu.Name = "contextMenuStrip1";
            this.addRuleMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.addRuleMenu.Size = new System.Drawing.Size(181, 70);
            // 
            // refreshListToolStripMenuItem
            // 
            this.refreshListToolStripMenuItem.Name = "refreshListToolStripMenuItem";
            this.refreshListToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.refreshListToolStripMenuItem.Text = "&Refresh List";
            this.refreshListToolStripMenuItem.Click += new System.EventHandler(this.RefreshListToolStripMenuItem_Click);
            // 
            // createRuleToolStripMenuItem
            // 
            this.createRuleToolStripMenuItem.Name = "createRuleToolStripMenuItem";
            this.createRuleToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.createRuleToolStripMenuItem.Text = "&Create Rule";
            this.createRuleToolStripMenuItem.Click += new System.EventHandler(this.CreateRuleToolStripMenuItem_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(415, 379);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // AddRulesForm
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(495, 433);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddRulesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "AddRulesForm";
            this.TopMost = true;
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.addRuleMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboxProfiles;
        private System.Windows.Forms.ListBox lboxRules;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboxDirections;
        private System.Windows.Forms.ToolTip infoToolTip;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.ContextMenuStrip addRuleMenu;
        private System.Windows.Forms.ToolStripMenuItem refreshListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createRuleToolStripMenuItem;
        private System.Windows.Forms.Button btnCancel;
    }
}