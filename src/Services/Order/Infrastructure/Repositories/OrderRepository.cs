using Order.Domain.Order;
using Order.Infrastructure.Data;
using System.Diagnostics.CodeAnalysis;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository(ApplicationDbContext dbContext) : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public async Task CreateOrderAsync([NotNull] Domain.Order.Order order)
        {
            await _dbContext.Orders.AddAsync(order);

            if (order.OrderDetails.Any())
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