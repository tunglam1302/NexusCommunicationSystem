using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCommunicationSystem.Models
{
    public interface IAccount
    {
         int Id { get; set; }
        [Required]
         string FirstName { get; set; }
        [Required]
         string LastName { get; set; }
        [Required]
         string Email { get; set; }
        [Required]
         string UserPassword { get; set; }
         string FeedBack { get; set; }
        [Required]
         AccountRole UserRole { get; set; }
    }
    public enum AccountRole
    {
        User = 1,
        Admin = 2,
        AccountDepartment = 3,
        EmployeeOfRetailOutlet = 4,
        TechnicalPeople = 5
    }
}
