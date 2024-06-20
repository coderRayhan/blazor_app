using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Web.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Components.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject]
        public IEmployeeService service { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Employees = await service.GetEmployeesAsync();
        }
    }
}
