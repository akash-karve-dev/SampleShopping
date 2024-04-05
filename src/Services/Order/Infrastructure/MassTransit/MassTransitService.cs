using MassTransit;
using Order.Application.Abstractions;

namespace Order.Infrastructure.MassTransit
{
    internal class MassTransitService(ISendEndpointProvider sendEndpointProvider) : IMassTransitService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider = sendEndpointProvider;

        public async Task SendAsync<T>(T payload, string queue) where T : class
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queue}"));
            await sendEndpoint.Send(payload);
        }
    }
}