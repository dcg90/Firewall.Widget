using FirewallWidget.Manager.DTO;

using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FirewallWidget.Presentation.ChildForms.CreateRule
{
    public partial class CreateIpForm : Form
    {

        private static readonly Regex ipOrSubnetRe =
            new Regex(@"^(?<ip>\d{1,3}(?:\.\d{1,3}){3})(?:/(?<mask>[1-9]\d?))?$");
        private static readonly Regex ipRe =
            new Regex(@"^\d{1,3}(?:\.\d{1,3}){3}$");

        private readonly Dictionary<RadioButton, Control[][]> rbtnToggles;

        public CreateIpForm()
        {
            InitializeComponent();

            rbtnToggles = new Dictionary<RadioButton, Control[][]>
            {
                [rbtnIpOrSubnet] = new[] {
                    new [] { tboxIpOrSubnet }, new[] { tboxIpIntervalFrom, tboxIpIntervalTo } },
                [rbtnIpInterval] = new[] {
                    new[]{tboxIpIntervalFrom, tboxIpIntervalTo}, new[] { tboxIpOrSubnet }
                }
            };
        }

        private void RBtn_ToggleEnable(object sender, System.EventArgs e)
        {
            if (sender is RadioButton rbtn)
            {
                foreach (var c in rbtnToggles[rbtn][0]) { c.Enabled = rbtn.Checked; }
                foreach (var c in rbtnToggles[rbtn][1]) { c.Enabled = !rbtn.Checked; }
            }

        }

        private void BtnOk_Click(object sender, System.EventArgs e)
        {
            if (rbtnIpOrSubnet.Checked)
            {
                var m = ipOrSubnetRe.Match(tboxIpOrSubnet.Text);
                if (m.Success)
                {
                    Ip = new IPModel
                    {
                        IpOrSubnet = m.Groups["ip"].Value,
                        SubnetMask = m.Groups["mask"].Value
                    };
                    return;
                }
            }
            if (rbtnIpInterval.Checked)
            {
                var m1 = ipRe.Match(tboxIpIntervalFrom.Text);
                var m2 = ipRe.Match(tboxIpIntervalTo.Text);
                if (m1.Success && m2.Success &&
                    IpDto.ToInt32(tboxIpIntervalTo.Text) > IpDto.ToInt32(tboxIpIntervalFrom.Text))
                {
                    Ip = new IPModel
                    {
                        From = tboxIpIntervalFrom.Text,
                        To = tboxIpIntervalTo.Text
                    };
                    return;
                }
            }

            MessageBox.Show(
                "Enter a valid Ip configuration", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            DialogResult = DialogResult.None;
        }

        public IPModel Ip { get; set; }
    }
}
