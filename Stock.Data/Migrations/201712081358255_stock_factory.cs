namespace Stock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stock_factory : DbMigration
    {
        public override void Up()
        {
            DropColumn("Stock.StockItems", "ReceivedBy");
            DropColumn("Stock.StockItems", "UsedBy");
        }
        
        public override void Down()
        {
            AddColumn("Stock.StockItems", "UsedBy", c => c.Int(nullable: false));
            AddColumn("Stock.StockItems", "ReceivedBy", c => c.Int(nullable: false));
        }
    }
}
