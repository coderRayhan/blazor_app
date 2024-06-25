using AutoMapper;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Web.Interfaces;
using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Components.Pages
{
    public partial class EditEmployee
    {
        [Inject]
        private IMapper _mapper { get; set; }
        private Employee Employee { get; set; } = new Employee();
        public EmployeeViewModel EmployeeViewModel { get; set; } = new EmployeeViewModel();
        [Inject]
        public IEmployeeService employeeService { get; set; }
        [Inject]
        public IDepartmentService departmentService { get; set; }
        [Parameter]
        public string Id { get; set; }
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
        protected override async Task OnInitializedAsync()
        {
            Employee = await employeeService.GetEmployeeByIdAsync(int.Parse(Id));
            EmployeeViewModel = _mapper.Map<EmployeeViewModel>(Employee);
            Departments = await departmentService.GetDepartmentsAsync();
        }
    }
}
