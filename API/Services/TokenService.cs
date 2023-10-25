using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace API.Services;


public class TokenService : ItokenService
{

private readonly SymmetricSecurityKey _key;
    public TokenService(IConfiguration config)
    {
//_key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
  _key = GenerateSecureKey(64);
    }
       private SymmetricSecurityKey GenerateSecureKey(int sizeInBytes)
    {
        var randomNumberGenerator = RandomNumberGenerator.Create();
        var keyBytes = new byte[sizeInBytes];
        randomNumberGenerator.GetBytes(keyBytes);
        return new SymmetricSecurityKey(keyBytes);
    }
    public string CreateToken(AppUser user)
    {
var claims=new List<Claim>
{
    new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
};

var creds=new SigningCredentials(_key,SecurityAlgorithms.HmacSha512);

var tokenDescriptor=new SecurityTokenDescriptor
{
    Subject=new ClaimsIdentity(claims),

    Expires=DateTime.Now.AddDays(7),
    SigningCredentials=creds
};


var tokenHandler=new JwtSecurityTokenHandler();

var token=tokenHandler.CreateToken(tokenDescriptor);

return tokenHandler.WriteToken(token);




    }
}