using Resallie.Data;
using Resallie.Models;
using Resallie.Respositories.Base;

namespace Resallie
{
    public abstract class AbstractObj
    { }
        
    public class Obj : AbstractObj
    { }

    public interface IBase <T>
    {
        public Task<T> Get(int id);
    }

    public abstract class Base : IBase<AbstractObj>
    {
        AppDbContext _ctx;
        public Base(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<AbstractObj> Get(int id)
        {
            throw new NotImplementedException();
        }

        //public virtual AbstractObj Get(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //async Task<AbstractObj> IBase<AbstractObj>.Get(int id)


    }

    public class TestObj : Base
    {
        public TestObj(AppDbContext ctx) : base(ctx)
        {
        }

        //public override AbstractObj Get(int id)
        //{
        //    return new Obj();
        //}





        //public TestObj(AppDbContext ctx) : base(ctx)
        //{
        //}

        //public override Task<Model?> Get(int id)
        //{
        //    return new Advertisement();
        //}
    }
}