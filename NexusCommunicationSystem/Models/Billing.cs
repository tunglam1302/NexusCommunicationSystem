using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Billing
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("Updated At")]
        public DateTime UpdatedAt { get; set; }
        [DisplayName("Billing Amount")]
        public int BillingAmount { get; set; }
        [DisplayName("Contract Id")]
        public int ContractId { get; set; }
        public virtual Contract Contract { get; set; }
        public Billing() { }
        public Billing(Contract contract){
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            ContractId = contract.Id;
            Contract = contract;
        }
    }
}