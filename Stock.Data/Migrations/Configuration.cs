namespace Stock.Data.Migrations
{
    using Stock.Domain;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Stock.Data.StockContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(Stock.Data.StockContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Categories.AddOrUpdate(c => c.CategoryId,
                new Category() { Name = "Dry Good" },
                new Category() { Name = "Frozen" },
                new Category() { Name = "Cool" });

            var stockItem1 = new StockItem()
            {
                Category = context.Categories.SingleOrDefault(c => c.CategoryId == 1),
                Name = "White Rice",
                Description = "White rice for pilaf",
                MinimumLevel = 5,
                MaximumLevel = 40,
                LevelUnit = "Lbs",
            };
            stockItem1.CreateItemEntry(15, 3.50m, DateTime.Now.AddDays(14));

            var stockItem2 = new StockItem()
            {
                Category = context.Categories.SingleOrDefault(c => c.CategoryId == 1),
                Name = "Brown Rice",
                Description = "Brown rice for healthy foods",
                MinimumLevel = 5,
                MaximumLevel = 40,
                LevelUnit = "Lbs",

            };
            stockItem2.CreateItemEntry(15, 3.00m, DateTime.Now.AddDays(14));

            var stockItem3 = new StockItem()
            {
                Category = context.Categories.SingleOrDefault(c => c.CategoryId == 2),
                Name = "Vanilla Ice cream",
                Description = "",
                MinimumLevel = 5,
                MaximumLevel = 20,
                LevelUnit = "Gallons",
            };
            stockItem3.CreateItemEntry(10, 4.00m, DateTime.Now.AddDays(20), "28f");

            var stockItem4 = new StockItem()
            {
                Category = context.Categories.SingleOrDefault(c => c.CategoryId == 3),
                Name = "Chicken Breast",
                Description = "Chicken breast must be properly handled",
                MinimumLevel = 25,
                MaximumLevel = 100,
                LevelUnit = "Lbs",
            };
            stockItem4.CreateItemEntry(88, 1.98m, DateTime.Now.AddDays(7), "32f");
            context.StockItems.AddOrUpdate(s => s.StockItemId, 
                    stockItem1, 
                    stockItem2,
                    stockItem3,
                    stockItem4
                );
        }
    }
}
