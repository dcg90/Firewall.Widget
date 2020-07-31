using System.Linq;

namespace FirewallWidget.Manager.DTO
{
    public class CreateFirewallRuleDto
    {
        public ProfileDto Profile { get; set; }

        public RuleDirectionDto Direction { get; set; }

        public string ProgramPath { get; set; }

        public string Name { get; set; }

        public IpDto[] Ips { get; set; }

        public ProtocolDto? Protocol { get; set; }

        public string Ports { get; set; }

        public bool CreateRuleEnabled { get; set; }
    }

    public enum ProtocolDto
    {
        TCP = 6,
        UDP = 17
    }

    public class IpDto
    {
        public string IpOrSubnet { get; set; }

        public string SubnetMask { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public static long ToInt32(string ip)
        {
            var octets = ip?.Split(new[] { '.' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (octets?.Length != 4 && octets.Any(o => o.Length > 3))
            { return -1; }

            long result = 0;
            foreach (var octet in octets)
            {
                if (long.TryParse(octet, out var octetVal))
                {
                    result <<= 8;
                    result |= octetVal;
                }
                else { return -1; }
            }

            return result;
        }

        public static string FromInt32(long intIp)
        {
            var ip = string.Empty;

            for (var i = 0; i < 4; i++)
            {
                if (i > 0)
                { ip = "." + ip; }

                ip = (intIp & 255) + ip;
                intIp >>= 8;
            }

            return ip;
        }

        public static string IpMask(int bits)
        {
            if (bits > 32)
            { return null; }

            long n = (uint.MaxValue << (32 - bits)) & uint.MaxValue;
            return FromInt32(n);
        }
    }
}
