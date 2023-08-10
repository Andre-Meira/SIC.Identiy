namespace Identiy.Domain.Abstracts;

public delegate Task EntityHandler(Entity entity);

public interface IUnitOfWork
{  
    Task SaveChangesEntity();    
}