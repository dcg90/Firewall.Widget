﻿
using AutoMapper;

using FirewallWidget.DataAccess.Contexts;
using FirewallWidget.Manager.Extensions;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace FirewallWidget.Manager
{
    public class Startup
    {
        public void Configure(IServiceCollection services)
        {
            services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddScoped<EFDbContext>()
                .ConfigureRepositories()
                .ConfigureServices();
        }
    }
}