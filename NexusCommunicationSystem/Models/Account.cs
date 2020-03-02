using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Account : IAccount
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Firt Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DisplayName("Password")]
        public string UserPassword { get; set; }
        [Required]
        [DisplayName("User Role")]
        public AccountRole UserRole { get; set; }
        [DisplayName("Retail Store Id")]
        public int RetailStoreId { get; set; }
        public virtual RetailStore RetailStore { get; set; }
        [DisplayName("Account Id")]
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