namespace Identiy.Domain.Abstracts;

public interface IRepository<T> where T : IAggregate
{
    IUnitOfWork UnitOfWork { get; }
}
