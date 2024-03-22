namespace Sample.Worker;

using MassTransit;
using MassTransit.RetryPolicies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class RecreateDatabaseHostedService<TDbContext>(
    IServiceScopeFactory scopeFactory,
    ILogger<RecreateDatabaseHostedService<TDbContext>> logger) :
    IHostedService
    where TDbContext : DbContext
{
    private readonly ILogger<RecreateDatabaseHostedService<TDbContext>> _logger = logger;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    private TDbContext? _context;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Applying migrations for {DbContext}", TypeCache<TDbContext>.ShortName);

        await Retry.Interval(20, TimeSpan.FromSeconds(3)).Retry(async () =>
        {
            var scope = _scopeFactory.CreateScope();

            try
            {
                _context = scope.ServiceProvider.GetRequiredService<TDbContext>();

                await _context.Database.EnsureDeletedAsync(cancellationToken);
                await _context.Database.EnsureCreatedAsync(cancellationToken);

                _logger.LogInformation("Migrations completed for {DbContext}", TypeCache<TDbContext>.ShortName);
            }
            finally
            {
                if (scope is IAsyncDisposable asyncDisposable)
                    await asyncDisposable.DisposeAsync();
                else
                    scope.Dispose();
            }
        }, cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}