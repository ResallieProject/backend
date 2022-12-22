using Resallie.Data;
using Resallie.Respositories.Base;

namespace Resallie.Respositories.Users
{
    public class UserWishListRepository : BaseRepository
    {
        public UserWishListRepository(AppDbContext ctx) : base(ctx)
        {
        }
    }
}