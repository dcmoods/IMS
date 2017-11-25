namespace Stock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stock_update : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Stock.StockItems", "Category_CategoryId", "Stock.Categories");
            DropIndex("Stock.StockItems", new[] { "Category_CategoryId" });
            AlterColumn("Stock.StockItems", "Category_CategoryId", c => c.Int());
            CreateIndex("Stock.StockItems", "Category_CategoryId");
            AddForeignKey("Stock.StockItems", "Category_CategoryId", "Stock.Categories", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("Stock.StockItems", "Category_CategoryId", "Stock.Categories");
            DropIndex("Stock.StockItems", new[] { "Category_CategoryId" });
            AlterColumn("Stock.StockItems", "Category_CategoryId", c => c.Int(nullable: false));
            CreateIndex("Stock.StockItems", "Category_CategoryId");
            AddForeignKey("Stock.StockItems", "Category_CategoryId", "Stock.Categories", "CategoryId", cascadeDelete: true);
        }
    }
}
