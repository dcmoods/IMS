namespace Stock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationship_change_Stockitem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Stock.InvoiceItems", "StockItemId", "Stock.StockItems");
            DropIndex("Stock.InvoiceItems", new[] { "StockItemId" });
            RenameColumn(table: "Stock.InvoiceItems", name: "StockItemId", newName: "StockItem_StockItemId");
            AlterColumn("Stock.InvoiceItems", "StockItem_StockItemId", c => c.Int());
            CreateIndex("Stock.InvoiceItems", "StockItem_StockItemId");
            AddForeignKey("Stock.InvoiceItems", "StockItem_StockItemId", "Stock.StockItems", "StockItemId");
        }
        
        public override void Down()
        {
            DropForeignKey("Stock.InvoiceItems", "StockItem_StockItemId", "Stock.StockItems");
            DropIndex("Stock.InvoiceItems", new[] { "StockItem_StockItemId" });
            AlterColumn("Stock.InvoiceItems", "StockItem_StockItemId", c => c.Int(nullable: false));
            RenameColumn(table: "Stock.InvoiceItems", name: "StockItem_StockItemId", newName: "StockItemId");
            CreateIndex("Stock.InvoiceItems", "StockItemId");
            AddForeignKey("Stock.InvoiceItems", "StockItemId", "Stock.StockItems", "StockItemId", cascadeDelete: true);
        }
    }
}
