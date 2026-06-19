using Waterworks.Shared;

namespace Waterworks.Maui.Services;

public sealed class SessionState
{
    public LoginResponse? CurrentUser { get; set; }
}
