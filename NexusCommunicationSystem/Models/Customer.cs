using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Customer : IAccount
    {
        public int Id { get; set; }
        [DisplayName("Firt Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        public string Email { get; set; }
        [DisplayName("Password")]
        public string UserPassword { get; set; }
        [DisplayName("User Role")]
        public AccountRole UserRole { get; set; }
        [DisplayName("Feed backs")]
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Contract> Orders { get; set; }
        [DisplayName("Account Id")]
        public string AccountId { get;set; }
        [DisplayName("Passcode")]
        public string Passcode { get; set; }
        public Customer() { }

        public Customer(string firstName, string lastName, string email, string userPassword, AccountRole accountRole)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserPassword = userPassword;
            UserRole = accountRole;
            AccountId = this.Id.ToString();
            Passcode = this.RandomDigits();
        }
        public string RandomDigits()
        {
            Random r = new Random();
            var x = r.Next(0, 1000000);
            string s = x.ToString("000000");
            return s;
        }
    }
}