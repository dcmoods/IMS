namespace Stock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class si_categoryId_added : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Stock.StockItems", "Category_CategoryId", "Stock.Categories");
            DropIndex("Stock.StockItems", new[] { "Category_CategoryId" });
            RenameColumn(table: "Stock.StockItems", name: "Category_CategoryId", newName: "CategoryId");
            AlterColumn("Stock.StockItems", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("Stock.StockItems", "CategoryId");
            AddForeignKey("Stock.StockItems", "CategoryId", "Stock.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Stock.StockItems", "CategoryId", "Stock.Categories");
            DropIndex("Stock.StockItems", new[] { "CategoryId" });
            AlterColumn("Stock.StockItems", "CategoryId", c => c.Int());
            RenameColumn(table: "Stock.StockItems", name: "CategoryId", newName: "Category_CategoryId");
            CreateIndex("Stock.StockItems", "Category_CategoryId");
            AddForeignKey("Stock.StockItems", "Category_CategoryId", "Stock.Categories", "CategoryId");
        }
    }
}
