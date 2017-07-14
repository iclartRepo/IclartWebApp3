namespace IclartWebApp.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        TIN = c.String(),
                        Office_Address = c.String(),
                        Warehouse_Address = c.String(),
                        Credit_Limit = c.Double(nullable: false),
                        Dealer = c.Boolean(nullable: false),
                        Accounts_Receivables = c.Double(nullable: false),
                        Credit_Terms = c.String(),
                        Discount_Scheme = c.Double(nullable: false),
                        Agent = c.String(),
                        Contacts_Order = c.String(),
                        Contacts_Accounting = c.String(),
                        Telephone_Number = c.String(),
                        Fax_Number = c.String(),
                        Email = c.String(),
                        Rounded_Payment = c.Boolean(nullable: false),
                        Usual_Ordered_Item = c.String(),
                        Witholding_Tax = c.Double(nullable: false),
                        Vat_Exemption = c.Boolean(nullable: false),
                        Collection_Period = c.String(),
                        Combine_Items = c.Boolean(nullable: false),
                        Overpayment_Counter = c.Double(nullable: false),
                        Created_Date = c.DateTime(nullable: false),
                        Modified_Date = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompetitorDiscountSchemesEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompetitorId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        DiscountScheme = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientEntities", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.CompetitorEntities", t => t.CompetitorId, cascadeDelete: true)
                .Index(t => t.CompetitorId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.CompetitorEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SOSEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        Sos_Date = c.DateTime(nullable: false),
                        Remarks = c.String(),
                        Status = c.Boolean(nullable: false),
                        Pickup = c.Boolean(nullable: false),
                        Exported = c.Boolean(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientEntities", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.SOSCustomEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SOSId = c.Int(nullable: false),
                        ItemDescription = c.String(),
                        Quantity = c.Int(nullable: false),
                        Unit = c.String(),
                        Price = c.Double(nullable: false),
                        QuantityDelivered = c.Int(nullable: false),
                        Discarded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SOSEntities", t => t.SOSId, cascadeDelete: true)
                .Index(t => t.SOSId);
            
            CreateTable(
                "dbo.SOSProductEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SOSId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        QuantityDelivered = c.Int(nullable: false),
                        Discarded = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductEntities", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.SOSEntities", t => t.SOSId, cascadeDelete: true)
                .Index(t => t.SOSId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.ProductEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductCategoryId = c.Int(nullable: false),
                        Name = c.String(),
                        CompanyPrice = c.Double(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategoryEntities", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.CompetitorPricesEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CompetitorId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompetitorEntities", t => t.CompetitorId, cascadeDelete: true)
                .ForeignKey("dbo.ProductEntities", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CompetitorId);
            
            CreateTable(
                "dbo.ProductCategoryEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PasswordExpiryEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ExpiryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SOSProductEntities", "SOSId", "dbo.SOSEntities");
            DropForeignKey("dbo.SOSProductEntities", "ProductId", "dbo.ProductEntities");
            DropForeignKey("dbo.ProductEntities", "ProductCategoryId", "dbo.ProductCategoryEntities");
            DropForeignKey("dbo.CompetitorPricesEntities", "ProductId", "dbo.ProductEntities");
            DropForeignKey("dbo.CompetitorPricesEntities", "CompetitorId", "dbo.CompetitorEntities");
            DropForeignKey("dbo.SOSCustomEntities", "SOSId", "dbo.SOSEntities");
            DropForeignKey("dbo.SOSEntities", "ClientId", "dbo.ClientEntities");
            DropForeignKey("dbo.CompetitorDiscountSchemesEntities", "CompetitorId", "dbo.CompetitorEntities");
            DropForeignKey("dbo.CompetitorDiscountSchemesEntities", "ClientId", "dbo.ClientEntities");
            DropIndex("dbo.CompetitorPricesEntities", new[] { "CompetitorId" });
            DropIndex("dbo.CompetitorPricesEntities", new[] { "ProductId" });
            DropIndex("dbo.ProductEntities", new[] { "ProductCategoryId" });
            DropIndex("dbo.SOSProductEntities", new[] { "ProductId" });
            DropIndex("dbo.SOSProductEntities", new[] { "SOSId" });
            DropIndex("dbo.SOSCustomEntities", new[] { "SOSId" });
            DropIndex("dbo.SOSEntities", new[] { "ClientId" });
            DropIndex("dbo.CompetitorDiscountSchemesEntities", new[] { "ClientId" });
            DropIndex("dbo.CompetitorDiscountSchemesEntities", new[] { "CompetitorId" });
            DropTable("dbo.PasswordExpiryEntities");
            DropTable("dbo.ProductCategoryEntities");
            DropTable("dbo.CompetitorPricesEntities");
            DropTable("dbo.ProductEntities");
            DropTable("dbo.SOSProductEntities");
            DropTable("dbo.SOSCustomEntities");
            DropTable("dbo.SOSEntities");
            DropTable("dbo.CompetitorEntities");
            DropTable("dbo.CompetitorDiscountSchemesEntities");
            DropTable("dbo.ClientEntities");
        }
    }
}
