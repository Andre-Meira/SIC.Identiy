namespace User.Domain.Abstracts;

public interface IUnitOfWork
{  
    Task SaveChangesEntity();    
}