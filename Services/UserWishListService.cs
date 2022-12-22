using Resallie.Controllers.Interfaces;
using Resallie.Models;
using Resallie.Respositories.Interfaces;
using Resallie.Respositories.Users;
using Resallie.Services.Base;

namespace Resallie.Services
{
    public class UserWishListService : BaseService
    {
        UserWishListRepository _repository;
        public UserWishListService(IBaseService<UserWishListRepository> repository)
        {
            _repository = (UserWishListRepository)repository;
        }

        public async Task<List<UserWishList>> GetAllFromThisUser(int UserId)
        {
            return await _repository.GetAllFromThisUser(UserId);
        }

        //public UserWishListService(IBaseRepositoy<UserWishList> repository) 
        //    :   base((IBaseRepositoy<Model>)repository)
        //{
        //}

        //public async Task<List<UserWishList>> GetAllFromThisUser(int UserId)
        //{
        //    return await ((UserWishListRepository)_repository)
        //        .GetAllFromThisUser(UserId);
        //}
    }
}