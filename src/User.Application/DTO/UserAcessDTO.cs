using User.Domain.AgregattesModel.UserAgregattes;

namespace User.Application.DTO;

public class UserAcessDTO
{
    public UserAcessDTO(
        Guid id, 
        string name, 
        string email, 
        string status, 
        string userType,
        DateTime createdDate)
    {
        Id = id;
        Name = name;
        Email = email;
        Status = status;
        UserType = userType;
        CreatedDate = createdDate;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }

    public string UserType { get; set; }
    public DateTime CreatedDate { get; set; }

    public static UserAcessDTO ToDto(UserAcess userAcess)
    {
        return new UserAcessDTO(userAcess.Id, userAcess.Name, userAcess.Email.Value, userAcess.Status.ToString(),
            userAcess.UserType.ToString(), userAcess.DtCreation);
    }
}
