namespace Identiy.Domain.Abstracts;

public interface IRepository<T> where T : IAggregate
{
    public IUnitOfWork UnitOfWork { get; }

    public Task Add(T Entity);
    public Task<T> Get(decimal id);
    public IEnumerable<T> GetAll();
}
