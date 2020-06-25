namespace FirewallWidget.Presentation
{
    partial class MainForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnOptions = new System.Windows.Forms.Button();
            this.optionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addRulesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.publicAllowOutboundToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlRules = new System.Windows.Forms.Panel();
            this.pnlScrollDown = new System.Windows.Forms.Panel();
            this.pnlScrollUp = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.optionsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.btnOptions);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 8);
            this.panel1.TabIndex = 1;
            this.panel1.MouseEnter += new System.EventHandler(this.ShowForm);
            this.panel1.MouseLeave += new System.EventHandler(this.HideForm);
            // 
            // btnOptions
            // 
            this.btnOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOptions.Location = new System.Drawing.Point(0, 0);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(80, 8);
            this.btnOptions.TabIndex = 1;
            this.btnOptions.Text = "...";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.BtnOptions_Click);
            this.btnOptions.MouseEnter += new System.EventHandler(this.ShowForm);
            this.btnOptions.MouseLeave += new System.EventHandler(this.HideForm);
            // 
            // optionsMenu
            // 
            this.optionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRulesToolStripMenuItem,
            this.publicAllowOutboundToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exitToolStripMenuItem});
            this.optionsMenu.Name = "optionsMenu";
            this.optionsMenu.Size = new System.Drawing.Size(301, 98);
            this.optionsMenu.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.OptionsMenu_Closed);
            // 
            // addRulesToolStripMenuItem
            // 
            this.addRulesToolStripMenuItem.Name = "addRulesToolStripMenuItem";
            this.addRulesToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.addRulesToolStripMenuItem.Text = "&Add rules";
            this.addRulesToolStripMenuItem.Click += new System.EventHandler(this.AddRulesToolStripMenuItem_Click);
            // 
            // publicAllowOutboundToolStripMenuItem
            // 
            this.publicAllowOutboundToolStripMenuItem.Checked = true;
            this.publicAllowOutboundToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.publicAllowOutboundToolStripMenuItem.Name = "publicAllowOutboundToolStripMenuItem";
            this.publicAllowOutboundToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.publicAllowOutboundToolStripMenuItem.Text = "Outbound connections allowed on PUBLIC";
            this.publicAllowOutboundToolStripMenuItem.Click += new System.EventHandler(this.PublicAllowOutboundToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.optionsToolStripMenuItem.Text = "&Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(297, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(300, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // pnlRules
            // 
            this.pnlRules.AllowDrop = true;
            this.pnlRules.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlRules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRules.Location = new System.Drawing.Point(0, 17);
            this.pnlRules.Name = "pnlRules";
            this.pnlRules.Size = new System.Drawing.Size(80, 339);
            this.pnlRules.TabIndex = 5;
            this.pnlRules.DragDrop += new System.Windows.Forms.DragEventHandler(this.PnlRules_DragDrop);
            this.pnlRules.DragEnter += new System.Windows.Forms.DragEventHandler(this.PnlRules_DragEnter);
            this.pnlRules.DragOver += new System.Windows.Forms.DragEventHandler(this.PnlRules_DragOver);
            this.pnlRules.DragLeave += new System.EventHandler(this.PnlRules_DragLeave);
            this.pnlRules.MouseEnter += new System.EventHandler(this.ShowForm);
            this.pnlRules.MouseLeave += new System.EventHandler(this.HideForm);
            // 
            // pnlScrollDown
            // 
            this.pnlScrollDown.AllowDrop = true;
            this.pnlScrollDown.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.pnlScrollDown.BackgroundImage = global::FirewallWidget.Presentation.Properties.Resources.down_arrow;
            this.pnlScrollDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlScrollDown.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlScrollDown.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlScrollDown.Location = new System.Drawing.Point(0, 356);
            this.pnlScrollDown.Name = "pnlScrollDown";
            this.pnlScrollDown.Size = new System.Drawing.Size(80, 9);
            this.pnlScrollDown.TabIndex = 4;
            this.pnlScrollDown.Tag = "ScrollDown";
            this.pnlScrollDown.Click += new System.EventHandler(this.ScrollRulesDownHandler);
            this.pnlScrollDown.DragEnter += new System.Windows.Forms.DragEventHandler(this.PnlScroll_DragEnter);
            this.pnlScrollDown.DragOver += new System.Windows.Forms.DragEventHandler(this.PnlScroll_DragOver);
            this.pnlScrollDown.MouseEnter += new System.EventHandler(this.ShowForm);
            this.pnlScrollDown.MouseLeave += new System.EventHandler(this.HideForm);
            // 
            // pnlScrollUp
            // 
            this.pnlScrollUp.AllowDrop = true;
            this.pnlScrollUp.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pnlScrollUp.BackgroundImage = global::FirewallWidget.Presentation.Properties.Resources.up_arrow2_disabled;
            this.pnlScrollUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pnlScrollUp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pnlScrollUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlScrollUp.Location = new System.Drawing.Point(0, 8);
            this.pnlScrollUp.Name = "pnlScrollUp";
            this.pnlScrollUp.Size = new System.Drawing.Size(80, 9);
            this.pnlScrollUp.TabIndex = 2;
            this.pnlScrollUp.Tag = "ScrollUp";
            this.pnlScrollUp.Click += new System.EventHandler(this.ScrollRulesUpHandler);
            this.pnlScrollUp.DragEnter += new System.Windows.Forms.DragEventHandler(this.PnlScroll_DragEnter);
            this.pnlScrollUp.DragOver += new System.Windows.Forms.DragEventHandler(this.PnlScroll_DragOver);
            this.pnlScrollUp.MouseEnter += new System.EventHandler(this.ShowForm);
            this.pnlScrollUp.MouseLeave += new System.EventHandler(this.HideForm);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(80, 365);
            this.Controls.Add(this.pnlRules);
            this.Controls.Add(this.pnlScrollDown);
            this.Controls.Add(this.pnlScrollUp);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(80, 3000);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.panel1.ResumeLayout(false);
            this.optionsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.ContextMenuStrip optionsMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addRulesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem publicAllowOutboundToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.Panel pnlScrollUp;
        private System.Windows.Forms.Panel pnlScrollDown;
        private System.Windows.Forms.Panel pnlRules;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
    }
}

