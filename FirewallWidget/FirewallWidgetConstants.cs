using System.Drawing;

namespace FirewallWidget.Presentation
{
    internal static class FirewallWidgetConstants
    {
        public static readonly string RULE_CONTROL_DRAG_FORMAT = typeof(RuleControl).FullName;

        public static readonly Point FIRST_RULE_LOCATION = new Point(5, 5);

        public const int VERTICAL_RULES_MARGIN = 6;

        public const string SCROLL_UP_TAG = "ScrollUp";

        public const string SCROLL_DOWN_TAG = "ScrollDown";

        public const int FORM_WIDTH = 42;
    }
}
