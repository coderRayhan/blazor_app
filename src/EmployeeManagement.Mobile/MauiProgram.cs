using EmployeeManagement.Services.Implementations;
using EmployeeManagement.Services.Interfaces;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;
using Shared.Components.Services;

namespace EmployeeManagement.Mobile;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();
        // Add MudBlazor services
        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
            config.SnackbarConfiguration.BackgroundBlurred = true;
            config.SnackbarConfiguration.ShowTransitionDuration = 300;
            config.SnackbarConfiguration.HideTransitionDuration = 300;
        });
        //builder.Services.AddHttpClient("blazorHttp", (client) =>
        //{
        //    client.BaseAddress = new Uri("http://localhost:5260");
        //});
#if WINDOWS
        builder.Services.AddSingleton(_ => new HttpClient
        {
        BaseAddress = new Uri("http://localhost:5260")
        });
#else
        builder.Services.AddSingleton(_ => new HttpClient
        {
            BaseAddress = new Uri("http://10.0.2.2:5260")
        });
#endif
        builder.Services.AddTransient<IEmployeeService, EmployeeServiceMAUI>();
        builder.Services.AddTransient<IDepartmentService, DepartmentServiceMAUI>();
        builder.Services.AddScoped<RSnackbarService>();
        builder.Services.AddScoped<FormDialogService>();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
