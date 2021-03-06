﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class RetailStore
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        
        public string Address { get; set; }
        [Required]
        public string Telephone { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }

        public RetailStore()
        { }

        public RetailStore(string name, string address, string telephone)
        {
            Name = name;
            Address = address;
            Telephone = telephone;
        }
    }
}