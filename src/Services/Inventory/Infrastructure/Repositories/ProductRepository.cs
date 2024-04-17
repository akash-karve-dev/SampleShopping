using Inventory.Domain.Product;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories
{
    internal class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public Task<Guid> CreateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> GetByIdAsync(Guid productId)
        {
            return await _dbContext.Products.FindAsync(productId);
        }

        public async Task UpdateAsync(Product product)
        {
            await Task.CompletedTask;
            _dbContext.Products.Update(product);
        }
    }
}