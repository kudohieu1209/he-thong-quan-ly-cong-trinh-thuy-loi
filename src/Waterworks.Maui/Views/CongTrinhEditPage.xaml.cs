using Waterworks.Maui.Services;
using Waterworks.Shared;

namespace Waterworks.Maui.Views;

public partial class CongTrinhEditPage : ContentPage, IQueryAttributable
{
    private readonly ApiClient _apiClient;
    private int? _id;

    public CongTrinhEditPage()
        : this(MauiProgram.Services.GetRequiredService<ApiClient>())
    {
    }

    public CongTrinhEditPage(ApiClient apiClient)
    {
        InitializeComponent();
        _apiClient = apiClient;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("id", out var rawId) || !int.TryParse(rawId?.ToString(), out var id))
        {
            _id = null;
            Title = "Them cong trinh";
            return;
        }

        _id = id;
        Title = "Sua cong trinh";
        var item = await _apiClient.GetCongTrinhByIdAsync(id);
        if (item is null)
        {
            MessageLabel.Text = "Khong tim thay cong trinh.";
            return;
        }

        TenEntry.Text = item.TenCongTrinh;
        MaHieuEntry.Text = item.MaHieu;
        DiaDiemEntry.Text = item.DiaDiem;
        NamEntry.Text = item.NamXayDung?.ToString();
        TrangThaiEntry.Text = item.TrangThai;
        MoTaEditor.Text = item.MoTa;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        MessageLabel.Text = string.Empty;

        if (string.IsNullOrWhiteSpace(TenEntry.Text))
        {
            MessageLabel.Text = "Ten cong trinh la bat buoc.";
            return;
        }

        int? namXayDung = int.TryParse(NamEntry.Text, out var parsedYear) ? parsedYear : null;
        var request = new CongTrinhUpsertRequest(
            TenEntry.Text,
            MaHieuEntry.Text,
            CapCongTrinhId: null,
            LoaiCongTrinhId: null,
            DonViQuanLyId: null,
            DonViHanhChinhId: null,
            DiaDiemEntry.Text,
            DuLieuGIS: null,
            namXayDung,
            MoTaEditor.Text,
            TrangThaiEntry.Text,
            HinhAnh: null);

        try
        {
            await _apiClient.SaveCongTrinhAsync(_id, request);
            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {
            MessageLabel.Text = $"Khong luu duoc: {ex.Message}";
        }
    }
}
