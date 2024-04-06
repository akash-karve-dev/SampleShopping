using Order.Domain.Order;
using Order.Infrastructure.Data;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository(ApplicationDbContext dbContext) : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task CreateOrderAsync(Domain.Order.Order order)
        {
            await _dbContext.Orders.AddAsync(order);

            if (order.OrderDetails.Count != 0)
            {
                await _dbContext.OrderDetails.AddRangeAsync(order.OrderDetails);
            }
        }

        public async Task<Domain.Order.Order?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders.FindAsync(id);
        }
    }
}