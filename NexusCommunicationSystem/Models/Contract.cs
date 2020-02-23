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
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public double SecurityDeposit { get; set; }
        public double TotalAmount { get; set; }
        public int Quantity { get; set; }
        public DateTime? NextPaymentAt { get; set; }
        public double Discounts { get; set; }
        public string AcceptedBy { get; set; }

        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int RetailStoreId { get; set; }
        public int ServicePackageId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        public virtual RetailStore RetailStore { get; set; }
        public virtual ServicePackage ServicePackage { get; set; }

        public Contract() { }

        public Contract(OrderStatus orderStatus, double securityDeposit, double totalAmount, int quantity, DateTime nextPaymentAt, double discounts, Customer customer, Service service, RetailStore retailStore, ServicePackage servicePackage) {
            OrderStatus = orderStatus;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            SecurityDeposit = securityDeposit;
            TotalAmount = totalAmount;
            Quantity = quantity;
            NextPaymentAt = nextPaymentAt;
            Discounts = discounts;

            Customer = customer;
            Service = service;
            RetailStore = retailStore;
            ServicePackage = servicePackage;

            CustomerId = customer.Id;
            ServiceId = service.Id;
            RetailStoreId = retailStore.Id;
            ServicePackageId = servicePackage.Id;
        }
    }

    public enum OrderStatus
    {
        Pending = 0,
        Confirmed = 4,
        DirectTransfer = 3,
        Done = 1,
        Shipping = 2,
        Cancel = 5,
        Deleted = -1,
    }
}