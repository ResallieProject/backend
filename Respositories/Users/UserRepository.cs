using Microsoft.EntityFrameworkCore;
using Resallie.Data;
using Resallie.Models;

namespace Resallie.Respositories.Users;

public class UserRepository
{
    private AppDbContext _ctx;

    public UserRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<User> Create(User user)
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