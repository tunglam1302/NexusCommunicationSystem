using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public virtual Customer Customer { get; set; }
    }
}