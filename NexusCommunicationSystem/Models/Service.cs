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
        public virtual ICollection<Contract> Contracts { get; set; }
        public virtual ICollection<Service_Equipment> Service_Equipments { get; set; }

        public Service() { }

        public Service(string name, string image, string description = null)
        {
            Name = name;
            Image = image;
            Description = description;
        }
    }

    
}