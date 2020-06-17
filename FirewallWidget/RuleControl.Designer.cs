namespace FirewallWidget
{
    partial class RuleControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pboxRule = new System.Windows.Forms.PictureBox();
            this.pboxContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.removeRuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ruleNameToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pboxRule)).BeginInit();
            this.pboxContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // pboxRule
            // 
            this.pboxRule.ContextMenuStrip = this.pboxContext;
            this.pboxRule.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pboxRule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pboxRule.Location = new System.Drawing.Point(0, 0);
            this.pboxRule.Name = "pboxRule";
            this.pboxRule.Size = new System.Drawing.Size(32, 32);
            this.pboxRule.TabIndex = 0;
            this.pboxRule.TabStop = false;
            this.pboxRule.Click += new System.EventHandler(this.PboxRule_Click);
            this.pboxRule.MouseLeave += new System.EventHandler(this.PboxRule_MouseLeave);
            // 
            // pboxContext
            // 
            this.pboxContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setIconToolStripMenuItem,
            this.removeIconToolStripMenuItem,
            this.toolStripMenuItem1,
            this.removeRuleToolStripMenuItem});
            this.pboxContext.Name = "pboxContext";
            this.pboxContext.Size = new System.Drawing.Size(181, 98);
            this.pboxContext.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.PboxContext_Closed);
            this.pboxContext.Opening += new System.ComponentModel.CancelEventHandler(this.PboxContext_Opening);
            // 
            // setIconToolStripMenuItem
            // 
            this.setIconToolStripMenuItem.Name = "setIconToolStripMenuItem";
            this.setIconToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.setIconToolStripMenuItem.Text = "Set Icon";
            this.setIconToolStripMenuItem.Click += new System.EventHandler(this.SetIconToolStripMenuItem_Click);
            // 
            // removeIconToolStripMenuItem
            // 
            this.removeIconToolStripMenuItem.Name = "removeIconToolStripMenuItem";
            this.removeIconToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeIconToolStripMenuItem.Text = "Remove Icon";
            this.removeIconToolStripMenuItem.Click += new System.EventHandler(this.RemoveIconToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // removeRuleToolStripMenuItem
            // 
            this.removeRuleToolStripMenuItem.Name = "removeRuleToolStripMenuItem";
            this.removeRuleToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.removeRuleToolStripMenuItem.Text = "Remove Rule";
            this.removeRuleToolStripMenuItem.Click += new System.EventHandler(this.RemoveRuleToolStripMenuItem_Click);
            // 
            // RuleControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pboxRule);
            this.Name = "RuleControl";
            this.Size = new System.Drawing.Size(32, 32);
            ((System.ComponentModel.ISupportInitialize)(this.pboxRule)).EndInit();
            this.pboxContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pboxRule;
        private System.Windows.Forms.ToolTip ruleNameToolTip;
        private System.Windows.Forms.ContextMenuStrip pboxContext;
        private System.Windows.Forms.ToolStripMenuItem setIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeIconToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeRuleToolStripMenuItem;
    }
}
