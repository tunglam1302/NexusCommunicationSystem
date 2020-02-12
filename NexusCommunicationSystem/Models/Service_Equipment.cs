namespace NexusCommunicationSystem.Models
{
    public class Service_Equipment
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public virtual Service Service { get; set; }
        public virtual Equipment Equipment { get; set; }
    }
}