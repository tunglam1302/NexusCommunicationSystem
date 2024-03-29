﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NexusCommunicationSystem.Models
{
    public class Feedback
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        [DisplayName("Created At")]
        public DateTime CreatedAt { get; set; }
        [DisplayName("Updated At")]
        public DateTime UpdatedAt { get; set; }
        [DisplayName("Customer Id")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public Feedback() { }
        public Feedback(string content, Customer customer) {
            Content = content;
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
            Customer = customer;
            CustomerId = Customer.Id;
        }
    }
}