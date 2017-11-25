namespace Order.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class od_lineitems : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Order.InvoiceItems", "InvoiceId", "Order.Invoices");
            DropForeignKey("Order.InvoiceItems", "StockItem_StockItemId", "Order.StockItems");
            DropIndex("Order.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("Order.InvoiceItems", new[] { "StockItem_StockItemId" });
            CreateTable(
                "Order.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.OrderId);
            
            CreateTable(
                "Order.LineItems",
                c => new
                    {
                        InvoiceItemId = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        StockItemId = c.Int(nullable: false),
                        Quantity = c.Double(),
                        PricePerUnit = c.Decimal(precision: 18, scale: 2),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceItemId)
                .ForeignKey("Order.Orders", t => t.Order_OrderId)
                .Index(t => t.Order_OrderId);
            
            DropTable("Order.Invoices");
            DropTable("Order.InvoiceItems");
            DropTable("Order.StockItems");
        }
        
        public override void Down()
        {
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
            
            CreateTable(
                "Order.InvoiceItems",
                c => new
                    {
                        InvoiceItemId = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        StockItem_StockItemId = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceItemId);
            
            CreateTable(
                "Order.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId);
            
            DropForeignKey("Order.LineItems", "Order_OrderId", "Order.Orders");
            DropIndex("Order.LineItems", new[] { "Order_OrderId" });
            DropTable("Order.LineItems");
            DropTable("Order.Orders");
            CreateIndex("Order.InvoiceItems", "StockItem_StockItemId");
            CreateIndex("Order.InvoiceItems", "InvoiceId");
            AddForeignKey("Order.InvoiceItems", "StockItem_StockItemId", "Order.StockItems", "StockItemId");
            AddForeignKey("Order.InvoiceItems", "InvoiceId", "Order.Invoices", "InvoiceId", cascadeDelete: true);
        }
    }
}
