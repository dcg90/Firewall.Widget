using FirewallWidget.DataAccess.Contracts.Repositories;
using FirewallWidget.DataAccess.Repositories.EF;
using FirewallWidget.Manager.Contracts.Services;
using FirewallWidget.Manager.Services;

using Microsoft.Extensions.DependencyInjection;

namespace FirewallWidget.Manager.Extensions
{
    internal static class ContainerExtensions
    {
        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped<IRulesRepository, RulesRepository>();
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IRuleService, RuleService>()
                .AddSingleton<IFirewallService, FirewallService>();
        }
    }
}
