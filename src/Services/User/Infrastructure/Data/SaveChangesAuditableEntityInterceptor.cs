using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using User.Domain;

namespace User.Infrastructure.Data
{
    public sealed class SaveChangesAuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is not null)
            {
                var context = eventData.Context;

                var entries = context.ChangeTracker.Entries<IAuditableEntity>();

                foreach (var entry in entries)
                {
                    if (entry.State == EntityState.Modified)
                    {
                        entry.Property(nameof(IAuditableEntity.ModifiedAt)).CurrentValue = DateTime.Now;
                    }

                    if (entry.State == EntityState.Added)
                    {
                        entry.Property(nameof(IAuditableEntity.CreatedAt)).CurrentValue = DateTime.Now;
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}