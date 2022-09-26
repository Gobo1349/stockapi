using MediatR;
using stockapi.Domain.Events;

namespace stockApi.Infrastructure.Handlers;

public class ReachedMinimumDomainEventHandler : INotificationHandler<ReachMinimumDomainEvent>
{
    public Task Handle(ReachMinimumDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
}