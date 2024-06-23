using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Web.Interfaces;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Components.Pages
{
    public partial class EditEmployee
    {
        public Employee Employee { get; set; } = new Employee();
        [Inject]
        public IEmployeeService employeeService { get; set; }
        [Inject]
        public IDepartmentService departmentService { get; set; }
        [Parameter]
        public string Id { get; set; }
        public string Gender { get; set; }
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
        public string DepartmentId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Employee = await employeeService.GetEmployeeByIdAsync(int.Parse(Id));
            Gender = Employee.Gender.ToString();
            Departments = await departmentService.GetDepartmentsAsync();
            DepartmentId = Employee.DepartmentId.ToString();
        }
    }
}
