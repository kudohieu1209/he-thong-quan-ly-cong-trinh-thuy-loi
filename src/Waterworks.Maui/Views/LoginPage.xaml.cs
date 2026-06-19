using Waterworks.Maui.Services;

namespace Waterworks.Maui.Views;

public partial class LoginPage : ContentPage
{
    private readonly ApiClient _apiClient;
    private readonly SessionState _session;

    public LoginPage()
        : this(
            MauiProgram.Services.GetRequiredService<ApiClient>(),
            MauiProgram.Services.GetRequiredService<SessionState>())
    {
    }

    public LoginPage(ApiClient apiClient, SessionState session)
    {
        InitializeComponent();
        _apiClient = apiClient;
        _session = session;
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        MessageLabel.Text = string.Empty;

        try
        {
            var user = await _apiClient.LoginAsync(UserNameEntry.Text ?? string.Empty, PasswordEntry.Text ?? string.Empty);
            if (user is null)
            {
                MessageLabel.Text = "Sai ten dang nhap hoac mat khau.";
                return;
            }

            _session.CurrentUser = user;
            await Shell.Current.GoToAsync("//home");
        }
        catch (Exception ex)
        {
            MessageLabel.Text = $"Khong ket noi duoc API: {ex.Message}";
        }
    }
}
