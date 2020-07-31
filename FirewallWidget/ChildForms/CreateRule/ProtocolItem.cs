using FirewallWidget.Manager.DTO;

namespace FirewallWidget.Presentation.ChildForms.CreateRule
{
    public class ProtocolItem
    {
        public string Display { get; set; }

        public ProtocolDto Protocol { get; set; }

        public override string ToString()
        {
            return Display;
        }
    }
}
