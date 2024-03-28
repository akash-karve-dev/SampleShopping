namespace Order.Domain.Order
{
    public class OrderDetail
    {
        public OrderDetail(Guid id, Guid orderId, Guid productId, int quantity)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        //public static OrderDetail Create(Guid orderId, Guid productId, int quantity)
        //{
        //    return new OrderDetail
        //    {
        //        Id = Guid.NewGuid(),
        //        OrderId = orderId,
        //        ProductId = productId,
        //        Quantity = quantity
        //    };
        //}
    }
}