using EmployeeManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Domain.Entities
{
    public class Department : BaseEntity
    {
        [Required]
        public string DepartmentName { get; set; } = string.Empty;
    }
}
