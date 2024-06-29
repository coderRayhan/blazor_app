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
        public IDialogService DialogService { get; set; }
        [Inject]
        public IEmployeeService service { get; set; }
        public List<Employee> Employees { get; set; }
        public int SelectedEmployeeCount { get; set; }
        public EmployeeViewModel EmployeeViewModel { get; set; }
        public bool ShowFooter { get; set; } = true;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Employees = (await service.GetEmployeesAsync()).ToList();
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

        protected async void Delete_Employee(int Id)
        {
            Employees = (await service.GetEmployeesAsync()).ToList();
            StateHasChanged();
        }

        //calling dynamic modal
        public async void Add_Employee()
        {
            var parameters = new DialogParameters<FormDialog>();
            // parameters.Add(x => x.ContentText, "Do you really want to delete these records? This process cannot be undone.");
            parameters.Add(x => x.Content, (RenderFragment)(builder =>
            {
                builder.OpenComponent(0, typeof(CreateEmployee));
                builder.CloseComponent();
                builder.AddAttribute(1, "OnSubmit", EventCallback.Factory.Create<EmployeeViewModel>(this, HandleSubmit));
                builder.AddAttribute(2, "OnCancel", EventCallback.Factory.Create(this, HandleCancel));
            }));
            parameters.Add(x => x.TitleText, "Delete Employee.");
            parameters.Add(x => x.Color, Color.Error);

            var dialog = DialogService.Show<FormDialog>("Dynamic Dialog", parameters);
            var result = await dialog.Result;
        }

        private void HandleSubmit(EmployeeViewModel value)
        {
            EmployeeViewModel = value;
            MudDialog.Close(DialogResult.Ok(true));
        }

        private void HandleCancel()
        {
            MudDialog.Cancel();
        }
    }
}
