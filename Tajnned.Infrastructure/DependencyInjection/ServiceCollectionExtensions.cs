using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Tajnned.Application.Interfaces.Hangfire;
using Tajnned.Application.Interfaces.Services;
using Tajnned.Application.Interfaces.Sql;
using Tajnned.Domain.Interfaces.Repositories;
using Tajnned.Domain.Models;
using Tajnned.Infrastructure.Auth;
using Tajnned.Infrastructure.BackgroundJobs;
using Tajnned.Infrastructure.Data;
using Tajnned.Infrastructure.Repositories;
using Tajnned.Infrastructure.Sql;

namespace Tajnned.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Default")));
            services.Configure<JWT>(configuration.GetSection("JWT"));
          
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserManagementService, UserManagementService>();
            services.AddScoped<ISqlExecutor, SqlExecutor>();
            services.AddScoped<IJobService, JobService>();

            return services;
        }
    }
}
