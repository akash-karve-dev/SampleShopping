using SharedKernel.ResultPattern;

namespace Order.Domain.Order
{
    public class OrderErrors
    {
        public static Error NotFound(Guid orderId) => new("Order.NotFound", $"Order with Id: {orderId} not found.");
    }
}