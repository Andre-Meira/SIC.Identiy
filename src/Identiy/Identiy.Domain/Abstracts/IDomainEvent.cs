using MediatR;

namespace Identiy.Domain.Abstracts;

public interface IDomainEvent : INotification 
{
    public Guid id { get; }
}
