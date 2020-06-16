using FirewallWidget.Data;
using FirewallWidget.DataAccess.Contracts.Repositories;
using FirewallWidget.DataAccess.Repositories.EF;
using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.Services;

using Microsoft.Extensions.DependencyInjection;

using System;

namespace FirewallWidget.Manager.Extensions
{
    internal static class ContainerExtensions
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient<IRulesRepository, RulesRepository>()
                .AddTransient<IOptionsRepository, OptionsRepository>();
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IRuleService, RuleService>()
                .AddTransient<IOptionsService, OptionsService>()
                .AddSingleton<IFirewallService, FirewallService>();
        }

        public static IServiceCollection Ensure(this IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            EnsureOptions(provider);

            return services;
        }

        private static void EnsureOptions(IServiceProvider serviceProvider)
        {
            var optionsRepository = serviceProvider.GetRequiredService<IOptionsRepository>();
            var options = optionsRepository.ReadOptions();
            if (options == null)
            {
                optionsRepository.UpdateOptions(new Options
                {
                    OverrideRules = true
                });
            }
        }
    }
}
