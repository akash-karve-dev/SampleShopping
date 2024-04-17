using Inventory.Domain;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Inventory.Infrastructure.Data
{
    internal class SaveChangesAuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var context = eventData.Context;

            if (context != null)
            {
                foreach (var item in context.ChangeTracker.Entries<IAuditableEntity>())
                {
                    if (item.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                    {
                        item.Property(c => c.CreatedAt).CurrentValue = DateTime.Now;
                    }
                    if (item.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                    {
                        item.Property(c => c.ModifiedAt).CurrentValue = DateTime.Now;
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}