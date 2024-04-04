namespace Order.Application.Dto.Output
{
    public record OrderStatusDto
    {
        public Guid OrderId { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}