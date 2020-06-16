using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;
using FirewallWidget.Presentation;

namespace FirewallWidget.ChildForms
{
    public partial class OptionsForm : NextToMainForm
    {
        private readonly OptionsDto options;
        private readonly IOptionsService optionsService;

        public OptionsForm(MainForm mainForm, IOptionsService optionsService)
            : base(mainForm)
        {
            InitializeComponent();

            options = optionsService.ReadOptions();
            this.optionsService = optionsService;

            cboxOverrideRules.Checked = options.OverrideRules;
        }

        private void cboxOverrideRules_CheckedChanged(object sender, System.EventArgs e)
        {
            options.OverrideRules = cboxOverrideRules.Checked;
            optionsService.UpdateOptions(options);
        }
    }
}
