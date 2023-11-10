using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Kambam.API.Authentication;


public interface IJWTManagerRepository
{
    Tokens Authenticate(Users users);
}

public class JWTManagerRepository : IJWTManagerRepository
{
    private readonly IConfiguration _configuration;
    public JWTManagerRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public Tokens Authenticate(Users users)
    {
        var authUserName = _configuration["LoginAuth:User"];
        var authPassword = _configuration["LoginAuth:Password"];

        if (authUserName != users.Login || authPassword != users.Senha)
        {
            return null;
        }

        // Else we generate JSON Web Token
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
          {
             new Claim(ClaimTypes.Name, users.Login)
          }),
            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        };

        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Tokens { Token = tokenHandler.WriteToken(token) };

    }
}