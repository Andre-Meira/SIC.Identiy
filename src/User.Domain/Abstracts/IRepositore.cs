namespace User.Domain.Abstracts;

public interface IRepository<T> where T : IAggregate
{
    public IUnitOfWork UnitOfWork { get; }

    public void Add(T Entity);
    public Task<T?> Get(decimal id, CancellationToken cancellation = default);
    public IEnumerable<T> GetAll(int itemsPerPage = 10);
}


public interface IUnitOfWork
{
    Task SaveChangesEntity(CancellationToken cancellationToken = default);   
}