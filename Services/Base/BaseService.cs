using Resallie.Controllers.Interfaces;
using Resallie.Models;
using Resallie.Respositories.Base;
using Resallie.Respositories.Interfaces;

namespace Resallie.Services.Base
{
    public abstract class BaseService : IBaseService<Model>
    {
        protected BaseRepository _repository;
        public BaseService (IBaseRepositoy<Model> repository)
        {
            _repository = (BaseRepository)repository;
        }

        public Task<Model> Create(Model t)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Model?> Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}