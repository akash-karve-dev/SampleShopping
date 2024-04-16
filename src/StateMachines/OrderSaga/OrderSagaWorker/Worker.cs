namespace OrderSaga.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}