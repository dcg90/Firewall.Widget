using FirewallWidget.Manager.DTO;

namespace FirewallWidget.ChildForms
{
    internal class DirectionItem
    {
        public string Display { get; set; }

        public RuleDirectionDto Direction { get; set; }

        public override string ToString()
        {
            return Display;
        }
    }
}
