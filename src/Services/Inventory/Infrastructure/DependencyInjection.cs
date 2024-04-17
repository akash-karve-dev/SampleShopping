using Inventory.Domain.Inventory;
using Inventory.Domain.Product;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;
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
            });

            return services;
        }
    }
}