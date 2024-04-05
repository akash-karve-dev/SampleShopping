namespace Order.Application.Dto.Input
{
    /// <summary>
    /// 
    /// </summary>
    public record CreateOrderDto
    {
        /// <summary>
        ///
        /// </summary>
        /// <example>43270899-c320-486f-a633-8f0eba7f57ef</example>
        public Guid ProductId { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <example>71479D27-D7A2-4439-99CB-0B536DCEFB45</example>
        public Guid UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <example>1</example>
        public int Quantity { get; set; }
    }
}