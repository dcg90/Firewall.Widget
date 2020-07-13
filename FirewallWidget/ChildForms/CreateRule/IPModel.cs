namespace FirewallWidget.Presentation.ChildForms.CreateRule
{
    public class IPModel
    {
        public string IpOrSubnet { get; set; }

        public string SubnetMask { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(IpOrSubnet))
            {
                return IpOrSubnet + (!string.IsNullOrEmpty(SubnetMask)
                    ? $"/{SubnetMask}"
                    : "");
            }

            return !string.IsNullOrEmpty(From) && !string.IsNullOrEmpty(To)
                ? $"{From}-{To}"
                : "";
        }
    }
}
