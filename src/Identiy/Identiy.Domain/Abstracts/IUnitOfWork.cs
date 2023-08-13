namespace Identiy.Domain.Abstracts;

public interface IUnitOfWork
{  
    Task SaveChangesEntity();    
}