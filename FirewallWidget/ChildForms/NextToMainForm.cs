using System.Drawing;
using System.Windows.Forms;

namespace FirewallWidget.ChildForms
{
    public class NextToMainForm : Form
    {
        private NextToMainForm()
        { }

        public NextToMainForm(Form main)
            : this()
        {
            Shown += (s, e) =>
            {
                if (main != null)
                {
                    Location = PointToScreen(
                        new Point(main.Location.X + main.Width + 2, main.Location.Y));
                }
            };
        }

    }
}
