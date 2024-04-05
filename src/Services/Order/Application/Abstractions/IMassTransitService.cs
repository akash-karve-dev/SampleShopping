namespace Order.Application.Abstractions
{
    public interface IMassTransitService
    {
        Task SendAsync<T>(T payload, string queue) where T : class;
    }
}