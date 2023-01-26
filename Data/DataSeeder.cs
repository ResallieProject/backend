using Bogus;
using Resallie.Models;


namespace Resallie.Data
{
    public class DataSeeder
    {
        private readonly AppDbContext appDbContext;

        public DataSeeder(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public void Seed(string tableName, int quantity)
        {
            switch (tableName)
            {
                case "Category":
                case "category":
                case "Categories":
                case "categories":
                    for (int i = 0; i < quantity; i++)
                    {
                        appDbContext.Categories.Add(new Faker<Category>()
                        .RuleFor(m => m.Name, f => f.Commerce.Product())
                        .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
                        .RuleFor(m => m.CreatedAt, f => DateTime.Now));
                    }
                    break;

                case "Advertisement":
                case "advertisement":
                case "Advertisements":
                case "advertisements":
                    {
                        for (int i = 0; i < quantity; i++)
                        {
                            appDbContext.Advertisements.Add(new Faker<Advertisement>()
                        .RuleFor(m => m.Title, f => f.Commerce.ProductName())
                        .RuleFor(m => m.Defects, f => f.Music.Genre())
                        .RuleFor(m => m.Description, f => f.Commerce.ProductDescription())
                        .RuleFor(m => m.CategoryId, f => 1)
                        .RuleFor(m => m.UserId, f => 1)
                        .RuleFor(m => m.CreatedAt, f => DateTime.Now));
                        }
                    }
                    break;

                case "User":
                case "user":
                case "Users":
                case "users":
                    {
                        for (int i = 0; i < quantity; i++)
                        {
                            appDbContext.Users.Add(new Faker<User>()
                        .RuleFor(m => m.FirstName, f => f.Person.FirstName)
                        .RuleFor(m => m.LastName, f => f.Person.LastName)
                        .RuleFor(m => m.Email, f => f.Person.Email)
                        .RuleFor(m => m.Password, f => f.Date.ToString())
                        .RuleFor(m => m.Gender, f => f.Person.Gender.ToString())
                        .RuleFor(m => m.Phone, f => f.Person.Phone)
                        .RuleFor(m => m.CreatedAt, f => DateTime.Now));
                        }
                    }
                    break;
            }
            appDbContext.SaveChanges();
        }
    }
}