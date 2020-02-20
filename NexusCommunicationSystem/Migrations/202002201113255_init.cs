namespace NexusCommunicationSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
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
                        UserRole = c.Int(nullable: false),
                        RetailStoreId = c.Int(nullable: false),
                        AccountId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RetailStores", t => t.RetailStoreId, cascadeDelete: true)
                .Index(t => t.RetailStoreId);
            
            CreateTable(
                "dbo.Contracts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderStatus = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        SecurityDeposit = c.Double(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        AmountDue = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        NextPaymentAt = c.DateTime(nullable: false),
                        ChargeForReplacementDone = c.Double(nullable: false),
                        Discounts = c.Double(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.AccountId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.ServiceId)
                .Index(t => t.AccountId);
            
            CreateTable(
                "dbo.Billings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        ContractId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contracts", t => t.ContractId, cascadeDelete: true)
                .Index(t => t.ContractId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        UserPassword = c.String(),
                        UserRole = c.Int(nullable: false),
                        AccountId = c.String(),
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
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Image = c.String(nullable: false),
                        Description = c.String(),
                        TotalAmount = c.Int(nullable: false),
                        ServicePackageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServicePackages", t => t.ServicePackageId, cascadeDelete: true)
                .Index(t => t.ServicePackageId);
            
            CreateTable(
                "dbo.Service_Equipment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        ServiceId = c.Int(nullable: false),
                        EquipmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipments", t => t.EquipmentId, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId)
                .Index(t => t.EquipmentId);
            
            CreateTable(
                "dbo.Equipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        Amount = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Vendors", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId);
            
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
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accounts", "RetailStoreId", "dbo.RetailStores");
            DropForeignKey("dbo.Services", "ServicePackageId", "dbo.ServicePackages");
            DropForeignKey("dbo.Service_Equipment", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Equipments", "VendorId", "dbo.Vendors");
            DropForeignKey("dbo.Service_Equipment", "EquipmentId", "dbo.Equipments");
            DropForeignKey("dbo.Contracts", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Contracts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Feedbacks", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Billings", "ContractId", "dbo.Contracts");
            DropForeignKey("dbo.Contracts", "AccountId", "dbo.Accounts");
            DropIndex("dbo.Equipments", new[] { "VendorId" });
            DropIndex("dbo.Service_Equipment", new[] { "EquipmentId" });
            DropIndex("dbo.Service_Equipment", new[] { "ServiceId" });
            DropIndex("dbo.Services", new[] { "ServicePackageId" });
            DropIndex("dbo.Feedbacks", new[] { "CustomerId" });
            DropIndex("dbo.Billings", new[] { "ContractId" });
            DropIndex("dbo.Contracts", new[] { "AccountId" });
            DropIndex("dbo.Contracts", new[] { "ServiceId" });
            DropIndex("dbo.Contracts", new[] { "CustomerId" });
            DropIndex("dbo.Accounts", new[] { "RetailStoreId" });
            DropTable("dbo.RetailStores");
            DropTable("dbo.ServicePackages");
            DropTable("dbo.Vendors");
            DropTable("dbo.Equipments");
            DropTable("dbo.Service_Equipment");
            DropTable("dbo.Services");
            DropTable("dbo.Feedbacks");
            DropTable("dbo.Customers");
            DropTable("dbo.Billings");
            DropTable("dbo.Contracts");
            DropTable("dbo.Accounts");
        }
    }
}
