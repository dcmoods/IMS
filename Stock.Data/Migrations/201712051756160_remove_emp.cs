namespace Stock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_emp : DbMigration
    {
        public override void Up()
        {
            DropTable("Stock.Employees");
        }
        
        public override void Down()
        {
            CreateTable(
                "Stock.Employees",
                c => new
                    {
                        EmployeeId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeId);
            
        }
    }
}
