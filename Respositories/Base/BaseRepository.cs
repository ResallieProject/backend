using Resallie.Data;
using Resallie.Models;
using Resallie.Respositories.Interfaces;
using Resallie.Respositories.Users;

namespace Resallie.Respositories.Base
{
    public abstract class BaseRepository : IBaseRepositoy<Model>
    {
        protected AppDbContext _ctx;

        public BaseRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<Model> Create(Model t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual Task<Model?> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}