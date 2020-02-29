﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Customer : IAccount
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserPassword { get; set; }
        public AccountRole UserRole { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Contract> Orders { get; set; }
        public string AccountId { get;set; }
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