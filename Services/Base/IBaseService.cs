namespace Resallie.Controllers.Interfaces
{
    internal interface IBaseService<T>
    {
        public Task<T?> Get(int id);
        public Task<bool> Delete(int id);
        public Task<T> Create(T t);
    }
}