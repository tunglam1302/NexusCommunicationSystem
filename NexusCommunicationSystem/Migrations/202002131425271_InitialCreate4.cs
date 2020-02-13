namespace NexusCommunicationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        UserPassword = c.String(),
                        FeedBack = c.String(),
                        UserRole = c.Int(nullable: false),
                        RetailStore_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RetailStores", t => t.RetailStore_Id)
                .Index(t => t.RetailStore_Id);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderStatus = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        SecurityDeposit = c.Single(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        AmountDue = c.Double(nullable: false),
                        Amount = c.Int(nullable: false),
                        NextPaymentAt = c.DateTime(nullable: false),
                        ChargeForReplacementDone = c.Single(nullable: false),
                        Discounts = c.Single(nullable: false),
                        Account_Id = c.Int(),
                        Customer_Id = c.Int(),
                        RetailStore_Id = c.Int(),
                        Service_Id = c.Int(),
                        ServicePackage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Account_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.RetailStores", t => t.RetailStore_Id)
                .ForeignKey("dbo.Services", t => t.Service_Id)
                .ForeignKey("dbo.ServicePackages", t => t.ServicePackage_Id)
                .Index(t => t.Account_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.RetailStore_Id)
                .Index(t => t.Service_Id)
                .Index(t => t.ServicePackage_Id);
            
            CreateTable(
                "dbo.Billings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.Order_Id)
                .Index(t => t.Order_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        UserPassword = c.String(),
                        FeedBack = c.String(),
                        UserRole = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Feedbacks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.RetailStores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Telephone = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Image = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Service_Equipment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Equipment_Id = c.Int(),
                        Service_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.Equipment_Id)
                .ForeignKey("dbo.Services", t => t.Service_Id)
                .Index(t => t.Equipment_Id)
                .Index(t => t.Service_Id);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        Amount = c.Int(nullable: false),
                        Vendor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vendors", t => t.Vendor_Id)
                .Index(t => t.Vendor_Id);
            
            CreateTable(
                "dbo.Vendors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServicePackages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentPeriod = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contracts", "ServicePackage_Id", "dbo.ServicePackages");
            DropForeignKey("dbo.Service_Equipment", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.Equipments", "Vendor_Id", "dbo.Vendors");
            DropForeignKey("dbo.Service_Equipment", "Equipment_Id", "dbo.Equipments");
            DropForeignKey("dbo.Contracts", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.Contracts", "RetailStore_Id", "dbo.RetailStores");
            DropForeignKey("dbo.Accounts", "RetailStore_Id", "dbo.RetailStores");
            DropForeignKey("dbo.Contracts", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Feedbacks", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Billings", "Order_Id", "dbo.Contracts");
            DropForeignKey("dbo.Contracts", "Account_Id", "dbo.Accounts");
            DropIndex("dbo.Equipments", new[] { "Vendor_Id" });
            DropIndex("dbo.Service_Equipment", new[] { "Service_Id" });
            DropIndex("dbo.Service_Equipment", new[] { "Equipment_Id" });
            DropIndex("dbo.Feedbacks", new[] { "Customer_Id" });
            DropIndex("dbo.Billings", new[] { "Order_Id" });
            DropIndex("dbo.Contracts", new[] { "ServicePackage_Id" });
            DropIndex("dbo.Contracts", new[] { "Service_Id" });
            DropIndex("dbo.Contracts", new[] { "RetailStore_Id" });
            DropIndex("dbo.Contracts", new[] { "Customer_Id" });
            DropIndex("dbo.Contracts", new[] { "Account_Id" });
            DropIndex("dbo.Accounts", new[] { "RetailStore_Id" });
            DropTable("dbo.ServicePackages");
            DropTable("dbo.Vendors");
            DropTable("dbo.Equipments");
            DropTable("dbo.Service_Equipment");
            DropTable("dbo.Services");
            DropTable("dbo.RetailStores");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Customers");
            DropTable("dbo.Billings");
            DropTable("dbo.Contracts");
            DropTable("dbo.Accounts");
        }
    }
}
