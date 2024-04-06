using SharedMessage.Entity;

namespace SharedMessage.Commands
{
    public record CreateOrderCommand
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = [];
    }

    public record RollbackStockCommand
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = [];
    }

    public record CreatePaymentCommand
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = [];
    }
}