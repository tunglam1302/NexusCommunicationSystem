using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NexusCommunicationSystem.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        [DisplayName("Vendor Id")]
        public int VendorId { get; set; }
        public virtual Vendor Vendor { get; set; }
        [DisplayName("Service Equipment")]
        public virtual ICollection<Service_Equipment> Service_Equipments { get; set; }
        public Equipment()
        { }
        public Equipment(string name, double price, int amount, Vendor vendor)
        {
            Name = name;
            Price = price;
            Amount = amount;
            Vendor = vendor;
            VendorId = vendor.Id;
        }
    }
}