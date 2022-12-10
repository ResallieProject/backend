using Resallie.Data.Dto;
using Resallie.Models;
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
}