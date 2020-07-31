using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;

using System.Drawing;
using System.Windows.Forms;

namespace FirewallWidget.ChildForms
{
    public class NextToMainForm : Form
    {
        private readonly OptionsDto options;

        public NextToMainForm() { }

        private NextToMainForm(IOptionsService optionsService)
        { options = optionsService.ReadOptions(); }

        public NextToMainForm(Form main, IOptionsService optionsService)
            : this(optionsService)
        {
            Shown += (s, e) =>
            {
                if (main != null)
                {
                    Location = options.DockLeft
                        ? new Point(main.Location.X + main.Width + 2, main.Location.Y)
                        : new Point(main.Location.X - Width - 2, main.Location.Y);
                }
            };
        }

    }
}
