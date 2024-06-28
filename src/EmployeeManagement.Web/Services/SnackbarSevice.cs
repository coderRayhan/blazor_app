using MudBlazor;

namespace EmployeeManagement.Web.Services;
public class SnackbarSevice(ISnackbar snackbar){
    public void Show_Snackbar(string message, Severity severity = Severity.Normal, Action<SnackbarOptions> options = null){
        snackbar.Add(message, severity, options);
    }
}