using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("OrderStatus")]
        public OrderStatus OrderStatus { get; set; }
        [DisplayName("Create dAt")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("Updated At")]
        public DateTime UpdatedAt { get; set; }
        [DisplayName("Security Deposit")]
        public double SecurityDeposit { get; set; }
        [DisplayName("Total Amount")]
        public double TotalAmount { get; set; }
        public int Quantity { get; set; }
        [DisplayName("Next Payment At")]
        public DateTime? NextPaymentAt { get; set; }
        public double Discounts { get; set; }
        [DisplayName("Accepted By")]
        public string AcceptedBy { get; set; }
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }
        [DisplayName("Service Id")]
        public int ServiceId { get; set; }
        [DisplayName("Retail Store Id")]
        public int RetailStoreId { get; set; }
        [DisplayName("Service Package Id")]
        public int ServicePackageId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Service Service { get; set; }
        [DisplayName("Retail Store")]
        public virtual RetailStore RetailStore { get; set; }
        [DisplayName("Service Package")]
        public virtual ServicePackage ServicePackage { get; set; }
        public virtual ICollection<Billing> Billings { get; set; }

        public Contract() { }

        public Contract(OrderStatus orderStatus, double securityDeposit, double totalAmount, int quantity, DateTime nextPaymentAt, double discounts, Customer customer, Service service, RetailStore retailStore, ServicePackage servicePackage, DateTime? createdAt = null, DateTime? updatedAt = null) {
            OrderStatus = orderStatus;
            CreatedAt = createdAt??DateTime.Now;
            UpdatedAt = updatedAt ?? DateTime.Now;
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