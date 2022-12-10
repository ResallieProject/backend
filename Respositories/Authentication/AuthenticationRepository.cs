using Microsoft.EntityFrameworkCore;
using Resallie.Data;
using Resallie.Models;

namespace Resallie.Respositories.Authentication;

public class AuthenticationRepository
{
    private AppDbContext _ctx;

    public AuthenticationRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<User> RegisterUser(User user)
    {
        await _ctx.Users.AddAsync(user);
        await _ctx.SaveChangesAsync();
        
        return user;
    }
    
    public async Task<User?> FindUserByEmail(string email)
    {
        return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}