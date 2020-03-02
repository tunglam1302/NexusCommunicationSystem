using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NexusCommunicationSystem.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        public string Description { get; set; }
        [DisplayName("Total Amount")]
        public int TotalAmount { get; set; }
        public virtual ICollection<Contract> Contracts { get; set; }
        [DisplayName("Service Equipments")]
        public virtual ICollection<Service_Equipment> Service_Equipments { get; set; }

        public Service() { }

        public Service(string name, string image,int totalAmount, string description = null)
        {
            Name = name;
            Image = image;
            Description = description;
            TotalAmount = totalAmount;
        }

        public Service(int id, string name, string image,ServicePackage servicePackage, string description = null)
        {
            Id = id;
            Name = name;
            Image = image;
            Description = description;
        }
    }

    
}