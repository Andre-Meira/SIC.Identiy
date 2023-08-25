using MediatR;
using System.ComponentModel.DataAnnotations;
using User.Domain.Enums;

namespace User.Application.Commands;

public class CreateUserCommand : IRequest
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
