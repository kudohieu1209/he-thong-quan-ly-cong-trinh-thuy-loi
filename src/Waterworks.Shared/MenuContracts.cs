namespace Waterworks.Shared;

public sealed record MenuGroupDto(string Key, string Title, IReadOnlyList<MenuItemDto> Items);

public sealed record MenuItemDto(string Key, string Title, string Route);
