using SharedMessage.Entity;

namespace SharedMessage.Commands
{
    public record CreateOrderCommand
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = [];
    }
}