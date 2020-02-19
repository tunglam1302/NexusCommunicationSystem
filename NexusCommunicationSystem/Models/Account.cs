using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Account : IAccount
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public AccountRole UserRole { get; set; }

        public int RetailStoreId { get; set; }
        public virtual RetailStore RetailStore { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }

        public Account() { }

        public Account(RetailStore retailStore, string firstName, string lastName, string email, string userPassword, AccountRole userRole) {
            RetailStore = retailStore;
            RetailStoreId = retailStore.Id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserPassword = userPassword;
            UserRole = userRole;
        }
    }
}