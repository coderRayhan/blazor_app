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
        public int SelectedEmployeeCount { get; set; }
        public bool ShowFooter { get; set; } = true;
        protected override async Task OnInitializedAsync()
        {
            Employees = await service.GetEmployeesAsync();
        }

        protected void Child_Select_Changed(bool val)
        {
            if (val)
            {
                SelectedEmployeeCount++;
            }
            else
            {
                SelectedEmployeeCount--;
            }
        }
    }
}
