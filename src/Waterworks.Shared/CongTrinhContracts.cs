namespace Waterworks.Shared;

public sealed record CongTrinhDto(
    int Id,
    string? TenCongTrinh,
    string? MaHieu,
    int? CapCongTrinhId,
    int? LoaiCongTrinhId,
    int? DonViQuanLyId,
    int? DonViHanhChinhId,
    string? DiaDiem,
    string? DuLieuGIS,
    int? NamXayDung,
    string? MoTa,
    string? TrangThai,
    string? HinhAnh,
    string? LoaiCongTrinh,
    string? CapCongTrinh,
    string? DonViQuanLy,
    string? DonViHanhChinh);

public sealed record CongTrinhUpsertRequest(
    string? TenCongTrinh,
    string? MaHieu,
    int? CapCongTrinhId,
    int? LoaiCongTrinhId,
    int? DonViQuanLyId,
    int? DonViHanhChinhId,
    string? DiaDiem,
    string? DuLieuGIS,
    int? NamXayDung,
    string? MoTa,
    string? TrangThai,
    string? HinhAnh);

public sealed record LookupItemDto(int Id, string Name);
