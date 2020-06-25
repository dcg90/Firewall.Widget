using FirewallWidget.Presentation.Properties;

using System.Drawing;
using System.Windows.Forms;

using static FirewallWidget.Presentation.FirewallWidgetConstants;

namespace FirewallWidget
{
    internal static class PanelExtension
    {
        public static void Disable(this Panel pnl)
        {
            switch (pnl.Tag)
            {
                case SCROLL_UP_TAG:
                    pnl.BackColor = SystemColors.ScrollBar;
                    pnl.BackgroundImage = Resources.up_arrow2_disabled;
                    pnl.Cursor = Cursors.Default;
                    break;
                case SCROLL_DOWN_TAG:
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
                case SCROLL_UP_TAG:
                    pnl.BackColor = SystemColors.GradientActiveCaption;
                    pnl.BackgroundImage = Resources.up_arrow2;
                    pnl.Cursor = Cursors.Hand;
                    break;
                case SCROLL_DOWN_TAG:
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
