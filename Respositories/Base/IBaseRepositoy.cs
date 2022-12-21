
namespace Resallie.Respositories.Interfaces
{
    public interface IBaseRepositoy<T>
    {
        public Task<T?> Get(int id);
        public Task<bool> Delete(int id);
        public Task<T> Create(T t);
    }
}
