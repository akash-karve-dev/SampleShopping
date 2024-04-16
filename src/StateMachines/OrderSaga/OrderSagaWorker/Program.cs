using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderSaga.Worker;
using OrderSaga.Worker.OrderSaga;
using SharedMessage;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        var services = builder.Services;

        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();

            cfg.AddEntityFrameworkOutbox<ApplicationDbContext>(c =>
            {
                c.UseBusOutbox();
                c.UseSqlServer();

                c.DuplicateDetectionWindow = TimeSpan.FromSeconds(60);
            });

            cfg.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>()
               .EntityFrameworkRepository(r =>
               {
                   r.AddDbContext<DbContext, ApplicationDbContext>((provider, builder) =>
                   {
                       builder.UseSqlServer("Data Source=OrderSagaDb;Initial Catalog=OrderSagaDb;Integrated Security=false;User Id=sa;Password=Password1234!;TrustServerCertificate=True", m =>
                       {
                           //m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                           //m.MigrationsHistoryTable($"__{nameof(ApplicationDbContext)}");
                       });
                   });
               });

            cfg.UsingRabbitMq((context, transport) =>
            {
                transport.Host("RabbitMqBroker", "/", m =>
                {
                    m.Username("admin");
                    m.Password("admin");
                });

                transport.ReceiveEndpoint(Constants.CreateOrderCommandQueue, e =>
                {
                    e.ConfigureSaga<OrderStateInstance>(context);
                });
            });
        });

        services.AddDbContext<ApplicationDbContext>(builder =>
        {
            builder.UseSqlServer("Data Source=OrderSagaDb;Initial Catalog=OrderSagaDb;Integrated Security=false;User Id=sa;Password=Password1234!;TrustServerCertificate=True");
        });

        builder.Services.AddHostedService<Worker>();

        var host = builder.Build();

        //using (var scope = host.Services.CreateScope())
        //{
        //    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //    dbContext.Database.EnsureCreated();
        //}

        host.Run();
    }
}