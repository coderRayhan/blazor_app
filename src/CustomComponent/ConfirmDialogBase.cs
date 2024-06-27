using Microsoft.AspNetCore.Components;

namespace CustomComponent
{
    public class ConfirmDialogBase : ComponentBase
    {
        public bool ShowConfirmDialog { get; set; } = false;
        public string ConfirmButtonText { get; set; } = "Ok";
    }
}
