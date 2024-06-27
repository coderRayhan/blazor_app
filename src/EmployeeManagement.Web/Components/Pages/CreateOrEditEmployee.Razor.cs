using AutoMapper;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Web.Interfaces;
using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Components.Pages
{
    public partial class CreateOrEditEmployee
    {
        public string PageTitle { get; set; }
        [Inject]
        private IMapper _mapper { get; set; }
        private Employee Employee { get; set; } = new Employee();
        public EmployeeViewModel EmployeeViewModel { get; set; } = new EmployeeViewModel();
        [Inject]
        public IEmployeeService employeeService { get; set; }
        [Inject]
        public IDepartmentService departmentService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Id { get; set; }
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
        protected override async Task OnInitializedAsync()
        {
            int.TryParse(Id, out int id);

            if (id != 0)
            {
                Employee = await employeeService.GetEmployeeByIdAsync(int.Parse(Id));
                PageTitle = "Edit Employee";
            }
            else PageTitle = "Create Employee";
            EmployeeViewModel = _mapper.Map<EmployeeViewModel>(Employee);

            Departments = await departmentService.GetDepartmentsAsync();
        }

        public async Task OnValidSubmitAsync()
        {
            Employee result = new();
            var employee = _mapper.Map<Employee>(EmployeeViewModel);
            if(employee.Id != 0) result = await employeeService.UpdateEmployeeAsync(employee);

            else result = await employeeService.CreateEmployeeAsync(employee);

            if (result is not null) NavigateToEmployeeList();
        }

        public async Task OnDeleteAsync()
        {
            await employeeService.DeleteEmployeeAsync(EmployeeViewModel.Id);

            NavigateToEmployeeList();
        }

        public void NavigateToEmployeeList()
        {
            NavigationManager.NavigateTo("/employeelist");
        }
    }
}
