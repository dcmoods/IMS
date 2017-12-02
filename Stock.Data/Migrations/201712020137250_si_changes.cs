namespace Stock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class si_changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Stock.InvoiceItems", "InvoiceId", "Stock.Invoices");
            DropForeignKey("Stock.InvoiceItems", "StockItem_StockItemId", "Stock.StockItems");
            DropIndex("Stock.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("Stock.InvoiceItems", new[] { "StockItem_StockItemId" });
            CreateTable(
                "Stock.ItemEntries",
                c => new
                    {
                        ItemEntryId = c.Int(nullable: false, identity: true),
                        StockItemId = c.Int(nullable: false),
                        ReceivedDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        Quantity = c.Double(nullable: false),
                        PricePerUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Temperature = c.Double(),
                    })
                .PrimaryKey(t => t.ItemEntryId)
                .ForeignKey("Stock.StockItems", t => t.StockItemId, cascadeDelete: true)
                .Index(t => t.StockItemId);
            
            DropColumn("Stock.StockItems", "PricePerUnit");
            DropColumn("Stock.StockItems", "ReceivedDate");
            DropColumn("Stock.StockItems", "ExpirationDate");
            DropColumn("Stock.StockItems", "Quantity");
            DropColumn("Stock.StockItems", "Temperature");
            DropTable("Stock.InvoiceItems");
            DropTable("Stock.Invoices");
        }
        
        public override void Down()
        {
            CreateTable(
                "Stock.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId);
            
            CreateTable(
                "Stock.InvoiceItems",
                c => new
                    {
                        InvoiceItemId = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        StockItem_StockItemId = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceItemId);
            
            AddColumn("Stock.StockItems", "Temperature", c => c.Double(nullable: false));
            AddColumn("Stock.StockItems", "Quantity", c => c.Double(nullable: false));
            AddColumn("Stock.StockItems", "ExpirationDate", c => c.DateTime(nullable: false));
            AddColumn("Stock.StockItems", "ReceivedDate", c => c.DateTime(nullable: false));
            AddColumn("Stock.StockItems", "PricePerUnit", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropForeignKey("Stock.ItemEntries", "StockItemId", "Stock.StockItems");
            DropIndex("Stock.ItemEntries", new[] { "StockItemId" });
            DropTable("Stock.ItemEntries");
            CreateIndex("Stock.InvoiceItems", "StockItem_StockItemId");
            CreateIndex("Stock.InvoiceItems", "InvoiceId");
            AddForeignKey("Stock.InvoiceItems", "StockItem_StockItemId", "Stock.StockItems", "StockItemId");
            AddForeignKey("Stock.InvoiceItems", "InvoiceId", "Stock.Invoices", "InvoiceId", cascadeDelete: true);
        }
    }
}
