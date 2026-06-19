using Waterworks.Maui.Services;

namespace Waterworks.Maui.Views;

public partial class HomePage : ContentPage
{
    private readonly ApiClient _apiClient;
    private readonly SessionState _session;

    public HomePage()
        : this(
            MauiProgram.Services.GetRequiredService<ApiClient>(),
            MauiProgram.Services.GetRequiredService<SessionState>())
    {
    }

    public HomePage(ApiClient apiClient, SessionState session)
    {
        InitializeComponent();
        _apiClient = apiClient;
        _session = session;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        WelcomeLabel.Text = _session.CurrentUser is null
            ? "Chua dang nhap"
            : $"Xin chao {_session.CurrentUser.UserName} ({_session.CurrentUser.Role})";

        MenuLayout.Clear();
        try
        {
            var groups = await _apiClient.GetMenuAsync();
            foreach (var group in groups)
            {
                MenuLayout.Add(new Label
                {
                    Text = group.Title,
                    FontAttributes = FontAttributes.Bold,
                    Margin = new Thickness(0, 8, 0, 0)
                });

                foreach (var item in group.Items)
                {
                    MenuLayout.Add(new Label
                    {
                        Text = $"  - {item.Title}",
                        TextColor = Colors.DarkSlateGray
                    });
                }
            }
        }
        catch (Exception ex)
        {
            MenuLayout.Add(new Label { Text = $"Khong tai duoc menu: {ex.Message}", TextColor = Colors.DarkRed });
        }
    }

    private async void OnOpenCongTrinhClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync("//congtrinh");
}
