using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Web.Interfaces;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EmployeeManagement.Web.Components.Pages
{
    public class EmployeeListBase : ComponentBase
    {
        [Inject]
        private FormDialogService DialogService { get; set; }
        [Inject]
        public IEmployeeService service { get; set; }
        public List<Employee> Employees { get; set; }
        public int SelectedEmployeeCount { get; set; }
        public EmployeeViewModel EmployeeViewModel { get; set; }
        public bool ShowFooter { get; set; } = true;
        private IDialogReference _dialogReference { get; set; }

        protected async Task LoadEmployeeList()
        {
            Employees = (await service.GetEmployeesAsync()).ToList();
            StateHasChanged();
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadEmployeeList();
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

        protected async void Employee_Deleted(int Id)
        {
            await LoadEmployeeList();
        }

        protected async Task ShowMyFormDialog()
        {
            var parameters = new DialogParameters();
            _dialogReference = await DialogService.ShowFormDialog<CreateEmployee, EmployeeViewModel>(parameters, OnFormSubmit, "Create Employee");
            
        }

        protected async Task OnFormSubmit(EmployeeViewModel formData)
        {
            await LoadEmployeeList();
            _dialogReference.Close(DialogResult.Ok(true));
        }
    }
}
