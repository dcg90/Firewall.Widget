using FirewallWidget.Manager.DTO;

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

using static FirewallWidget.Presentation.FirewallWidgetConstants;

namespace FirewallWidget.Presentation
{
    public partial class RuleControl
    {
        private void MyInitializeComponent(Point? location)
        {
            Location = location != null
                ? location.Value
                : LocationFromPrevious();

            if (Previous != null)
            { Previous.LocationChanged += UpdateLocation; }

            SetPictureBoxImage();
            ruleNameToolTip.SetToolTip(pboxRule, firewallRuleDto.Name);
        }

        private void UpdateLocation(object sender, EventArgs e)
        {
            Location = LocationFromPrevious();
        }

        private Point LocationFromPrevious()
        {
            return new Point(
                Previous.Location.X, Previous.Location.Y + Previous.Height + VERTICAL_RULES_MARGIN);
        }

        private void SetPictureBoxImage()
        {
            CreateRuleIcon();

            pboxRule.Image = firewallService.IsEnabled(firewallRuleDto)
                     ? icon
                     : iconGrayScale;
        }

        private void CreateRuleIcon()
        {
            if (Rule.Icon != null)
            { (icon, iconGrayScale) = (Rule.Icon, (Bitmap)ToolStripRenderer.CreateDisabledImage(Rule.Icon)); }
            else if (File.Exists(firewallRuleDto.ProgramPath))
            {
                icon = GetExeIcon(firewallRuleDto.ProgramPath);
                iconGrayScale = (Bitmap)ToolStripRenderer.CreateDisabledImage(icon);
            }
            else
            {
                icon = CreateDefaultRuleIcon(true);
                iconGrayScale = CreateDefaultRuleIcon(false);
            }

            DrawDirection(firewallRuleDto.Direction, icon);
            DrawDirection(firewallRuleDto.Direction, iconGrayScale);
        }

        private static Bitmap GetExeIcon(string filename)
        {
            return Icon.ExtractAssociatedIcon(filename).ToBitmap();
        }

        private static Bitmap CreateDefaultRuleIcon(bool enabledRule)
        {
            var icon = new Bitmap(32, 32);
            var graphics = Graphics.FromImage(icon);
            var rect = new RectangleF(0, 0, 32f, 32f);
            graphics.FillEllipse(enabledRule ? Brushes.DarkCyan : Brushes.Gray, rect);
            graphics.DrawLine(new Pen(Color.Red, 1.5f), new Point(), new Point(32, 32));
            graphics.DrawLine(new Pen(Color.Red, 1.5f), new Point(32, 0), new Point(0, 32));

            return icon;
        }

        private void DrawDirection(RuleDirectionDto direction, Bitmap icon)
        {
            var text = direction.ToString();
            var font = new Font("Consolas", 9.5f, FontStyle.Regular, GraphicsUnit.Point);
            var graphics = Graphics.FromImage(icon);
            var textSize = graphics.MeasureString(text, font);

            var rectangle = new RectangleF(
                new PointF(icon.Width - textSize.Width + 2, icon.Height - textSize.Height + 2),
                new SizeF(textSize.Width - 2, textSize.Height - 2));

            var brush = new SolidBrush(Color.FromArgb(170, Color.Black));

            graphics.FillPath(brush, RoundedRect(rectangle, 2));
            graphics.DrawString(text, font, Brushes.White, rectangle.Location);
        }

        // Reference: https://stackoverflow.com/a/33853557/4152153
        private static GraphicsPath RoundedRect(RectangleF bounds, float radius)
        {
            var diameter = radius * 2;
            var size = new SizeF(diameter, diameter);
            var arc = new RectangleF(bounds.Location, size);
            var path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void RaiseHideForm()
        {
            HideForm?.Invoke(this);
        }

        private void SwitchRuleEnabled()
        {
            try
            {
                var enabled = firewallService.SwitchEnabled(firewallRuleDto);
                pboxRule.Image = enabled ? icon : iconGrayScale;
            }
            catch (Exception exc)
            {
                if (MessageBox.Show(
                    "The following error occurred: \n" + exc.Message + "\nDelete this rule?", "Error",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    DeleteRule?.Invoke(this, Rule);
                }
            }
        }


    }
}
