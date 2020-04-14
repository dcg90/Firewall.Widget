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
