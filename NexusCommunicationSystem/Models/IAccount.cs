using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusCommunicationSystem.Models
{
    public interface IAccount
    {
        [Key]
         int Id { get; set; }
        [Required]
        [DisplayName("Account Id")]
        string AccountId { get; set; }
        [Required]
        [DisplayName("Firt Name")]
        string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        string LastName { get; set; }
        [Required]
         string Email { get; set; }
        [Required]
        [DisplayName("Password")]
        string UserPassword { get; set; }
        [Required]
        [DisplayName("User Role")]
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
