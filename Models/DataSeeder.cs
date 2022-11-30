using Resallie.Data;
using Bogus;

namespace Resallie.Models
{
    public class DataSeeder
    {
        private Dictionary<string, Model> ConvertionContext;
        private readonly AppDbContext appDbContext;

        public DataSeeder(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;

            ConvertionContext = new Dictionary<string, Model>()
            {
                {"categories", new Category()},
                {"advertisements", new Advertisement()}
            };
        }

        public void Seed(string categoryname, int quatity)
        {
            if(!ConvertionContext.ContainsKey(categoryname))
            {
                return;
            }
            
           ConvertionContext[categoryname].Seed(appDbContext, quatity);
        }
    }
}
