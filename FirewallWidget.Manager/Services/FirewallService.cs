using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;

using NetFwTypeLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using ReGroup = System.Text.RegularExpressions.Group;

namespace FirewallWidget.Manager.Services
{
    internal class Group
    {
        public ProfileDto Profile { get; set; }

        public RuleDirectionDto Direction { get; set; }

        public IEnumerable<FirewallRuleDto> Rules { get; set; }
    }

    public class FirewallService : IFirewallService
    {
        private static ICollection<Group> groupedRules;
        private static readonly INetFwPolicy2 firewallPolicy
            = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
        private static readonly Regex ipRe = new Regex(@"^(\d{1,3})(?:\.(\d{1,3})){3}$");
        private static readonly Regex portsRe = new Regex(@"^(\d+(?:-\d+)?)(?:,(\d+(?:-\d+)?))*$");

        static FirewallService()
        {
            LoadFirewallRules();
        }

        public IEnumerable<FirewallRuleDto> GetRules(ProfileDto profile, RuleDirectionDto direction)
        {
            foreach (var group in groupedRules)
            {
                if (group.Profile == profile && group.Direction == direction)
                { return group.Rules; }
            }

            return Enumerable.Empty<FirewallRuleDto>();
        }

        public bool IsEnabled(FirewallRuleDto rule)
        {
            var fwRule = ExtractFwRule(rule);
            if (fwRule != null)
            { return fwRule.Enabled; }

            throw DontExistOrSeveralFound(rule);
        }

        public IEnumerable<FirewallRuleDto> GetMatchingRules(
            string name, ProfileDto profile, RuleDirectionDto direction)
        {
            TryGetRules(name, profile, direction, out var rules);
            return rules;
        }

        public bool SwitchEnabled(FirewallRuleDto rule)
        {
            var fwRule = ExtractFwRule(rule);
            if (fwRule != null)
            {
                fwRule.Enabled = !fwRule.Enabled;
                return fwRule.Enabled;
            }

            throw DontExistOrSeveralFound(rule);

        }

        public void Refresh()
        {
            LoadFirewallRules();
        }

        private INetFwRule3 ExtractFwRule(FirewallRuleDto rule)
        {
            if (rule != null)
            {
                if (rule.FwRule != null)
                { return rule.FwRule; }

                if (TryGetRules(rule.Name, rule.Profile, rule.Direction, out var rules) && rules.Count() == 1)
                { return rules.Single().FwRule; }
            }

            return null;
        }

        private static InvalidOperationException DontExistOrSeveralFound(FirewallRuleDto rule)
        {
            return new InvalidOperationException("Rule " + rule?.Name + " doesn't exists.");
        }

        private bool TryGetRules(
            string name, ProfileDto profile, RuleDirectionDto direction,
            out IEnumerable<FirewallRuleDto> matchingRules)
        {
            matchingRules = GetRules(profile, direction)
                .Where(r => r.Name == name);

            return matchingRules.Count() > 0;
        }

        private static void LoadFirewallRules()
        {
            var rules = new List<INetFwRule3>();

            foreach (INetFwRule3 r in firewallPolicy.Rules)
            { rules.Add(r); }

            var allGroups = from r in rules
                            orderby r.Name
                            group r by new { r.Profiles, r.Direction } into g
                            select new { g.Key.Profiles, g.Key.Direction, Rules = g };

            groupedRules = new List<Group>();

            foreach (var group in allGroups)
            {
                foreach (int profile in Enum.GetValues(typeof(ProfileDto)))
                {
                    if ((profile & group.Profiles) == profile)
                    {
                        var g = groupedRules.FirstOrDefault(
                            gi => (int)gi.Profile == profile &&
                                  gi.Direction == (RuleDirectionDto)group.Direction);
                        if (g == null)
                        {
                            g = new Group
                            {
                                Direction = (RuleDirectionDto)group.Direction,
                                Profile = (ProfileDto)profile,
                                Rules = Enumerable.Empty<FirewallRuleDto>()
                            };
                            groupedRules.Add(g);
                        }
                        g.Rules = g.Rules.Concat(group.Rules.Select(ri => new FirewallRuleDto
                        {
                            Direction = (RuleDirectionDto)group.Direction,
                            Name = ri.Name,
                            Profile = (ProfileDto)profile,
                            ProgramPath = ri.ApplicationName,
                            FwRule = ri
                        }));
                    }
                }
            }

            foreach (var group in groupedRules)
            { group.Rules = group.Rules.OrderBy(r => r.Name); }
        }

        public bool OutboundConnectionsAllowedOn(ProfileDto profileDto)
        {

            var profile = (NET_FW_PROFILE_TYPE2_)profileDto;
            var action = firewallPolicy.get_DefaultOutboundAction(profile);

            return action == NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
        }

        public void SwitchOutboundConnectionsStateOn(ProfileDto profileDto)
        {
            var allowedOutboundConnections = OutboundConnectionsAllowedOn(profileDto);
            var profile = (NET_FW_PROFILE_TYPE2_)profileDto;
            firewallPolicy.set_DefaultOutboundAction(
                profile,
                allowedOutboundConnections
                    ? NET_FW_ACTION_.NET_FW_ACTION_BLOCK
                    : NET_FW_ACTION_.NET_FW_ACTION_ALLOW);
        }

