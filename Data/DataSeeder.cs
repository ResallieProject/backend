﻿using Resallie.Models;

namespace Resallie.Data
{
    public class DataSeeder
    {
        private readonly Dictionary<string, Model> ConvertionContext;
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

        public static void Seed(string tableName, int quantity)
        {

        }

    }
}   
