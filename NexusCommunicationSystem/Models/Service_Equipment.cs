﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NexusCommunicationSystem.Models
{
    public class Service_Equipment
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        [DisplayName("Service Id")]
        public int ServiceId { get; set; }
        [DisplayName("Equipment Id")]
        public int EquipmentId { get; set; }
        public virtual Service Service { get; set; }
        public virtual Equipment Equipment { get; set; }

        public Service_Equipment() { }
        public Service_Equipment(int quantity, Service service, Equipment equipment)
        {
            Quantity = quantity;
            Service = service;
            Equipment = equipment;
            ServiceId = service.Id;
            EquipmentId = equipment.Id;
        }

        public Service_Equipment(int id, int quantity, Service service, Equipment equipment)
        {
            Id = id;
            Quantity = quantity;
            Service = service;
            Equipment = equipment;
            ServiceId = service.Id;
            EquipmentId = equipment.Id;
        }
    }
}