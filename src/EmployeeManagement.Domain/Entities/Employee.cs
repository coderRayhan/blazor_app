using EmployeeManagement.Domain.Common;
using EmployeeManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities;
public class Employee : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime DoB { get; set; }
    public Gender Gender { get; set; }
    public int DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    public string PhotoPath { get; set; } = string.Empty;
}
