using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Order.Api;
using Order.Application;
using Order.Infrastructure;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            var files = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
            foreach (var file in files)
            {
                c.IncludeXmlComments(file, true);
            }
        });

        builder.Services.AddApplication();
        builder.Services.AddInfrastructure();

        builder.Services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = (context) =>
            {
                context.ProblemDetails.Extensions.TryAdd("trace-id", context.HttpContext.TraceIdentifier);
            };
        });

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