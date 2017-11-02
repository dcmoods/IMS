namespace Stock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Stock.StockItems",
                c => new
                    {
                        StockItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        MinimumLevel = c.Double(nullable: false),
                        MaximumLevel = c.Double(nullable: false),
                        LevelUnit = c.String(),
                        PricePerUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ReceivedDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StockItemId);
            
        }
        
        public override void Down()
        {
            DropTable("Stock.StockItems");
        }
    }
}
