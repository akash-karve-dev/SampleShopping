using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Data;
using Order.Domain.Order;
using Order.Infrastructure.Data;
using Order.Infrastructure.Repositories;

namespace Order.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.AddInterceptors(new SaveChangesAuditableInterceptor());

                options.UseSqlServer("Data Source=OrderDb;Initial Catalog=OrderDb;Integrated Security=false;User Id=sa;Password=Password1234!;TrustServerCertificate=True", sqlOptions =>
                {
                });
            });

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetService<ApplicationDbContext>()!);

            return services;
        }
    }
}