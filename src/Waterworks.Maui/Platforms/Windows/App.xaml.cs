namespace Waterworks.Maui.WinUI;

public partial class App : MauiWinUIApplication
{
    public App()
    {
        UnhandledException += (_, args) => LogCrash(args.Exception);
        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
        {
            if (args.ExceptionObject is Exception exception)
            {
                LogCrash(exception);
            }
        };

        InitializeComponent();
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    private static void LogCrash(Exception exception)
    {
        var logPath = Path.Combine(Path.GetTempPath(), "waterworks-maui-crash.log");
        File.AppendAllText(logPath, $"{DateTimeOffset.Now:u}{Environment.NewLine}{exception}{Environment.NewLine}{Environment.NewLine}");
    }
}
