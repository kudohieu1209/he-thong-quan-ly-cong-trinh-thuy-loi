using System.Collections.ObjectModel;
using Waterworks.Maui.Services;
using Waterworks.Shared;

namespace Waterworks.Maui.Views;

public partial class CongTrinhListPage : ContentPage
{
    private readonly ApiClient _apiClient;
    private readonly ObservableCollection<CongTrinhDto> _items = [];

    public CongTrinhListPage()
        : this(MauiProgram.Services.GetRequiredService<ApiClient>())
    {
    }

    public CongTrinhListPage(ApiClient apiClient)
    {
        InitializeComponent();
        _apiClient = apiClient;
        CongTrinhCollection.ItemsSource = _items;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        _items.Clear();
        foreach (var item in await _apiClient.GetCongTrinhAsync())
        {
            _items.Add(item);
        }
    }

    private async void OnAddClicked(object sender, EventArgs e) =>
        await Shell.Current.GoToAsync(nameof(CongTrinhEditPage));

    private async void OnRefreshClicked(object sender, EventArgs e) => await LoadAsync();

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as CongTrinhDto;
        if (selected is null)
        {
            return;
        }

        CongTrinhCollection.SelectedItem = null;
        await Shell.Current.GoToAsync($"{nameof(CongTrinhEditPage)}?id={selected.Id}");
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        if (sender is not Button button || button.CommandParameter is not int id)
        {
            return;
        }

        var confirm = await DisplayAlert("Xoa cong trinh", "Ban chac chan muon xoa cong trinh nay?", "Xoa", "Huy");
        if (!confirm)
        {
            return;
        }

        await _apiClient.DeleteCongTrinhAsync(id);
        await LoadAsync();
    }
}
