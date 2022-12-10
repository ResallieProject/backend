using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Resallie.Data.Dto;
using Resallie.Models;
using Resallie.Requests.Authentication;
using Resallie.Respositories.Authentication;

namespace Resallie.Services.Authentication;

public class AuthenticationService
{
    private AuthenticationRepository _repository;
    private IConfiguration _config;

    public AuthenticationService(AuthenticationRepository repository, IConfiguration config)
    {
        _repository = repository;
        _config = config;
    }

    public async Task<UserDto?> RegisterUser(User user)
    {
        var exists = await _repository.FindUserByEmail(user.Email);
        if (exists != null) return null;

        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);

        var dto = new UserDto(
            await _repository.RegisterUser(user)
        );

        return dto;
    }
    
    public async Task<string?> AuthenticateUser(string email, string password)
    {
        var user = await _repository.FindUserByEmail(email);
        if (user == null) return null;

        var isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
        if (!isValid) return null;
        
        var issuer = _config["Jwt:Issuer"];
        var audience = _config["Jwt:Audience"];
        var key = Encoding.ASCII.GetBytes
            (_config["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName + " " + user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(60 * 24), // expires in 1 day
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);

        return stringToken;
    }
}