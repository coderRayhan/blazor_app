using Microsoft.AspNetCore.Components;

namespace CustomComponent
{
    public class ConfirmModalBase : ComponentBase
    {
        public bool ShowConfirmDialog { get; set; } = false;
        public string ConfirmButtonText { get; set; } = "Ok";
        public string ModalBodyText { get; set; } = string.Empty;
        public string ModalHeaderText { get; set; } = "Modal Title";
        [Parameter]
        public EventCallback<bool> OnConfirmationChange { get; set; } = new();
        public void Show_Dialog()
        {
            ShowConfirmDialog = true;
            StateHasChanged();
        }
        // public void Close_Modal()
        // {
        //     ShowConfirmDialog = false;
        //     StateHasChanged();
        // }

        public async void ConfirmationChanged(bool value){
            ShowConfirmDialog = false;
            await OnConfirmationChange.InvokeAsync(value);
        }
    }
}
