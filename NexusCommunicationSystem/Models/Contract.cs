using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Contract
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public OrderStatus OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public double SecurityDeposit { get; set; }
        public double TotalAmount { get; set; }
        public double AmountDue { get; set; }
        public int Quantity { get; set; }
        public DateTime NextPaymentAt { get; set; }
        public double ChargeForReplacementDone { get; set; }
        public double Discounts { get; set; }

        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int AccountId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        public virtual Account Account { get; set; }
        public virtual ICollection<Billing> Billings { get; set; }

        public Contract() { }

        public Contract(OrderStatus orderStatus, double securityDeposit, double totalAmount, double amountDue, int quantity, DateTime nextPaymentAt, double chargeForReplacementDone, double discounts, Customer customer, Service service, Account account) {
            OrderStatus = orderStatus;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            SecurityDeposit = securityDeposit;
            TotalAmount = totalAmount;
            AmountDue = amountDue;
            Quantity = quantity;
            NextPaymentAt = nextPaymentAt;
            ChargeForReplacementDone = chargeForReplacementDone;
            Discounts = discounts;

            Customer = customer;
            Service = service;
            Account = account;

            CustomerId = customer.Id;
            ServiceId = service.Id;
            AccountId = account.Id;
        }
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