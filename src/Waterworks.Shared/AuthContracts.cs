namespace Waterworks.Shared;

public sealed record LoginRequest(string? UserName, string? Password);

public sealed record LoginResponse(
    string UserName,
    string Role,
    string? DisplayName,
    string AccessToken);

public sealed record UserInfoDto(
    string UserName,
    string Role,
    string? DisplayName);
