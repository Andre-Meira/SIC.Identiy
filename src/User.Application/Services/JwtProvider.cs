using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace User.Application.Services;

public class JwtProvider
{
    public JwtProvider(
        Guid id,
        string userName,
        string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }

    protected JwtProvider(string? name = null, 
        string? email = null)
    {        
        Id = Guid.Empty;
        UserName = name;
        Email = email;
    }

    public bool IsAuthenticate { get; private set; }

    public Guid Id { get; private set; }

    public string? UserName { get; private set; }

    public string? Email { get; private set; }
    
    public string GenerateToken()
    {
        if (UserName is null  && Email is null) 
            throw new ArgumentNullException();  

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new Claim(ClaimTypes.Name, UserName!),
                    new Claim(ClaimTypes.Email, Email!),
                    new Claim(ClaimTypes.Sid, Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),            
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
            SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static JwtProvider GetToken(ClaimsPrincipal user)
    {
        string? nome = user.Identity!.Name;
        string? email = user.FindFirst(ClaimTypes.Email)?.Value;
        string? id = user.FindFirst(ClaimTypes.Sid)?.Value;

        if (nome == null && email == null && id == null)
        {
            JwtProvider token = new JwtProvider(null, null);
            token.IsAuthenticate = false;
            return token;
        }
        return new JwtProvider(Guid.Parse(id!), nome!, email!) { IsAuthenticate = true};
    }
}
