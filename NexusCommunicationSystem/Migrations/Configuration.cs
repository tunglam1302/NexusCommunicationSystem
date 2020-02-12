namespace NexusCommunicationSystem.Migrations
{
    using NexusCommunicationSystem.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NexusCommunicationSystem.Models.NexusCommunicationSystemContext>
    {
        List<Account> listAccount = new List<Account>();
        List<Billing> listBilling = new List<Billing>();
        List<Customer> listCustomer = new List<Customer>();
        List<Equipment> listEquipment = new List<Equipment>();
        List<Feedback> listFeedback = new List<Feedback>();
        List<Order> listOrder = new List<Order>();
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
            //AddAccount("Tran", "An");
            //AddAccount("Tran", "Binh");
            //AddAccount("Tran", "Tuan");
            //AddAccount("Nguyen", "Can");
            //AddAccount("Dang", "Trung");

            //AddAccountRole("User");
            //AddAccountRole("Admin");

            //AddNews("Maintain your Finance and Technology Expertise", "Join us at our events to watch pitches by companies deploying the latest technology, watch our video library of over 400 videos, and follow our newsletters with valuable information on the latest reports and happenings in the scene.");

            ////context.Accounts.AddRange(listAccount);
            ////context.AccountRoles.AddRange(listAccountRole);
            //context.News.AddRange(listNews);
            //context.SaveChanges();
        }

        //private void AddAccount(string firstName, string lastName)
        //{
        //    listAccount.Add(new Account
        //    {
        //        FirstName = firstName,
        //        LastName = lastName,
        //        CreatedAt = DateTime.Now,
        //        Status = Account.AccountStatus.Active,

        //    });
        //}

        //private void AddAccountRole(string description)
        //{
        //    listAccountRole.Add(new AccountRole
        //    {
        //        Description = description,
        //        CreatedAt = DateTime.Now,
        //        Status = AccountRole.AccountRoleStatus.Active,
        //    });
        //}

        //private void AddNews(string title, string content)
        //{
        //    listNews.Add(new News
        //    {
        //        Title = title,
        //        Content = content,
        //        PublishedAt = DateTime.Now,
        //        Status = News.AuthorStatus.Active,
        //    });
        //}
    }
}
