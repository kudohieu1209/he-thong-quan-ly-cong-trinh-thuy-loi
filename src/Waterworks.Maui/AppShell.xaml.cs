using Waterworks.Maui.Views;

namespace Waterworks.Maui;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(CongTrinhEditPage), typeof(CongTrinhEditPage));
    }
}
