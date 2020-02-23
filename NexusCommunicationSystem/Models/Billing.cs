using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Billing
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int ContractId { get; set; }
        public Billing(){
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}