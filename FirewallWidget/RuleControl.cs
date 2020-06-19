using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using static FirewallWidget.Presentation.FirewallWidgetConstants;

namespace FirewallWidget.Presentation
{
    public partial class RuleControl : UserControl
    {
        private readonly FirewallRuleDto firewallRuleDto;
        private readonly IFirewallService firewallService;

        private Bitmap iconGrayScale;
        private Bitmap icon;
        private bool mouseDown;
        private Point mouseDownLocation;

        public RuleControl Previous { get; private set; }

        public RuleControl Next { get; set; }

        public RuleDto Rule { get; private set; }

        public event Action<RuleControl, RuleDto> DeleteRule;
        public event Action<RuleControl, RuleDto> UpdateRule;
        public event Action<RuleControl> HideForm;

        private RuleControl(
            RuleDto rule, FirewallRuleDto firewallRuleDto,
            RuleControl previous, Point? location,
            IFirewallService firewallService)
        {
            if (previous == null && location == null)
            {
                throw new ArgumentNullException(
                  $"{nameof(previous)},{nameof(location)}");
            }

            InitializeComponent();

            Previous = previous;
            Rule = rule;
            this.firewallRuleDto = firewallRuleDto;
            this.firewallService = firewallService;

            MyInitializeComponent(location);
        }

        public RuleControl(
            RuleDto rule, FirewallRuleDto firewallRuleDto,
            RuleControl previous, IFirewallService firewallService)
            : this(rule, firewallRuleDto, previous, null, firewallService)
        { }

        public RuleControl(
            RuleDto rule, FirewallRuleDto firewallRuleDto,
            Point location, IFirewallService firewallService)
          : this(rule, firewallRuleDto, null, location, firewallService)
        { }

        public void SetPrevious(RuleControl nextPrevious, bool updateLocation = true)
        {
            if (Previous != null && !Previous.IsDisposed)
            { Previous.LocationChanged -= UpdateLocation; }

            if (nextPrevious != null)
            { nextPrevious.LocationChanged += UpdateLocation; }

            Previous = nextPrevious;
            if (updateLocation)
            {
                Location = Previous != null
                      ? LocationFromPrevious()
                      : FIRST_RULE_LOCATION;
            }
        }

        private void SetIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog()
            {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "Icon File (*.ico)|*.ico|Executable (*.exe)|*.exe",
                Multiselect = false,
                Title = "Select file to get icon"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Rule.Icon = Path.GetExtension(ofd.FileName) == ".exe"
                    ? GetExeIcon(ofd.FileName)
                    : new Icon(ofd.FileName).ToBitmap();

                UpdateRule?.Invoke(this, Rule);
                SetPictureBoxImage();
            }

            RaiseHideForm();
        }

        private void RemoveIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rule.Icon = null;
            UpdateRule?.Invoke(this, Rule);
            SetPictureBoxImage();
            RaiseHideForm();
        }

        private void RemoveRuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteRule?.Invoke(this, Rule);
            RaiseHideForm();
        }

        private void PboxRule_MouseLeave(object sender, EventArgs e)
        {
            RaiseHideForm();
        }

        private void PboxContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            removeIconToolStripMenuItem.Enabled = Rule.Icon != null;
        }

        private void PboxContext_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            RaiseHideForm();
        }

        private void PboxRule_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            mouseDownLocation = e.Location;
        }

        private void PboxRule_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown && e.Button == MouseButtons.Left)
            {
                var a = Math.Abs(e.X - mouseDownLocation.X) * Math.Abs(e.Y - mouseDownLocation.Y);
                if (a >= 5)
                {
                    mouseDown = false;
                    DoDragDrop(this, DragDropEffects.Move | DragDropEffects.Copy);
                }
            }
        }

        private void PboxRule_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            { SwitchRuleEnabled(); }
        }
    }
}
