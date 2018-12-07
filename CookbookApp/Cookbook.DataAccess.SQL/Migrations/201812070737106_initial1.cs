namespace Cookbook.DataAccess.SQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LastName", c => c.String());
            DropColumn("dbo.Users", "LstName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "LstName", c => c.String());
            DropColumn("dbo.Users", "LastName");
        }
    }
}
