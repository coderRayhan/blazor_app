using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace EmployeeManagement.Web.Services;
public class FormDialogService(IDialogService dialogService)
{
    public async Task<IDialogReference> ShowFormDialog<TComponent, TResponse>(DialogParameters parameters, Func<TResponse, Task> action, string dialogTitle = "Form Dialog") where TComponent : ComponentBase
    {
        parameters.Add("OnSubmit", EventCallback.Factory.Create<TResponse>(this, action ));
        var options = new DialogOptions
        {
            ClassBackground = "dialog_backdrop",
            CloseButton = true,
            DisableBackdropClick = true,
            FullWidth = true,
            MaxWidth = MaxWidth.Small,
            Position = DialogPosition.Center
        };
        var dialog = await dialogService.ShowAsync<TComponent>(dialogTitle, parameters, options);

        var result = await dialog.Result;

        if (!result.Canceled && result.Data is not null)
        {
            Console.WriteLine("Confirmed");
        }

        return dialog;
    }

    public void CloseFormDialog(IDialogReference dialogReference)
    {
        dialogReference.Close(DialogResult.Ok(true));
    }
}