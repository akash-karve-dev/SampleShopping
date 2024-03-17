﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Data;
using User.Infrastructure.Data;

namespace User.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql("Server=UserDb;Port=5432;Database=UserDb;User Id=admin;Password=admin;");
            });

            services.AddScoped<IApplicationDbConext, ApplicationDbContext>();
            return services;
        }
    }
}