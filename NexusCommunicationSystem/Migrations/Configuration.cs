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
        List<Contract> listOrder = new List<Contract>();
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
            listVendor.Add(new Vendor("Mobilephone", "Cau Giay, Hanoi"));
            listVendor.Add(new Vendor("FPT", "Minh Khai, Hanoi"));
            listVendor.Add(new Vendor("Viettel", "Hoang Mai, Hanoi"));

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

            context.ServicePackages.AddRange(listServicePackage);
            context.SaveChanges();

            listService.Add(new Service("Landline","Land line service",context.ServicePackages.Where(s=>s.Id==1).Single()));
            listService.Add(new Service("Broadband","Internet Broad band service", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("DialUp","Internet Dialup service", context.ServicePackages.Where(s => s.Id == 1).Single()));
            
            listService.Add(new Service("Combo-DialupH10-LandlineL75","Combo Dialup Hour basis 10 Hrs 50USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupH30-LandlineL75","Combo Dialup Hour basis 30 Hrs 130USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupH60-LandlineL75","Combo Dialup Hour basis 60 Hrs 260USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited28Monthly-LandlineL75","Combo Dialup Unlimited 28Kbps Monthly 75USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited28Quaterly-LandlineL75", "Combo Dialup Unlimited 28Kbps Quaterly 150USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited56Monthly-LandlineL75", "Combo Dialup Unlimited 56Kbps Monthly 100USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited56Quaterly-LandlineL75", "Combo Dialup Unlimited 56Kbps Quaterly 180USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandH30-LandlineL75", "Combo Broad band Hour basis 30 Hrs 175USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandH60-LandlineL75", "Combo Broad band Hour basis 60 Hrs 315USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited64Monthly-LandlineL75", "Combo Broad band Unlimited 64Kbps Monthly 225USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited64Quaterly-LandlineL75", "Combo Broad band Unlimited 64Kbps Quaterly 400USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited128Monthly-LandlineL75", "Combo Broad band Unlimited 128Kbps Monthly 350USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited128Quaterly-LandlineL75", "Combo Broad band Unlimited 128Kbps Quaterly 445USD and Land line Local Plan unlimited 75USD", context.ServicePackages.Where(s => s.Id == 1).Single()));

            listService.Add(new Service("Combo-DialupH10-LandlineL35", "Combo Dialup Hour basis 10 Hrs 50USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupH30-LandlineL35", "Combo Dialup Hour basis 30 Hrs 130USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupH60-LandlineL35", "Combo Dialup Hour basis 60 Hrs 260USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited28Monthly-LandlineL35", "Combo Dialup Unlimited 28Kbps Monthly 75USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited28Quaterly-LandlineL35", "Combo Dialup Unlimited 28Kbps Quaterly 150USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited56Monthly-LandlineL35", "Combo Dialup Unlimited 56Kbps Monthly 100USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited56Quaterly-LandlineL35", "Combo Dialup Unlimited 56Kbps Quaterly 180USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandH30-LandlineL35", "Combo Broad band Hour basis 30 Hrs 175USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandH60-LandlineL35", "Combo Broad band Hour basis 60 Hrs 315USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited64Monthly-LandlineL35", "Combo Broad band Unlimited 64Kbps Monthly 225USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited64Quaterly-LandlineL35", "Combo Broad band Unlimited 64Kbps Quaterly 400USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited128Monthly-LandlineL35", "Combo Broad band Unlimited 128Kbps Monthly 350USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited128Quaterly-LandlineL35", "Combo Broad band Unlimited 128Kbps Quaterly 445USD and Land line Local Plan monthly 35USD", context.ServicePackages.Where(s => s.Id == 1).Single()));

            listService.Add(new Service("Combo-DialupH10-LandlineSTDMonthly125", "Combo Dialup Hour basis 10 Hrs 50USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupH30-LandlineSTDMonthly125", "Combo Dialup Hour basis 30 Hrs 130USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupH60-LandlineSTDMonthly125", "Combo Dialup Hour basis 60 Hrs 260USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited28Monthly-LandlineSTDMonthly125", "Combo Dialup Unlimited 28Kbps Monthly 75USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited28Quaterly-LandlineSTDMonthly125", "Combo Dialup Unlimited 28Kbps Quaterly 150USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited56Monthly-LandlineSTDMonthly125", "Combo Dialup Unlimited 56Kbps Monthly 100USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited56Quaterly-LandlineSTDMonthly125", "Combo Dialup Unlimited 56Kbps Quaterly 180USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandH30-LandlineSTDMonthly125", "Combo Broad band Hour basis 30 Hrs 175USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandH60-LandlineSTDMonthly125", "Combo Broad band Hour basis 60 Hrs 315USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited64Monthly-LandlineSTDMonthly125", "Combo Broad band Unlimited 64Kbps Monthly 225USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited64Quaterly-LandlineSTDMonthly125", "Combo Broad band Unlimited 64Kbps Quaterly 400USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited128Monthly-LandlineSTDMonthly125", "Combo Broad band Unlimited 128Kbps Monthly 350USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited128Quaterly-LandlineSTDMonthly125", "Combo Broad band Unlimited 128Kbps Quaterly 445USD and Land line STD monthly 125USD", context.ServicePackages.Where(s => s.Id == 1).Single()));

            listService.Add(new Service("Combo-DialupH10-LandlineSTDHalfYear420", "Combo Dialup Hour basis 10 Hrs 50USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupH30-LandlineSTDHalfYear420", "Combo Dialup Hour basis 30 Hrs 130USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupH60-LandlineSTDHalfYear420", "Combo Dialup Hour basis 60 Hrs 260USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited28Monthly-LandlineSTDHalfYear420", "Combo Dialup Unlimited 28Kbps Monthly 75USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited28Quaterly-LandlineSTDHalfYear420", "Combo Dialup Unlimited 28Kbps Quaterly 150USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited56Monthly-LandlineSTDHalfYear420", "Combo Dialup Unlimited 56Kbps Monthly 100USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited56Quaterly-LandlineSTDHalfYear420", "Combo Dialup Unlimited 56Kbps Quaterly 180USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandH30-LandlineSTDHalfYear420", "Combo Broad band Hour basis 30 Hrs 175USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandH60-LandlineSTDHalfYear420", "Combo Broad band Hour basis 60 Hrs 315USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited64Monthly-LandlineSTDHalfYear420", "Combo Broad band Unlimited 64Kbps Monthly 225USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited64Quaterly-LandlineSTDHalfYear420", "Combo Broad band Unlimited 64Kbps Quaterly 400USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited128Monthly-LandlineSTDHalfYear420", "Combo Broad band Unlimited 128Kbps Monthly 350USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited128Quaterly-LandlineSTDHalfYear420", "Combo Broad band Unlimited 128Kbps Quaterly 445USD and Land line STD Half Year 420USD", context.ServicePackages.Where(s => s.Id == 1).Single()));

            listService.Add(new Service("Combo-DialupH10-LandlineSTDYearly", "Combo Dialup Hour basis 10 Hrs 50USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupH30-LandlineSTDYearly", "Combo Dialup Hour basis 30 Hrs 130USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupH60-LandlineSTDYearly", "Combo Dialup Hour basis 60 Hrs 260USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited28Monthly-LandlineSTDYearly", "Combo Dialup Unlimited 28Kbps Monthly 75USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited28Quaterly-LandlineSTDYearly", "Combo Dialup Unlimited 28Kbps Quaterly 150USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited56Monthly-LandlineSTDYearly", "Combo Dialup Unlimited 56Kbps Monthly 100USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-DialupUnlimited56Quaterly-LandlineSTDYearly", "Combo Dialup Unlimited 56Kbps Quaterly 180USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandH30-LandlineSTDYearly", "Combo Broad band Hour basis 30 Hrs 175USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandH60-LandlineSTDYearly", "Combo Broad band Hour basis 60 Hrs 315USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited64Monthly-LandlineSTDYearly", "Combo Broad band Unlimited 64Kbps Monthly 225USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited64Quaterly-LandlineSTDYearly", "Combo Broad band Unlimited 64Kbps Quaterly 400USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited128Monthly-LandlineSTDYearly", "Combo Broad band Unlimited 128Kbps Monthly 350USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));
            listService.Add(new Service("Combo-BroadbandUnlimited128Quaterly-LandlineSTDYearly", "Combo Broad band Unlimited 128Kbps Quaterly 445USD and Land line STD Yearly", context.ServicePackages.Where(s => s.Id == 1).Single()));


            context.Services.AddRange(listService);
            context.SaveChanges();

            listService_Equipment.Add(new Service_Equipment(2, context.Services.Where(s => s.Id == 1).Single(), context.Equipments.Where(e => e.Id == 3).Single()));
            listService_Equipment.Add(new Service_Equipment(1, context.Services.Where(s => s.Id == 2).Single(), context.Equipments.Where(e => e.Id == 2).Single()));
            listService_Equipment.Add(new Service_Equipment(3, context.Services.Where(s => s.Id == 3).Single(), context.Equipments.Where(e => e.Id == 1).Single()));
            listService_Equipment.Add(new Service_Equipment(4, context.Services.Where(s => s.Id == 1).Single(), context.Equipments.Where(e => e.Id == 3).Single()));

            context.Service_Equipments.AddRange(listService_Equipment);
            context.SaveChanges();
        }
    }
}
