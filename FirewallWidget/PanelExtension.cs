using FirewallWidget.Properties;

using System.Drawing;
using System.Windows.Forms;

namespace FirewallWidget
{
    internal static class PanelExtension
    {
        public static void Disable(this Panel pnl)
        {
            switch (pnl.Tag)
            {
                case "ScrollUp":
                    pnl.BackColor = SystemColors.ScrollBar;
                    pnl.BackgroundImage = Resources.up_arrow2_disabled;
                    pnl.Cursor = Cursors.Default;
                    break;
                case "ScrollDown":
                    pnl.BackColor = SystemColors.ScrollBar;
                    pnl.BackgroundImage = Resources.down_arrow_disabled;
                    pnl.Cursor = Cursors.Default;
                    break;
                default:
                    break;
            }
        }

        public static void Enable(this Panel pnl)
        {
            switch (pnl.Tag)
            {
                case "ScrollUp":
                    pnl.BackColor = SystemColors.GradientActiveCaption;
                    pnl.BackgroundImage = Resources.up_arrow2;
                    pnl.Cursor = Cursors.Hand;
                    break;
                case "ScrollDown":
                    pnl.BackColor = SystemColors.GradientActiveCaption;
                    pnl.BackgroundImage = Resources.down_arrow;
                    pnl.Cursor = Cursors.Hand;
                    break;
                default:
                    break;
            }
        }
    }
}
