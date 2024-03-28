namespace Order.Domain.Order
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(Guid id);

        Task CreateOrderAsync(Order order);
    }
}