using NexusCommunicationSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace NexusCommunicationSystem.Migrations
{


    internal sealed class Configuration : DbMigrationsConfiguration<NexusCommunicationSystem.Models.NexusCommunicationSystemContext>
    {
        List<Account> listAccount = new List<Account>();
        List<Billing> listBilling = new List<Billing>();
        List<Customer> listCustomer = new List<Customer>();
        List<Equipment> listEquipment = new List<Equipment>();
        List<Feedback> listFeedback = new List<Feedback>();
        List<Contract> listContract = new List<Contract>();
        List<RetailStore> listRetailStore = new List<RetailStore>();
        List<Service> listService = new List<Service>();
        List<Service_Equipment> listService_Equipment = new List<Service_Equipment>();
        List<ServicePackage> listServicePackage = new List<ServicePackage>();
        List<Vendor> listVendor = new List<Vendor>();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NexusCommunicationSystem.Models.NexusCommunicationSystemContext context)
        {
            listVendor.Add(new Vendor("Mobifone", "Nhan Chinh, Hanoi"));
            listVendor.Add(new Vendor("FPT", "Duy Tan, Hanoi"));
            listVendor.Add(new Vendor("Viettel", "Kim Ma, Hanoi"));

            context.Vendors.AddRange(listVendor);
            context.SaveChanges();

            listEquipment.Add(new Equipment("router", 10000, 5, context.Vendors.Where(v => v.Id == 1).Single()));
            listEquipment.Add(new Equipment("modem", 20000, 2, context.Vendors.Where(v => v.Id == 2).Single()));
            listEquipment.Add(new Equipment("labor", 30000, 3, context.Vendors.Where(v => v.Id == 3).Single()));

            context.Equipments.AddRange(listEquipment);
            context.SaveChanges();

            listServicePackage.Add(new ServicePackage("Monthly"));
            listServicePackage.Add(new ServicePackage("Yearly"));
            listServicePackage.Add(new ServicePackage("HalfYearly"));
            listServicePackage.Add(new ServicePackage("Quaterly"));
            listServicePackage.Add(new ServicePackage("HourlyBasis10"));
            listServicePackage.Add(new ServicePackage("HourlyBasis30"));
            listServicePackage.Add(new ServicePackage("HourlyBasis60"));

            context.ServicePackages.AddRange(listServicePackage);
            context.SaveChanges();


            //Telephone Service Group: Landline
            listService.Add(new Service("Landline Local 75",      "https://res.cloudinary.com/truonghusk17/image/upload/v1582201922/nyffp4guir6zey2ipvoh.jpg", 500000,  "Unlimited 75$ for an year/call charges 55cents per min/Free setup Fee/Online support 247"));
            listService.Add(new Service("Landline Local 35",      "https://res.cloudinary.com/truonghusk17/image/upload/v1582201922/nyffp4guir6zey2ipvoh.jpg", 400000,  "Monthly 125$/Local 70cents, STD 2.25$ per min/Free setup Fee/Online support 247"));
            listService.Add(new Service("Landline STD 125",       "https://res.cloudinary.com/truonghusk17/image/upload/v1582201922/nyffp4guir6zey2ipvoh.jpg", 600000,  "Monthly 125$/Local 70cents, STD 2.25$ per min/Free setup Fee/Online support 247"));
            listService.Add(new Service("Landline STD 420",       "https://res.cloudinary.com/truonghusk17/image/upload/v1582201922/nyffp4guir6zey2ipvoh.jpg", 700000,  "Half yearly 420$/Local 60cents, STD 2.00$ per min/Free setup Fee/Online support 247"));
            listService.Add(new Service("Landline STD Yearly",    "https://res.cloudinary.com/truonghusk17/image/upload/v1582201922/nyffp4guir6zey2ipvoh.jpg", 900000,  "Yearly 840$/Validity/Free setup Fee/Online support 247"));
                                                                                                                                                             
            //Internet Service Group: Dial-up                                                                                                                
            listService.Add(new Service("Dialup Hourly",          "https://res.cloudinary.com/truonghusk17/image/upload/v1582201922/nyffp4guir6zey2ipvoh.jpg", 800000,  "Pay per hour used/Free setup Fee/Free Modem or Router/Online support 247"));
            listService.Add(new Service("Dialup Unlimited 28",    "https://res.cloudinary.com/truonghusk17/image/upload/v1582201922/nyffp4guir6zey2ipvoh.jpg", 300000,  "Unlimited 28Kbps/Free setup Fee/Free Modem or Router/Online support 247"));
            listService.Add(new Service("Dialup Unlimited 56",    "https://res.cloudinary.com/truonghusk17/image/upload/v1582201922/nyffp4guir6zey2ipvoh.jpg", 200000,  "Unlimited 56Kbps/Free setup Fee/Free Modem or Router/Online support 247"));
                                                                                                                                                               
            //Internet Service Group: Broadband                                                                                                                
            listService.Add(new Service("Broadband Hourly",       "https://res.cloudinary.com/truonghusk17/image/upload/v1582201922/nyffp4guir6zey2ipvoh.jpg", 100000,  "Pay per hour used/Validity is for 1 or 6 month/Free setup Fee/Free Modem or Router/Online support 247"));
            listService.Add(new Service("Broadband Unlimited 64", "https://res.cloudinary.com/truonghusk17/image/upload/v1582201922/nyffp4guir6zey2ipvoh.jpg", 1500000,  "Unlimited 64Kbps/Free setup Fee/Free Modem or Router/Online support 247"));
            listService.Add(new Service("Broadband Unlimited 128", "https://res.cloudinary.com/truonghusk17/image/upload/v1582201922/nyffp4guir6zey2ipvoh.jpg", 2000000,  "Unlimited 128Kbps/Free setup Fee/Free Modem or Router/Online support 247"));

            context.Services.AddRange(listService);
            context.SaveChanges();

            listService_Equipment.Add(new Service_Equipment(2, context.Services.Where(s => s.Id == 1).Single(), context.Equipments.Where(e => e.Id == 3).Single()));
            listService_Equipment.Add(new Service_Equipment(1, context.Services.Where(s => s.Id == 2).Single(), context.Equipments.Where(e => e.Id == 2).Single()));
            listService_Equipment.Add(new Service_Equipment(3, context.Services.Where(s => s.Id == 3).Single(), context.Equipments.Where(e => e.Id == 1).Single()));
            listService_Equipment.Add(new Service_Equipment(4, context.Services.Where(s => s.Id == 1).Single(), context.Equipments.Where(e => e.Id == 3).Single()));

            context.Service_Equipments.AddRange(listService_Equipment);
            context.SaveChanges();

            listRetailStore.Add(new RetailStore("FPT", "Duy Tan", "023467555"));
            listRetailStore.Add(new RetailStore("Viettel", "Kim Ma", "023467556"));
            listRetailStore.Add(new RetailStore("Mobifone", "Nhan Chinh", "023467557"));

            context.RetailStores.AddRange(listRetailStore);
            context.SaveChanges();

            listAccount.Add(new Account(context.RetailStores.Where(r => r.Id == 1).Single(), "Chung", "Tran", "chung@gmail.com", "123", AccountRole.AccountDepartment));
            listAccount.Add(new Account(context.RetailStores.Where(r => r.Id == 2).Single(), "Truong", "Dang", "truong@gmail.com", "123", AccountRole.Admin));
            listAccount.Add(new Account(context.RetailStores.Where(r => r.Id == 3).Single(), "Vy", "Pham", "vy@gmail.com", "123", AccountRole.EmployeeOfRetailOutlet));
            listAccount.Add(new Account(context.RetailStores.Where(r => r.Id == 1).Single(), "Linh", "Nguyen", "linh@gmail.com", "123", AccountRole.TechnicalPeople));

            context.Accounts.AddRange(listAccount);
            context.SaveChanges();

            listCustomer.Add(new Customer("Trung", "Nguyen", "trung@gmail.com", "123", AccountRole.User));
            listCustomer.Add(new Customer("Dai", "Nguyen", "dai@gmail.com", "123", AccountRole.User));
            listCustomer.Add(new Customer("Nam", "Nguyen", "nam@gmail;com", "123", AccountRole.User));

            context.Customers.AddRange(listCustomer);
            context.SaveChanges();

            listFeedback.Add(new Feedback("good", context.Customers.Where(c => c.Id == 1).Single()));
            listFeedback.Add(new Feedback("so so", context.Customers.Where(c => c.Id == 2).Single()));
            listFeedback.Add(new Feedback("very good", context.Customers.Where(c => c.Id == 3).Single()));

            context.Feedbacks.AddRange(listFeedback);
            context.SaveChanges();

            listContract.Add(new Contract(OrderStatus.Pending, 0.3, 2000000,  5, new DateTime(2019, 1, 1), 0.1, context.Customers.Where(c => c.Id == 1).Single(), context.Services.Where(s => s.Id == 1).Single(), context.RetailStores.Where(a => a.Id == 1).Single(), context.ServicePackages.Where(sp=>sp.Id==1).Single()));
            listContract.Add(new Contract(OrderStatus.Pending, 0.3, 2000000,  5, new DateTime(2019, 1, 1), 0.1, context.Customers.Where(c => c.Id == 2).Single(), context.Services.Where(s => s.Id == 2).Single(), context.RetailStores.Where(a => a.Id == 2).Single(), context.ServicePackages.Where(sp => sp.Id == 1).Single()));
            listContract.Add(new Contract(OrderStatus.Pending, 0.3, 2000000,  5, new DateTime(2019, 1, 1), 0.1, context.Customers.Where(c => c.Id == 3).Single(), context.Services.Where(s => s.Id == 3).Single(), context.RetailStores.Where(a => a.Id == 3).Single(), context.ServicePackages.Where(sp => sp.Id == 1).Single()));

            context.Contracts.AddRange(listContract);
            context.SaveChanges();

            listBilling.Add(new Billing(context.Contracts.Where(c=>c.Id ==1).Single()));
            listBilling.Add(new Billing(context.Contracts.Where(c => c.Id == 2).Single()));
            listBilling.Add(new Billing(context.Contracts.Where(c => c.Id == 3).Single()));

            context.Billings.AddRange(listBilling);
            context.SaveChanges();

        }
    }
}
