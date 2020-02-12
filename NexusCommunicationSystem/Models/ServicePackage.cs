using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NexusCommunicationSystem.Models
{
    public class ServicePackage
    {
        public int Id { get; set; }
        [Required]
        public string PaymentPeriod { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}