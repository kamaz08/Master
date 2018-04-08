namespace Lista2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changehashmodel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.HashModels", "Test");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HashModels", "Test", c => c.Int(nullable: false));
        }
    }
}
