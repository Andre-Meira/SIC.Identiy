using System.ComponentModel.DataAnnotations.Schema;

namespace User.Domain.Abstracts;

public abstract record ValueObject 
{
    public abstract void Validate();
}
