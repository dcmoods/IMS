namespace Stock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_tables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Stock.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "Stock.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
            CreateTable(
                "Stock.InvoiceItems",
                c => new
                    {
                        InvoiceItemId = c.Int(nullable: false, identity: true),
                        StockItemId = c.Int(nullable: false),
                        InvoiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceItemId)
                .ForeignKey("Stock.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("Stock.StockItems", t => t.StockItemId, cascadeDelete: true)
                .Index(t => t.StockItemId)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "Stock.Invoices",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InvoiceId);
            
            AddColumn("Stock.StockItems", "ReceivedBy", c => c.Int(nullable: false));
            AddColumn("Stock.StockItems", "UsedBy", c => c.Int(nullable: false));
            AddColumn("Stock.StockItems", "Category_CategoryId", c => c.Int(nullable: false));
            CreateIndex("Stock.StockItems", "Category_CategoryId");
            AddForeignKey("Stock.StockItems", "Category_CategoryId", "Stock.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("Stock.InvoiceItems", "StockItemId", "Stock.StockItems");
            DropForeignKey("Stock.InvoiceItems", "InvoiceId", "Stock.Invoices");
            DropForeignKey("Stock.StockItems", "Category_CategoryId", "Stock.Categories");
            DropIndex("Stock.InvoiceItems", new[] { "InvoiceId" });
            DropIndex("Stock.InvoiceItems", new[] { "StockItemId" });
            DropIndex("Stock.StockItems", new[] { "Category_CategoryId" });
            DropColumn("Stock.StockItems", "Category_CategoryId");
            DropColumn("Stock.StockItems", "UsedBy");
            DropColumn("Stock.StockItems", "ReceivedBy");
            DropTable("Stock.Invoices");
            DropTable("Stock.InvoiceItems");
            DropTable("Stock.Employees");
            DropTable("Stock.Categories");
        }
    }
}
