using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace EmployeeManagement.Web.Components.Pages;
public partial class EmployeeDetailsBase : ComponentBase
{
    public Employee? Employee { get; set; }
    [Parameter]
    public string? Id { get; set; }
    [Inject]
    private IEmployeeService service { get; set; }
    protected string Coordinates { get; set; } = string.Empty;
    protected override async Task OnInitializedAsync()
    {
        Employee = await service.GetEmployeeByIdAsync(int.Parse(Id));
    }

    protected void Mouse_Move(MouseEventArgs e){
        Coordinates = $"X => {e.ClientX} & Y => {e.ClientY}";
    }
}