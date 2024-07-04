using EmployeeManagement.Web.Components.Shared;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Reflection;

namespace EmployeeManagement.Web.Services
{
    public class FormDialogService2
    {
        private readonly IDialogService _dialogService;
        public FormDialogService2(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public IDialogReference ShowFormDialog<TForm, TModel>(string title, object model) where TForm : ComponentBase
        {
            var parameters = new DialogParameters
        {
            { "Body", (RenderFragment)((builder) =>
                {
                    builder.OpenComponent(0, typeof(TForm));
                    builder.AddAttribute(1, "Model", model);
                    builder.CloseComponent();
                })
            }
        };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true,
                DisableBackdropClick = true,
            };

            return _dialogService.Show<FormDialog>(title, parameters, options);
        }
    }
}
