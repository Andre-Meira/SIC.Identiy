using MediatR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using User.Domain.Enums;

namespace User.Application.Commands;

public record CreateUserCommand : IRequest<Guid>
{
    [EmailAddress]
    [Required(AllowEmptyStrings = false)]    
    public string Email { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = null!;

    public UserTypeEnum UserType { get; set; } 
}

public record DisableUserCommand : IRequest
{
    
    public Guid idUser;

    [Required]
    public string Reason { get; set; } = null!;
}
