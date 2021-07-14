using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FidelityHub.Application.Extensions
{
    public static class DbContextExtensions
    {
        public static void Rollback(this DbContext context)
        {
            foreach (var entry in context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified; //Revert changes made to deleted entity.
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                }
            }
        }

        public static bool IsChangeSuccessful(this DbContext context, int expectedChangeCount)
        {
            return context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).Count() == expectedChangeCount;
        }
    }
}
