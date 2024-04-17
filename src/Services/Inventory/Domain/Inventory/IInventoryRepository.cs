namespace Inventory.Domain.Inventory
{
    public interface IInventoryRepository
    {
        Task<Guid> CreateAsync(Inventory inventory);

        Task UpdateAsync(Inventory inventory);

        Task DeleteAsync(Guid inventoryId);

        Task<Inventory?> GetByIdAsync(Guid inventoryId);

        Task<Inventory?> GetByProductIdAsync(Guid productId);
    }
}