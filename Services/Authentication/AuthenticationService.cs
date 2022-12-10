using Resallie.Data.Dto;
using Resallie.Models;
using Resallie.Requests.Authentication;
using Resallie.Respositories.Authentication;

namespace Resallie.Services.Authentication;

public class AuthenticationService
{
    private AuthenticationRepository _repository;

    public AuthenticationService(AuthenticationRepository repository)
    {
        _repository = repository;
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
    
    public async Task<UserDto?> AuthenticateUser(string email, string password)
    {
        var user = await _repository.FindUserByEmail(email);
        if (user == null) return null;

        var isValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
        if (!isValid) return null;
        
        // User? user = await _repository.UpdateUserToken(request.Id, request.Token);

        return new UserDto(user);
    }
}