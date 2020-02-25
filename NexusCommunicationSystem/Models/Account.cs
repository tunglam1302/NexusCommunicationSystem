using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Account : IAccount
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string UserPassword { get; set; }
        [Required]
        public AccountRole UserRole { get; set; }

        public int RetailStoreId { get; set; }
        public virtual RetailStore RetailStore { get; set; }
        public string AccountId { get; set; }

        public Account() { }

        public Account(RetailStore retailStore, string firstName, string lastName, string email, string userPassword, AccountRole userRole) {
            RetailStore = retailStore;
            RetailStoreId = retailStore.Id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserPassword = userPassword;
            UserRole = userRole;
            AccountId = this.Id.ToString();
        }
    }
}