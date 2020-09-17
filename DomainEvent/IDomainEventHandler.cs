using DomainDispatching.DomainEvent;

namespace DomainDispatching.DomainEvent
{
    public interface IDomainEventHandler<TDomainEvent> where TDomainEvent: IDomainEvent
    {
        void Handle(TDomainEvent @domainEvent);
    }
}