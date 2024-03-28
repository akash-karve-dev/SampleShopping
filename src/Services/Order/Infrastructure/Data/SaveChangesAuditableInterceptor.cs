using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Order.Domain;

namespace Order.Infrastructure.Data
{
    internal class SaveChangesAuditableInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                var context = eventData.Context;

                var entries = context.ChangeTracker.Entries<IAuditableEntity>();

                foreach (var entity in entries)
                {
                    if (entity.State == EntityState.Added)
                    {
                        entity.Property(nameof(IAuditableEntity.CreatedAt)).CurrentValue = DateTime.Now;
                    }

                    if (entity.State == EntityState.Modified)
                    {
                        entity.Property(nameof(IAuditableEntity.ModifiedAt)).CurrentValue = DateTime.Now;
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}