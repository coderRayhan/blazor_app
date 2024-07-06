using MudBlazor;

namespace EmployeeManagement.UI.Services;
public class RSnackbarService(ISnackbar snackbar){
    public void Show_Snackbar(string message, Severity severity = Severity.Normal, Action<SnackbarOptions> options = null){
        snackbar.Add(message, severity, options);
    }
}