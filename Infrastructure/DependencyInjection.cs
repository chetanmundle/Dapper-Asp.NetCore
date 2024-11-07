﻿
using App.Core.Interfaces;
using Infrastructure.Dapper;
using Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
           IConfigurationManager configuration)
        {
            // DapperService
            services.AddSingleton<DapperContext>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
