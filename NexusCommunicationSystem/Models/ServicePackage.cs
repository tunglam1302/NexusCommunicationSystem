using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NexusCommunicationSystem.Models
{
    public class ServicePackage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public PaymentPeriod PaymentPeriod { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public ServicePackage()
        { }
        public ServicePackage(PaymentPeriod paymentPeriod, string description = null)
        {
            PaymentPeriod = paymentPeriod;
        }
    }

    public enum PaymentPeriod
    {
        Monthly = 0,
        Quarterly = 1,
        HalfYearly = 2,
        Yearly = 3
    }
}