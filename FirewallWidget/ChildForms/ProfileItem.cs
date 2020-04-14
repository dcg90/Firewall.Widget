using FirewallWidget.Manager.DTO;

namespace FirewallWidget.ChildForms
{
    internal class ProfileItem
    {
        public string Display { get; set; }

        public ProfileDto Profile { get; set; }

        public override string ToString()
        {
            return Display;
        }
    }
}
