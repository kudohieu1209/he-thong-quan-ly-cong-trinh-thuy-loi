using Waterworks.Shared;

namespace Waterworks.Web.Services;

public sealed class SessionState
{
    public LoginResponse? CurrentUser { get; private set; }

    public bool IsAuthenticated => CurrentUser is not null;

    public void SetUser(LoginResponse user) => CurrentUser = user;

    public void Clear() => CurrentUser = null;
}
