namespace Stock.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class si_update_2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Stock.ItemEntries", "Temperature", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("Stock.ItemEntries", "Temperature", c => c.Double());
        }
    }
}
