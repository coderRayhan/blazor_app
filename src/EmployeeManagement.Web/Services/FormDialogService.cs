using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EmployeeManagement.Web.Services;
public class FormDialogService(IDialogService dialogService)
{
    public async Task ShowFormDialog<TComponent>(DialogParameters parameters) where TComponent : ComponentBase
    {
        var dialog = dialogService.Show<TComponent>("Form Dialog", parameters);

        var result = await dialog.Result;

        if (!result.Canceled && result.Data is not null)
        {
            Console.WriteLine("Confirmed");
        }
    }
}