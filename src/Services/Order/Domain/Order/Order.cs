namespace Order.Domain.Order
{
    public class Order : IAuditableEntity
    {
        public Guid Id { get; set; }

        public string Status { get; set; } = string.Empty;

        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public List<OrderDetail> OrderDetails = [];

        public static Order Create(Guid userId)
        {
            var orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                Status = "Accepted",
                UserId = userId
            };

            return order;
        }

        public void AddOrderDetail(Guid productId, int quantity)
        {
            OrderDetails.Add(new OrderDetail(Guid.NewGuid(), Id, productId, quantity));
        }
    }
}