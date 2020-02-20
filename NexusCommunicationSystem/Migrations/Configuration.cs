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
            listService.Add(new Service("Landline Local 75", "Unlimited 75$ for an year/call charges 55cents per min/Free setup Fee/Online support 247", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Landline Local 35", "Monthly 125$/Local 70cents, STD 2.25$ per min/Free setup Fee/Online support 247", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Landline STD 125", "Monthly 125$/Local 70cents, STD 2.25$ per min/Free setup Fee/Online support 247", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Landline STD 420", "Half yearly 420$/Local 60cents, STD 2.00$ per min/Free setup Fee/Online support 247", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Landline STD Yearly", "Yearly 840$/Validity/Free setup Fee/Online support 247", context.ServicePackages.Where(s => s.Id == 1).Single()));

            //Internet Service Group: Dial-up
            listService.Add(new Service("Dialup Hourly", "Pay per hour used/Free setup Fee/Free Modem or Router/Online support 247", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Dialup Unlimited 28", "Unlimited 28Kbps/Free setup Fee/Free Modem or Router/Online support 247", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Dialup Unlimited 56", "Unlimited 56Kbps/Free setup Fee/Free Modem or Router/Online support 247", context.ServicePackages.Where(s => s.Id == 1).Single()));

            //Internet Service Group: Broadband
            listService.Add(new Service("Broadband Hourly", "Pay per hour used/Validity is for 1 or 6 month/Free setup Fee/Free Modem or Router/Online support 247", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Broadband Unlimited 64", "Unlimited 64Kbps/Free setup Fee/Free Modem or Router/Online support 247", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Broadband Unlimited 128", "Unlimited 128Kbps/Free setup Fee/Free Modem or Router/Online support 247", context.ServicePackages.Where(s => s.Id == 1).Single()));


            //Combo service group
            //listService.Add(new Service("Combo-DialupH10-LandlineL75","Combo Dialup Hour basis 10 Hrs 50USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupH30-LandlineL75","Combo Dialup Hour basis 30 Hrs 130USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupH60-LandlineL75","Combo Dialup Hour basis 60 Hrs 260USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited28Monthly-LandlineL75","Combo Dialup Unlimited 28Kbps Monthly 75USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited28Quaterly-LandlineL75", "Combo Dialup Unlimited 28Kbps Quaterly 150USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited56Monthly-LandlineL75", "Combo Dialup Unlimited 56Kbps Monthly 100USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited56Quaterly-LandlineL75", "Combo Dialup Unlimited 56Kbps Quaterly 180USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandH30-LandlineL75", "Combo Broad band Hour basis 30 Hrs 175USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandH60-LandlineL75", "Combo Broad band Hour basis 60 Hrs 315USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited64Monthly-LandlineL75", "Combo Broad band Unlimited 64Kbps Monthly 225USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited64Quaterly-LandlineL75", "Combo Broad band Unlimited 64Kbps Quaterly 400USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited128Monthly-LandlineL75", "Combo Broad band Unlimited 128Kbps Monthly 350USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited128Quaterly-LandlineL75", "Combo Broad band Unlimited 128Kbps Quaterly 445USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));

            //listService.Add(new Service("Combo-DialupH10-LandlineL35", "Combo Dialup Hour basis 10 Hrs 50USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupH30-LandlineL35", "Combo Dialup Hour basis 30 Hrs 130USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupH60-LandlineL35", "Combo Dialup Hour basis 60 Hrs 260USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited28Monthly-LandlineL35", "Combo Dialup Unlimited 28Kbps Monthly 75USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited28Quaterly-LandlineL35", "Combo Dialup Unlimited 28Kbps Quaterly 150USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited56Monthly-LandlineL35", "Combo Dialup Unlimited 56Kbps Monthly 100USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited56Quaterly-LandlineL35", "Combo Dialup Unlimited 56Kbps Quaterly 180USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandH30-LandlineL35", "Combo Broad band Hour basis 30 Hrs 175USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandH60-LandlineL35", "Combo Broad band Hour basis 60 Hrs 315USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited64Monthly-LandlineL35", "Combo Broad band Unlimited 64Kbps Monthly 225USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited64Quaterly-LandlineL35", "Combo Broad band Unlimited 64Kbps Quaterly 400USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited128Monthly-LandlineL35", "Combo Broad band Unlimited 128Kbps Monthly 350USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited128Quaterly-LandlineL35", "Combo Broad band Unlimited 128Kbps Quaterly 445USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));

            //listService.Add(new Service("Combo-DialupH10-LandlineSTDMonthly125", "Combo Dialup Hour basis 10 Hrs 50USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupH30-LandlineSTDMonthly125", "Combo Dialup Hour basis 30 Hrs 130USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupH60-LandlineSTDMonthly125", "Combo Dialup Hour basis 60 Hrs 260USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited28Monthly-LandlineSTDMonthly125", "Combo Dialup Unlimited 28Kbps Monthly 75USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited28Quaterly-LandlineSTDMonthly125", "Combo Dialup Unlimited 28Kbps Quaterly 150USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited56Monthly-LandlineSTDMonthly125", "Combo Dialup Unlimited 56Kbps Monthly 100USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited56Quaterly-LandlineSTDMonthly125", "Combo Dialup Unlimited 56Kbps Quaterly 180USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandH30-LandlineSTDMonthly125", "Combo Broad band Hour basis 30 Hrs 175USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandH60-LandlineSTDMonthly125", "Combo Broad band Hour basis 60 Hrs 315USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited64Monthly-LandlineSTDMonthly125", "Combo Broad band Unlimited 64Kbps Monthly 225USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited64Quaterly-LandlineSTDMonthly125", "Combo Broad band Unlimited 64Kbps Quaterly 400USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited128Monthly-LandlineSTDMonthly125", "Combo Broad band Unlimited 128Kbps Monthly 350USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited128Quaterly-LandlineSTDMonthly125", "Combo Broad band Unlimited 128Kbps Quaterly 445USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));

            //listService.Add(new Service("Combo-DialupH10-LandlineSTDHalfYear420", "Combo Dialup Hour basis 10 Hrs 50USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupH30-LandlineSTDHalfYear420", "Combo Dialup Hour basis 30 Hrs 130USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupH60-LandlineSTDHalfYear420", "Combo Dialup Hour basis 60 Hrs 260USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited28Monthly-LandlineSTDHalfYear420", "Combo Dialup Unlimited 28Kbps Monthly 75USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited28Quaterly-LandlineSTDHalfYear420", "Combo Dialup Unlimited 28Kbps Quaterly 150USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited56Monthly-LandlineSTDHalfYear420", "Combo Dialup Unlimited 56Kbps Monthly 100USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited56Quaterly-LandlineSTDHalfYear420", "Combo Dialup Unlimited 56Kbps Quaterly 180USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandH30-LandlineSTDHalfYear420", "Combo Broad band Hour basis 30 Hrs 175USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandH60-LandlineSTDHalfYear420", "Combo Broad band Hour basis 60 Hrs 315USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited64Monthly-LandlineSTDHalfYear420", "Combo Broad band Unlimited 64Kbps Monthly 225USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited64Quaterly-LandlineSTDHalfYear420", "Combo Broad band Unlimited 64Kbps Quaterly 400USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited128Monthly-LandlineSTDHalfYear420", "Combo Broad band Unlimited 128Kbps Monthly 350USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited128Quaterly-LandlineSTDHalfYear420", "Combo Broad band Unlimited 128Kbps Quaterly 445USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));

            //listService.Add(new Service("Combo-DialupH10-LandlineSTDYearly", "Combo Dialup Hour basis 10 Hrs 50USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupH30-LandlineSTDYearly", "Combo Dialup Hour basis 30 Hrs 130USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupH60-LandlineSTDYearly", "Combo Dialup Hour basis 60 Hrs 260USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited28Monthly-LandlineSTDYearly", "Combo Dialup Unlimited 28Kbps Monthly 75USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited28Quaterly-LandlineSTDYearly", "Combo Dialup Unlimited 28Kbps Quaterly 150USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited56Monthly-LandlineSTDYearly", "Combo Dialup Unlimited 56Kbps Monthly 100USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-DialupUnlimited56Quaterly-LandlineSTDYearly", "Combo Dialup Unlimited 56Kbps Quaterly 180USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandH30-LandlineSTDYearly", "Combo Broad band Hour basis 30 Hrs 175USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandH60-LandlineSTDYearly", "Combo Broad band Hour basis 60 Hrs 315USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited64Monthly-LandlineSTDYearly", "Combo Broad band Unlimited 64Kbps Monthly 225USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited64Quaterly-LandlineSTDYearly", "Combo Broad band Unlimited 64Kbps Quaterly 400USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited128Monthly-LandlineSTDYearly", "Combo Broad band Unlimited 128Kbps Monthly 350USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            //listService.Add(new Service("Combo-BroadbandUnlimited128Quaterly-LandlineSTDYearly", "Combo Broad band Unlimited 128Kbps Quaterly 445USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));


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

            listContract.Add(new Contract(OrderStatus.Pending, 0.3, 2000000, 100000, 5, new DateTime(2019, 1, 1), 0.1, 0.1, context.Customers.Where(c => c.Id == 1).Single(), context.Services.Where(s => s.Id == 1).Single(), context.Accounts.Where(a => a.Id == 1).Single()));
            listContract.Add(new Contract(OrderStatus.Pending, 0.3, 2000000, 100000, 5, new DateTime(2019, 1, 1), 0.1, 0.1, context.Customers.Where(c => c.Id == 2).Single(), context.Services.Where(s => s.Id == 2).Single(), context.Accounts.Where(a => a.Id == 2).Single()));
            listContract.Add(new Contract(OrderStatus.Pending, 0.3, 2000000, 100000, 5, new DateTime(2019, 1, 1), 0.1, 0.1, context.Customers.Where(c => c.Id == 3).Single(), context.Services.Where(s => s.Id == 3).Single(), context.Accounts.Where(a => a.Id == 3).Single()));

            context.Contracts.AddRange(listContract);
            context.SaveChanges();

            listBilling.Add(new Billing(context.Contracts.Where(o => o.Id == 1).Single()));
            listBilling.Add(new Billing(context.Contracts.Where(o => o.Id == 2).Single()));
            listBilling.Add(new Billing(context.Contracts.Where(o => o.Id == 3).Single()));

            context.Billings.AddRange(listBilling);
            context.SaveChanges();

        }
    }
}
