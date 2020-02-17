using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NexusCommunicationSystem.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        public virtual ICollection<Equipment> Equipments { get; set; }

        public Vendor()
        { }
        public Vendor(string name, string address)
        {
            Name = name;
            Address = address;
        }
    }
}