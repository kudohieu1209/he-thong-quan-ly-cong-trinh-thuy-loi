using Microsoft.Extensions.Logging;
using Waterworks.Maui.Services;
using Waterworks.Maui.Views;

namespace Waterworks.Maui;

public static class MauiProgram
{
    public static IServiceProvider Services { get; private set; } = default!;

    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>();

        builder.Services.AddSingleton<ApiClient>();
        builder.Services.AddSingleton<SessionState>();
        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<CongTrinhListPage>();
        builder.Services.AddTransient<CongTrinhEditPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();
        Services = app.Services;
        return app;
    }
}
