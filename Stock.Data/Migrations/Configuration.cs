namespace Stock.Data.Migrations
{
    using Stock.Domain;
    using System;
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

            context.StockItems.AddOrUpdate(s => s.StockItemId,
                new StockItem()
                {
                    Category = context.Categories.SingleOrDefault(c => c.CategoryId == 1),
                    Name = "White Rice",
                    Description = "White rice for pilaf",
                    MinimumLevel = 5,
                    MaximumLevel = 40,
                    LevelUnit = "Lbs",
                    Quantity = 30,
                    PricePerUnit = 1.33m,
                    ReceivedDate = DateTime.Today,
                    ExpirationDate = DateTime.Today.AddDays(14),
                },
                new StockItem()
                {
                    Category = context.Categories.SingleOrDefault(c => c.CategoryId == 1),
                    Name = "Brown Rice",
                    Description = "Brown rice for healthy foods",
                    MinimumLevel = 5,
                    MaximumLevel = 40,
                    LevelUnit = "Lbs",
                    Quantity = 30,
                    PricePerUnit = 1.33m,
                    ReceivedDate = DateTime.Today,
                    ExpirationDate = DateTime.Today.AddDays(14),
                },
                new StockItem()
                {
                    Category = context.Categories.SingleOrDefault(c => c.CategoryId == 2),
                    Name = "Vanilla Ice cream",
                    Description = "",
                    MinimumLevel = 5,
                    MaximumLevel = 20,
                    LevelUnit = "Gallons",
                    Quantity = 15,
                    PricePerUnit = 5.02m,
                    ReceivedDate = DateTime.Today,
                    ExpirationDate = DateTime.Today.AddDays(20)
                },
                new StockItem()
                {
                    Category = context.Categories.SingleOrDefault(c => c.CategoryId == 3),
                    Name = "Chicken Breast",
                    Description = "Chicken breast must be properly handled",
                    MinimumLevel = 25,
                    MaximumLevel = 100,
                    LevelUnit = "Lbs",
                    Quantity = 44,
                    PricePerUnit = 1.89m,
                    ReceivedDate = DateTime.Today,
                    ExpirationDate = DateTime.Today.AddDays(7),
                    Temperature = 40
                });
        }
    }
}
