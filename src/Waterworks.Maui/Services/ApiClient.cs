using System.Net.Http.Json;
using Waterworks.Shared;

namespace Waterworks.Maui.Services;

public sealed class ApiClient
{
    private readonly HttpClient _httpClient = new()
    {
        BaseAddress = new Uri(DefaultBaseAddress)
    };

    public static string DefaultBaseAddress =>
        DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5178"
            : "http://127.0.0.1:5178";

    public async Task<LoginResponse?> LoginAsync(string userName, string password, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "/api/auth/login",
            new LoginRequest(userName, password),
            cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken);
    }

    public async Task<IReadOnlyList<MenuGroupDto>> GetMenuAsync(CancellationToken cancellationToken = default) =>
        await _httpClient.GetFromJsonAsync<IReadOnlyList<MenuGroupDto>>("/api/menu", cancellationToken)
        ?? [];

    public async Task<IReadOnlyList<CongTrinhDto>> GetCongTrinhAsync(CancellationToken cancellationToken = default) =>
        await _httpClient.GetFromJsonAsync<IReadOnlyList<CongTrinhDto>>("/api/congtrinh/", cancellationToken)
        ?? [];

    public Task<CongTrinhDto?> GetCongTrinhByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _httpClient.GetFromJsonAsync<CongTrinhDto>($"/api/congtrinh/{id}", cancellationToken);

    public async Task SaveCongTrinhAsync(int? id, CongTrinhUpsertRequest request, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response = id is null or 0
            ? await _httpClient.PostAsJsonAsync("/api/congtrinh/", request, cancellationToken)
            : await _httpClient.PutAsJsonAsync($"/api/congtrinh/{id}", request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCongTrinhAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"/api/congtrinh/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
