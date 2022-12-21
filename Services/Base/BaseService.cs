using Resallie.Controllers.Interfaces;
using Resallie.Models;

namespace Resallie.Services.Base
{
    public abstract class BaseService : IBaseService<Model>
    {
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