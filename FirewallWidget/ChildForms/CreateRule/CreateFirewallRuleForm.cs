using FirewallWidget.ChildForms;
using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;

using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using static FirewallWidget.Presentation.FirewallWidgetConstants;

namespace FirewallWidget.Presentation.ChildForms.CreateRule
{
    public partial class CreateFirewallRuleForm : NextToMainForm
    {
        private static readonly Regex portsRe =
            new Regex(@"^(?:\d+(?:-\d+)?)(?:,\d+(?:-\d+)?)*$");
        private readonly IFirewallService firewallService;

        public CreateFirewallRuleForm(
            Form main, IOptionsService optionsService, IFirewallService firewallService)
            : base(main, optionsService)
        {
            this.firewallService = firewallService;

            InitializeComponent();

            FillCombos();
        }

        private void FillCombos()
        {
            cboxProfile.Items.AddRange(PROFILES);
            cboxDirection.Items.AddRange(DIRECTIONS);

            cboxProfile.SelectedItem = PROFILES[2];
            cboxDirection.SelectedItem = DIRECTIONS[1];

            cboxProtocol.Items.AddRange(new[]
            {
                new ProtocolItem{ Display ="TCP",  Protocol = ProtocolDto.TCP },
                new ProtocolItem{ Display="UDP", Protocol = ProtocolDto.UDP }
            });
        }

        private void CboxLoadFromProcess_CheckedChanged(object sender, EventArgs e)
        {
            if (cboxProcessNames.Items.Count == 0)
            { LoadProcessNames(); }

            cboxProcessNames.Enabled =
                btnRefreshProcesses.Enabled =
                cboxLoadFromProcess.Checked;
            tboxProgramPath.Enabled =
                btnSelectPath.Enabled =
                !cboxLoadFromProcess.Checked;
        }

        private void BtnRefreshProcesses_Click(object sender, EventArgs e)
        {
            LoadProcessNames();
        }

        private void LoadProcessNames()
        {
            var processes = new AutoCompleteStringCollection();
            var processNames = Process
                .GetProcesses()
                .Select(p => p.ProcessName)
                .Distinct()
                .OrderBy(p => p)
                .ToArray();

            cboxProcessNames.Items.Clear();
            cboxProcessNames.Items.AddRange(processNames);

            processes.AddRange(processNames);
            cboxProcessNames.AutoCompleteCustomSource = processes;
        }

        private void CboxProcessNames_SelectedIndexChanged(object sender, EventArgs e)
        { UpdateProgramPathFromProcess(); }

        private void CboxProcessNames_TextChanged(object sender, EventArgs e)
        { UpdateProgramPathFromProcess(); }

        private void UpdateProgramPathFromProcess()
        {
            if (string.IsNullOrEmpty(cboxProcessNames.Text))
            { tboxProgramPath.Text = string.Empty; }
            else
            {
                var process = Process
                            .GetProcessesByName(cboxProcessNames.Text)
                            .Where(p => !string.IsNullOrEmpty(p.MainModule.FileName))
                            .FirstOrDefault();
                tboxProgramPath.Text = process?.MainModule.FileName;
            }
        }

        private void BtnAddIp_Click(object sender, EventArgs e)
        {
            using (var createIp = new CreateIpForm())
            {
                if (createIp.ShowDialog(this) == DialogResult.OK)
                { lboxIpAddresses.Items.Add(createIp.Ip); }
            }
        }

        private void BtnRemoveIps_Click(object sender, EventArgs e)
        {
            for (var i = lboxIpAddresses.SelectedItems.Count - 1; i >= 0; i--)
            { lboxIpAddresses.Items.Remove(lboxIpAddresses.SelectedItems[i]); }
        }

        private void BtnCreateRule_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                var createRuleDto = CreateFirewallRule();
                Rule = firewallService.CreateFirewallRule(createRuleDto);
            }
            else
            { DialogResult = DialogResult.None; }
        }

        private CreateFirewallRuleDto CreateFirewallRule()
        {
            var createFirewallRule = new CreateFirewallRuleDto
            {
                Direction = (cboxDirection.SelectedItem as DirectionItem).Direction,
                Ips = lboxIpAddresses.Items.OfType<IPModel>()
                    .Select(m => new IpDto
                    {
                        From = m.From,
                        IpOrSubnet = m.IpOrSubnet,
                        SubnetMask = m.SubnetMask,
                        To = m.To
                    })
                    .ToArray(),
                Name = tboxRuleName.Text,
                Ports = tboxPorts.Text ?? "",
                Profile = (cboxProfile.SelectedItem as ProfileItem).Profile,
                ProgramPath = tboxProgramPath.Text ?? "",
                CreateRuleEnabled = cboxCreateEnabled.Checked
            };

            if (cboxProtocol.SelectedItem is ProtocolItem item)
            { createFirewallRule.Protocol = item.Protocol; }

            return createFirewallRule;
        }

        private bool ValidateInputs()
        {
            var errorsBuilder = new StringBuilder();
            if (string.IsNullOrEmpty(tboxRuleName.Text))
            { errorsBuilder.AppendLine("  * Specify a rule name."); }

            if (string.IsNullOrEmpty(tboxProgramPath.Text) &&
                lboxIpAddresses.Items.Count == 0 &&
                cboxProtocol.SelectedItem == null &&
                string.IsNullOrEmpty(tboxPorts.Text))
            {
                errorsBuilder.AppendLine("  * Specify either program, IP address(es), protocol, port(s)\n" +
                  "     or a combination.");
            }

            if (cboxProtocol.SelectedItem != null)
            {
                if (portsRe.IsMatch(tboxPorts.Text))
                {
                    if (tboxPorts.Text.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                       .Where(s => s.Contains("-"))
                       .Select(s => s.Split('-'))
                       .Any(s => int.Parse(s[0]) >= int.Parse(s[1])))
                    { errorsBuilder.AppendLine("  * Port range(s)."); }
                }
                else
                { errorsBuilder.AppendLine("  * Enter a valid comma-sparated list of ports."); }
            }

            var errors = errorsBuilder.ToString();
            if (!string.IsNullOrEmpty(errors))
            {
                MessageBox.Show(
                    $"Fix the following:\n\n{errors}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        public FirewallRuleDto Rule { get; private set; }

        private void CboxProtocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            tboxPorts.Enabled = true;
        }
    }
}
