using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NexusCommunicationSystem.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Contract> Orders { get; set; }
        public virtual ICollection<Service_Equipment> Service_Equipments { get; set; }
    }
}