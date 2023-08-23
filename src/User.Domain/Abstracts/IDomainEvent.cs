using MediatR;

namespace User.Domain.Abstracts;

public interface IDomainEvent : INotification 
{
    public Guid id { get; }
}
