using System.Windows.Forms;

namespace FirewallWidget
{
    public class NoTaskBarAltTabForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var @params = base.CreateParams;
                @params.ExStyle |= 0x80;
                return @params;
            }
        }
    }
}
