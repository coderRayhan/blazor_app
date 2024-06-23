using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Web.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace EmployeeManagement.Web.Components.Pages;
public class EmployeeDetailsBase : ComponentBase
{
    public Employee? Employee { get; set; }
    [Parameter]
    public string? Id { get; set; }
    [Inject]
    private IEmployeeService service { get; set; }
    protected string Coordinates { get; set; }
    protected string ButtonText { get; set; } = "Hide Button";
    protected string ClassName { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Employee = await service.GetEmployeeByIdAsync(int.Parse(Id));
    }

    protected void Mouse_Move(MouseEventArgs e){
        Coordinates = $"X => {e.ClientX} & Y => {e.ClientY}";
        StateHasChanged();
    }

    public void Show_Hide(MouseEventArgs e)
    {
        if(ButtonText == "Hide Button")
        {
            ButtonText = "Show Button";
            ClassName = "d-none";
        }
        else
        {
            ButtonText = "Hide Button";
            ClassName = null;
        }
    }
}