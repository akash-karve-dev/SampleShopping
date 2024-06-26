﻿using Inventory.Domain.Inventory;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories
{
    internal class InventoryRepository(ApplicationDbContext dbContext) : IInventoryRepository
    {
        private readonly ApplicationDbContext _dbContext = dbContext;

        public Task<Guid> CreateAsync(Domain.Inventory.Inventory inventory)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid inventoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<Domain.Inventory.Inventory?> GetByIdAsync(Guid inventoryId)
        {
            return await _dbContext.Inventories.FindAsync(inventoryId);
        }

        public async Task<Domain.Inventory.Inventory?> GetByProductIdAsync(Guid productId)
        {
            return await _dbContext.Inventories.FirstOrDefaultAsync(x => x.ProductId == productId);
        }

        public async Task UpdateAsync(Domain.Inventory.Inventory inventory)
        {
            await Task.CompletedTask;
            _dbContext.Inventories.Update(inventory);
        }
    }
}