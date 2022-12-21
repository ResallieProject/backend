using Resallie.Data;
using Resallie.Models;
using Resallie.Respositories.Base;
using Resallie.Respositories.Interfaces;

namespace Resallie.Services
{
    public class UserWishListRepository : BaseRepository
    {
        public UserWishListRepository(AppDbContext ctx) : base(ctx)
        {
        }
    }
}