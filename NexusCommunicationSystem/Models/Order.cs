﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public float SecurityDeposit { get; set; }
        public double TotalAmount { get; set; }
        public double PaidAmount { get; set; }
        public double AmountDue { get; set; }
        public DateTime DateToLatestPayment { get; set; }
        public float ChargeForReplacementDone { get; set; }
        public float Discounts { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        public virtual RetailStore RetailStore { get; set; }
        public virtual Account Account { get; set; }
        public virtual ServicePackage ServicePackage { get; set; }
        public virtual ICollection<Billing> Billings { get; set; }
    }

    public enum OrderStatus
    {
        Pending = 5,
        Confirmed = 4,
        DirectTransfer = 3,
        Done = 1,
        Shipping = 2,
        Cancel = 0,
        Deleted = -1,
    }
}