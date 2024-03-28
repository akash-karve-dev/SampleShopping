namespace Order.Application.Dto.Input
{
    public record CreateOrderDto
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
    }
}