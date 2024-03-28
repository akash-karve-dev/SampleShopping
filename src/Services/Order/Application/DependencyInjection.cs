using Microsoft.Extensions.DependencyInjection;

namespace Order.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });

            return services;
        }
    }
}