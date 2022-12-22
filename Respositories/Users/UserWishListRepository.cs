using Microsoft.EntityFrameworkCore;
using Resallie.Data;
using Resallie.Models;
using Resallie.Respositories.Base;

namespace Resallie.Respositories.Users
{
    public class UserWishListRepository : BaseRepository
    {
        public UserWishListRepository(AppDbContext ctx) : base(ctx)
        {
        }

        internal async Task<List<UserWishList>> GetAllFromThisUser(int userId)
        {
            return await _ctx.UsersWishLists.
                Where(item => item.UserId == userId).ToListAsync();
        }
    }
}