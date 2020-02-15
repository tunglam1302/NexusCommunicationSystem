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
        public int TotalAmount { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Service_Equipment> Service_Equipments { get; set; }

        public virtual ServicePackage ServicePackage { get; set; }

        public Service() { }

        public Service(string name, string image, ServicePackage servicePackage = null, string description = null)
        {
            Name = name;
            Image = image;
            Description = description;
            ServicePackage = servicePackage;
        }

        public Service(int id, string name, string image, string description = null,ServicePackage servicePackage = null)
        {
            Id = id;
            Name = name;
            Image = image;
            Description = description;
            ServicePackage = servicePackage;
        }
    }

    
}