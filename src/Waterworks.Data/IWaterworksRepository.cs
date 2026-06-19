using Waterworks.Shared;

namespace Waterworks.Data;

public interface IWaterworksRepository
{
    Task InitializeAsync(CancellationToken cancellationToken = default);
    Task<UserInfoDto?> ValidateLoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CongTrinhDto>> GetCongTrinhAsync(CancellationToken cancellationToken = default);
    Task<CongTrinhDto?> GetCongTrinhByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<CongTrinhDto> CreateCongTrinhAsync(CongTrinhUpsertRequest request, CancellationToken cancellationToken = default);
    Task<bool> UpdateCongTrinhAsync(int id, CongTrinhUpsertRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteCongTrinhAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LookupItemDto>> GetLoaiCongTrinhAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LookupItemDto>> GetCapCongTrinhAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LookupItemDto>> GetDonViAsync(CancellationToken cancellationToken = default);
}
