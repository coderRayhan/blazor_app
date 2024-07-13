using EmployeeManagement.Domain.Common;
using EmployeeManagement.Domain.CustomValidators;
using EmployeeManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Domain.Entities;
public class Employee : BaseEntity
{
    [Required(ErrorMessage = "First name is required")]
    [MinLength(2)]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Last name is required")]
    public string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    [EmailDomainValidator]
    public string Email { get; set; } = string.Empty;
    public DateTime? DoB { get; set; }
    public Gender Gender { get; set; }
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    public string PhotoPath { get; set; } = string.Empty;
}
