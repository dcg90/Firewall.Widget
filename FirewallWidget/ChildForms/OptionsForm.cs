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
            : base(mainForm, optionsService)
        {
            InitializeComponent();

            options = optionsService.ReadOptions();
            this.optionsService = optionsService;

            MyInitializeComponent();
        }

        private void MyInitializeComponent()
        {
            cboxOverrideRules.Checked = options.OverrideRules;
            cboxDockLeft.Checked = options.DockLeft;
        }

        private void CboxOverrideRules_CheckedChanged(object sender, System.EventArgs e)
        {
            options.OverrideRules = cboxOverrideRules.Checked;
            UpdateOptions();
        }

        private void CboxDockLeft_CheckedChanged(object sender, System.EventArgs e)
        {
            options.DockLeft = cboxDockLeft.Checked;
            UpdateOptions();
        }

        private void UpdateOptions()
        {
            optionsService.UpdateOptions(options);
        }
    }
}
