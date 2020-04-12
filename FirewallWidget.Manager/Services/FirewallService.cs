using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.DTO;

using NetFwTypeLib;

using System;
using System.Collections.Generic;
using System.Linq;

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

        public bool IsEnabled(string name, ProfileDto profile, RuleDirectionDto direction)
        {
            if (TryGetRule(name, profile, direction, out var rule))
            { return rule.FwRule.Enabled; }

            throw new InvalidOperationException("Rule " + name + " doesn't exists.");
        }

        public bool Exists(string name, ProfileDto profile, RuleDirectionDto direction)
        {
            return TryGetRule(name, profile, direction, out var _);
        }

        public bool SwitchEnabled(string name, ProfileDto profile, RuleDirectionDto direction)
        {
            if (TryGetRule(name, profile, direction, out var rule))
            {
                rule.FwRule.Enabled = !rule.FwRule.Enabled;
                return rule.FwRule.Enabled;
            }

            throw new InvalidOperationException("Rule " + name + " doesn't exists.");
        }

        public void Refresh()
        {
            LoadFirewallRules();
        }

        private bool TryGetRule(string name, ProfileDto profile, RuleDirectionDto direction, out FirewallRuleDto rule)
        {
            rule = null;

            foreach (var r in GetRules(profile, direction))
            {
                if (r.Name == name)
                {
                    rule = r;
                    return true;
                }
            }

            return false;
        }

        private static void LoadFirewallRules()
        {
            var firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
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
    }
}