        public FirewallRuleDto CreateFirewallRule(CreateFirewallRuleDto rule)
        {

            var (parseOk, ips, proto, ports) = ParseRuleData(rule);
            if (parseOk)
            {
                if (Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule")) is INetFwRule3 fwRule)
                {
                    fwRule.Name = rule.Name;
                    fwRule.Enabled = rule.CreateRuleEnabled;
                    fwRule.InterfaceTypes = "All";
                    fwRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
                    fwRule.Direction = (NET_FW_RULE_DIRECTION_)rule.Direction;
                    fwRule.Profiles = (int)rule.Profile;

                    if (!string.IsNullOrEmpty(ips))
                    { fwRule.RemoteAddresses = ips; }

                    if (proto != null)
                    {
                        fwRule.Protocol = proto.Value;
                        fwRule.RemotePorts = ports;
                    }

                    if (!string.IsNullOrEmpty(rule.ProgramPath))
                    { fwRule.ApplicationName = rule.ProgramPath; }

                    firewallPolicy.Rules.Add(fwRule);
                    var firewallRuleDto = new FirewallRuleDto
                    {
                        FwRule = fwRule,
                        Profile = rule.Profile,
                        Direction = rule.Direction,
                        Name = rule.Name,
                        ProgramPath = rule.ProgramPath
                    };
                    UpdateRulesGroup(rule.Profile, rule.Direction, firewallRuleDto);

                    return firewallRuleDto;
                }
            }

            return null;
        }

        private void UpdateRulesGroup(ProfileDto profile, RuleDirectionDto direction, FirewallRuleDto firewallRuleDto)
        {
            Group group = null;
            foreach (var g in groupedRules)
            {
                if (g.Profile == profile && g.Direction == direction)
                { group = g; break; }
            }

            group = group ?? new Group
            {
                Direction = direction,
                Profile = profile,
                Rules = Enumerable.Empty<FirewallRuleDto>()
            };

            group.Rules = group.Rules
                .Concat(new[] { firewallRuleDto })
                .OrderBy(r => r.Name);
        }

        private (bool, string, int?, string) ParseRuleData(CreateFirewallRuleDto rule)
        {
            (bool, string, int?, string) invalid = (false, null, null, null);

            if (rule == null)
            { return invalid; }

            if (string.IsNullOrEmpty(rule.Name))
            { return invalid; }

            if (string.IsNullOrEmpty(rule.ProgramPath) &&
                rule.Ips?.Length == 0 &&
                rule.Protocol == null &&
                string.IsNullOrEmpty(rule.Ports))
            { return invalid; }

            var proto = rule.Protocol == null ? null : (int?)rule.Protocol;
            var (portsOk, ports) = ParsePorts(rule.Ports);
            var (ipsOk, ips) = ParseIps(rule.Ips);

            if (proto is null)
            { portsOk = true; ports = null; }

            return ipsOk && portsOk
                ? (true, ips, proto, ports)
                : invalid;
        }

        private (bool, string) ParseIps(IpDto[] ips)
        {
            var allIps = new List<string>();
            foreach (var ip in ips ?? new IpDto[0])
            {
                var (ipOk, ipParsed) = ParseIp(ip);
                if (!ipOk)
                { return (false, null); }
                allIps.Add(ipParsed);
            }

            return (true, /*allIps.Count == 0 ? "*" :*/ string.Join(",", allIps));
        }

        private (bool, string) ParseIp(IpDto ipDto)
        {
            string _BuildIp(string _ip)
            {
                var m = ipRe.Match(_ip ?? "");
                return m.Success
                    ? string.Join(
                      ".",
                      m.Groups.OfType<ReGroup>().Skip(1)
                              .SelectMany(g => g.Captures.OfType<Capture>())
                              .Select(c => int.Parse(c.Value).ToString()))
                    : null;
            }

            if (!string.IsNullOrEmpty(ipDto.IpOrSubnet))
            {
                var ip = _BuildIp(ipDto.IpOrSubnet);
                if (ip == null)
                { return (false, null); }
                var mask = IpDto.IpMask(int.TryParse(ipDto.SubnetMask, out var ipMask) ? ipMask : 32);
                return (true, $"{ip}/{mask}");
            }

            var from = _BuildIp(ipDto.From);
            var to = _BuildIp(ipDto.To);

            return from != null && to != null
                ? (true, $"{from}-{to}")
                : (false, null);
        }

        private static (bool, string) ParsePorts(string ports)
        {
            var match = portsRe.Match(ports);
            if (match.Success)
            {
                var portsEnum = ports.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(s => s.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries))
                         .Select(arr => string.Join("-", arr.Select(s => int.Parse(s))));
                if (portsEnum.Where(s => s.Contains("-"))
                             .Select(s => s.Split('-'))
                             .Any(arr => int.Parse(arr[0]) >= int.Parse(arr[1])))
                { return (false, null); }

                return (true, string.Join(",", portsEnum));
            }
            return (false, null);
        }
    }
}
