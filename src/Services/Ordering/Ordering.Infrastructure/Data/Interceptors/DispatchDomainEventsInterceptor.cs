using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediator) : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            DispatchDomainEvents(eventData.Context).GetAwaiter().GetResult();
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public async Task DispatchDomainEvents(DbContext? context)
        {
            var aggregates = context?.ChangeTracker
                .Entries<IAggregate>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToArray();

            var domainEvents = aggregates?
                .SelectMany(e => e.DomainEvents)
                .ToList();

            aggregates?.ToList().ForEach(aggregate => aggregate.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent);
            }

        }
    }
}
