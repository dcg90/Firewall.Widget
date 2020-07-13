using FirewallWidget.ChildForms;
using FirewallWidget.Manager.DTO;

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

        public static readonly ProfileItem[] PROFILES = new[]
        {
            new ProfileItem { Display = "Domain", Profile = ProfileDto.Domain },
            new ProfileItem { Display = "Private", Profile = ProfileDto.Private },
            new ProfileItem { Display = "Public", Profile = ProfileDto.Public }
        };

        public static readonly DirectionItem[] DIRECTIONS = new[]
        {
            new DirectionItem { Display = "In", Direction = RuleDirectionDto.In },
            new DirectionItem { Display = "Out", Direction = RuleDirectionDto.Out },
            new DirectionItem { Display = "Max", Direction = RuleDirectionDto.Max }
        };
    }
}
