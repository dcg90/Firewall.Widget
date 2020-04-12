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
        private static readonly IEnumerable<Group> groupedRules;

        static FirewallService()
        {
            var firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            var rules = new List<FirewallRuleDto>();

            foreach (INetFwRule3 r in firewallPolicy.Rules)
            {
                rules.Add(new FirewallRuleDto
                {
                    Profile = (ProfileDto)r.Profiles,
                    Name = r.Name,
                    ProgramPath = r.ApplicationName,
                    Direction = (RuleDirectionDto)r.Direction,
                    FwRule = r
                });
            }

            groupedRules = from r in rules
                           orderby r.Name
                           group r by new { r.Profile, r.Direction } into g
                           select new Group
                           {
                               Profile = g.Key.Profile,
                               Direction = g.Key.Direction,
                               Rules = g
                           };
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

    }
}
