namespace Stock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockItemTemp : DbMigration
    {
        public override void Up()
        {
            AddColumn("Stock.StockItems", "Temperature", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Stock.StockItems", "Temperature");
        }
    }
}
