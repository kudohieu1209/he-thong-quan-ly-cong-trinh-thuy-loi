using System.Net.Http.Json;
using Waterworks.Shared;

namespace Waterworks.Web.Services;

public sealed class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<LoginResponse?> LoginAsync(string userName, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/auth/login", new LoginRequest(userName, password));
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<IReadOnlyList<MenuGroupDto>> GetMenuAsync() =>
        await _httpClient.GetFromJsonAsync<IReadOnlyList<MenuGroupDto>>("/api/menu") ?? [];

    public async Task<IReadOnlyList<CongTrinhDto>> GetCongTrinhAsync() =>
        await _httpClient.GetFromJsonAsync<IReadOnlyList<CongTrinhDto>>("/api/congtrinh/") ?? [];

    public Task<CongTrinhDto?> GetCongTrinhByIdAsync(int id) =>
        _httpClient.GetFromJsonAsync<CongTrinhDto>($"/api/congtrinh/{id}");

    public async Task SaveCongTrinhAsync(int? id, CongTrinhUpsertRequest request)
    {
        HttpResponseMessage response = id is null or 0
            ? await _httpClient.PostAsJsonAsync("/api/congtrinh/", request)
            : await _httpClient.PutAsJsonAsync($"/api/congtrinh/{id}", request);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteCongTrinhAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/congtrinh/{id}");
        response.EnsureSuccessStatusCode();
    }
}
