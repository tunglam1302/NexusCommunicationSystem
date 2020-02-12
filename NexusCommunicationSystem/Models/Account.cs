using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Account : IAccount
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FirstName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string LastName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Email { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserPassword { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FeedBack { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public AccountRole UserRole { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public virtual RetailStore RetailStore { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}