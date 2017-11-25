namespace Order.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class od_init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Order.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId);
            
            CreateTable(
                "Order.InvoiceItems",
                c => new
                    {
                        InvoiceItemId = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        StockItem_StockItemId = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceItemId)
                .ForeignKey("Order.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("Order.StockItems", t => t.StockItem_StockItemId)
                .Index(t => t.InvoiceId)
                .Index(t => t.StockItem_StockItemId);
            
            CreateTable(
                "Order.StockItems",
                c => new
                    {
                        StockItemId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PricePerUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.StockItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Order.InvoiceItems", "StockItem_StockItemId", "Order.StockItems");
            DropForeignKey("Order.InvoiceItems", "InvoiceId", "Order.Invoices");
            DropIndex("Order.InvoiceItems", new[] { "StockItem_StockItemId" });
            DropIndex("Order.InvoiceItems", new[] { "InvoiceId" });
            DropTable("Order.StockItems");
            DropTable("Order.InvoiceItems");
            DropTable("Order.Invoices");
        }
    }
}
