namespace Resallie.Controllers.Interfaces
{
    internal interface IBaseService<T>
    {
        public Task Get(int id);
        public Task Create(int userId);
    }
}