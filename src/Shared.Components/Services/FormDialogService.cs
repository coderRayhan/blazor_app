using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared.Components.Components;
using Shared.Components.Components.Common;

namespace Shared.Components.Services
{
    public class FormDialogService
    {
        private readonly IDialogService _dialogService;
        public FormDialogService(IDialogService dialogService)
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
                BackdropClick = true,
            };

            return _dialogService.Show<FormDialog>(title, parameters, options);
        }
    }
}
