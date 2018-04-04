namespace Lista2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HashModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HashEnum = c.Int(nullable: false),
                        Test = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Hash = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.HashModels");
        }
    }
}
