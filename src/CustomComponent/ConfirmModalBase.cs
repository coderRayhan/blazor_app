using Microsoft.AspNetCore.Components;

namespace CustomComponent
{
    public class ConfirmModalBase : ComponentBase
    {
        public bool ShowConfirmDialog { get; set; } = false;
        public string ConfirmButtonText { get; set; } = "Ok";
        public void Show_Dialog()
        {
            ShowConfirmDialog = true;
            StateHasChanged();
        }
    }
}
