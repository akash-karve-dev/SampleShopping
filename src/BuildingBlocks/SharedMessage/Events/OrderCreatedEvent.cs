using SharedMessage.Entity;

namespace SharedMessage.Events
{
    public record OrderCreatedEvent
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = [];
    }

    public record OrderFailedEvent
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = [];
    }

    public record OrderCompletedEvent
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = [];
    }

    public record StockReservedEvent
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = [];
    }

    public record StockReservationFailedEvent
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; } = [];
    }

    public record PaymentCompletedEvent
    {
        public Guid OrderId { get; set; }
    }

    public record PaymentFailedEvent
    {
        public Guid OrderId { get; set; }
    }
}