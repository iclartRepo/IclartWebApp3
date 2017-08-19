namespace IclartWebApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategoryInCustomEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SOSCustomEntities", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SOSCustomEntities", "Category");
        }
    }
}
