using Microsoft.AspNetCore.Authorization;

namespace User.API.Authorization
{
    public class BearerRequirement : IAuthorizationRequirement 
    {
        public const string Name = nameof(BearerRequirement);
    }    
}
