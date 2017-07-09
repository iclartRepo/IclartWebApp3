namespace IclartWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            RenameTable(name: "dbo.AspNetRoles", newName: "Roles");
            RenameTable(name: "dbo.AspNetUserRoles", newName: "UserRoles");
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
            RenameTable(name: "dbo.AspNetUserClaims", newName: "UserClaims");
            RenameTable(name: "dbo.AspNetUserLogins", newName: "UserLogins");
          
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            AddColumn("dbo.UserRoles", "IdentityUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserClaims", "IdentityUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.UserLogins", "IdentityUser_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.UserClaims", "UserId", c => c.String());
            CreateIndex("dbo.UserRoles", "IdentityUser_Id");
            CreateIndex("dbo.UserClaims", "IdentityUser_Id");
            CreateIndex("dbo.UserLogins", "IdentityUser_Id");
            AddForeignKey("dbo.UserClaims", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserLogins", "IdentityUser_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.UserRoles", "IdentityUser_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "IdentityUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "IdentityUser_Id", "dbo.Users");
            DropIndex("dbo.UserLogins", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserClaims", new[] { "IdentityUser_Id" });
            DropIndex("dbo.UserRoles", new[] { "IdentityUser_Id" });
            AlterColumn("dbo.UserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.UserLogins", "IdentityUser_Id");
            DropColumn("dbo.UserClaims", "IdentityUser_Id");
            DropColumn("dbo.Users", "Discriminator");
            DropColumn("dbo.UserRoles", "IdentityUser_Id");
            CreateIndex("dbo.UserLogins", "UserId");
            CreateIndex("dbo.UserClaims", "UserId");
            CreateIndex("dbo.UserRoles", "UserId");
            AddForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            RenameTable(name: "dbo.UserLogins", newName: "AspNetUserLogins");
            RenameTable(name: "dbo.UserClaims", newName: "AspNetUserClaims");
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
            RenameTable(name: "dbo.UserRoles", newName: "AspNetUserRoles");
            RenameTable(name: "dbo.Roles", newName: "AspNetRoles");
        }
    }
}
