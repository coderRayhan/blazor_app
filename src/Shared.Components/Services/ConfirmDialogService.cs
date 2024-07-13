using MudBlazor;
using Shared.Components.Components.Common;

namespace Shared.Components.Services;
public class ConfirmDialogService(IDialogService dialogService)
{
    public async Task<IDialogReference?> Show_Dialog(string contentText, string titleText = "Confirmation", string buttonText = "Yes", Color color = Color.Error)
    {
        var parameters = new DialogParameters<ConfirmDialog>
        {
            { x => x.ContentText, contentText },
            { x => x.TitleText, titleText },
            { x => x.ButtonText, buttonText },
            { x => x.Color, color }
        };

        var options = new DialogOptions() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };
        var dialog = dialogService.Show<ConfirmDialog>("Delete", parameters, options);
        return await Task.FromResult(dialog);
    }
}
