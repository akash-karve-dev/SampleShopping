using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Behaviors;

namespace User.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(m =>
            {
                m.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
                m.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            return services;
        }
    }
}