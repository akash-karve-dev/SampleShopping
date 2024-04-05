namespace SharedMessage.Entity
{
    public record OrderDetail
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}