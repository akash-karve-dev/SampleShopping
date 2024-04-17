using Inventory.Application.Data;
using Inventory.Domain.Inventory;
using Inventory.Domain.Product;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.MassTransit.Consumers;
using Inventory.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer("Data Source=InventoryDb;Initial Catalog=InventoryDb;Integrated Security=false;User Id=sa;Password=Password1234!;TrustServerCertificate=True");

                options.AddInterceptors(new SaveChangesAuditableEntityInterceptor());
            });

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddMassTransit(bus =>
            {
                bus.SetKebabCaseEndpointNameFormatter();

                bus.AddConsumer<OrderCreatedConsumer>();

                bus.AddEntityFrameworkOutbox<ApplicationDbContext>(options =>
                {
                    options.UseBusOutbox();
                    options.UseSqlServer();

                    options.DuplicateDetectionWindow = TimeSpan.FromSeconds(60);
                });

                bus.UsingRabbitMq((context, transport) =>
                {
                    transport.Host("RabbitMqBroker", "/", c =>
                    {
                        c.Username("admin");
                        c.Password("admin");
                    });

                    transport.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}