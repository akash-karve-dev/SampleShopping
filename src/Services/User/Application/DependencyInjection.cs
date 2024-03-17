using Microsoft.Extensions.DependencyInjection;

namespace User.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(m =>
            {
                m.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
            });

            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}