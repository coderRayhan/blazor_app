using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EmployeeManagement.Web.Services;
public class FormDialogService(IDialogService dialogService)
{
    public async Task<IDialogReference> ShowFormDialog<TComponent>(DialogParameters parameters, string dialogTitle = "Form Dialog") where TComponent : ComponentBase
    {
        var dialog = dialogService.Show<TComponent>(dialogTitle, parameters);

        var result = await dialog.Result;

        if (!result.Canceled && result.Data is not null)
        {
            Console.WriteLine("Confirmed");
        }

        return dialog;
    }

    public void CloseFormDialog(IDialogReference dialogReference)
    {
        dialogService.Close((DialogReference)dialogReference);
    }
}