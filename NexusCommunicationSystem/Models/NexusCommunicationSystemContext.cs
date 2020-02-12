using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class NexusCommunicationSystemContext:DbContext
    {
        public NexusCommunicationSystemContext() : base("name=MyDBContext")
        {
        }
        public System.Data.Entity.DbSet<NexusCommunicationSystem.Models.Account> Accounts { get; set; }
        public System.Data.Entity.DbSet<NexusCommunicationSystem.Models.Billing> Billings { get; set; }
        public System.Data.Entity.DbSet<NexusCommunicationSystem.Models.Customer> Customers { get; set; }
        public System.Data.Entity.DbSet<NexusCommunicationSystem.Models.Equipment> Equipments { get; set; }
        public System.Data.Entity.DbSet<NexusCommunicationSystem.Models.Feedback> Feedbacks { get; set; }
        public System.Data.Entity.DbSet<NexusCommunicationSystem.Models.Order> Orders { get; set; }
        public System.Data.Entity.DbSet<NexusCommunicationSystem.Models.RetailStore> RetailStores { get; set; }
        public System.Data.Entity.DbSet<NexusCommunicationSystem.Models.Service> Services { get; set; }
        public System.Data.Entity.DbSet<NexusCommunicationSystem.Models.Service_Equipment> Service_Equipments { get; set; }
        public System.Data.Entity.DbSet<NexusCommunicationSystem.Models.ServicePackage> ServicePackages { get; set; }
        public System.Data.Entity.DbSet<NexusCommunicationSystem.Models.Vendor> Vendors { get; set; }
    }
}