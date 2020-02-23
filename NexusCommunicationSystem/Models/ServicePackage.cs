using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NexusCommunicationSystem.Models
{
    public class ServicePackage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public ServicePackage()
        { }
        public ServicePackage(string name, string description = null)
        {
            Name = name;
        }
    }
}