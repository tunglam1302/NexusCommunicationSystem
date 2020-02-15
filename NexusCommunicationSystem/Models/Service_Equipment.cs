namespace NexusCommunicationSystem.Models
{
    public class Service_Equipment
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public virtual Service Service { get; set; }
        public virtual Equipment Equipment { get; set; }

        public Service_Equipment() { }
        public Service_Equipment(int quantity, Service service, Equipment equipment)
        {
            Quantity = quantity;
            Service = service;
            Equipment = equipment;
        }

        public Service_Equipment(int id, int quantity, Service service, Equipment equipment)
        {
            Id = id;
            Quantity = quantity;
            Service = service;
            Equipment = equipment;

        }
    }
}