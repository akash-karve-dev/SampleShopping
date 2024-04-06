using MassTransit;
using SharedMessage.Entity;

namespace OrderSaga.Worker.OrderSaga
{
    internal class OrderStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; } = string.Empty;

        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = [];
    }
}