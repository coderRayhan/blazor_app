using Microsoft.Extensions.DependencyInjection;
using Shared.Components.Services;

namespace Shared.Components;
public static class DependencyInjection
{
    public static void AddComponentServices(this IServiceCollection services)
    {
        services.AddScoped<RSnackbarService>();
        services.AddScoped<FormDialogService>();
        services.AddScoped<ConfirmDialogService>();
    }
}
