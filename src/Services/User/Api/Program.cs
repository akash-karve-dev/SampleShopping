using User.Api;
using User.Application;
using User.Infrastructure;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = (context) =>
            {
                context.ProblemDetails.Extensions.TryAdd("trace-id", context.HttpContext.TraceIdentifier);
            };
        });

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();

        /*
         * We can chain multiple IExceptionHandler implementations
         * Order matters
         * builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();  - First to execute
         * builder.Services.AddExceptionHandler<GlobalExceptionHandler>();      - Second to execute
         */
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

        var app = builder.Build();

        app.UseExceptionHandler(_ => { });

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}