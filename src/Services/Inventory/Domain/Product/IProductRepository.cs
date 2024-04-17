namespace Inventory.Domain.Product
{
    public interface IProductRepository
    {
        Task<Guid> CreateAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Guid productId);

        Task<Product?> GetByIdAsync(Guid productId);
    }
}