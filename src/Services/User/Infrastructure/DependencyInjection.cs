using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Abstractions;
using User.Domain.User;
using User.Infrastructure.Data;
using User.Infrastructure.Repositories;

namespace User.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.AddInterceptors(new SaveChangesAuditableEntityInterceptor());

                options.UseSqlServer("Data Source=UserDb;Initial Catalog=UserDb;Integrated Security=false;User Id=sa;Password=Password1234!;TrustServerCertificate=True", npgsqlOptions =>
                {
                    //npgsqlOptions.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    //npgsqlOptions.MigrationsHistoryTable($"__{nameof(ApplicationDbContext)}");

                    //npgsqlOptions.EnableRetryOnFailure(5);
                });
            });

            /*
             * TODO:
             * Need to find why it is required.
             * If we do not add this background service, it wont create masstransit outbox/inbox tables.
             */
            //services.AddHostedService<RecreateDatabaseHostedService<ApplicationDbContext>>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();

                x.AddEntityFrameworkOutbox<ApplicationDbContext>(o =>
                {
                    o.UseSqlServer();
                    o.UseBusOutbox();

                    o.DuplicateDetectionWindow = TimeSpan.FromSeconds(60);
                });

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("RabbitMqBroker", "/", broker =>
                    {
                        broker.Username("admin");
                        broker.Password("admin");
                    });
                });
            });

            return services;
        }
    }
}