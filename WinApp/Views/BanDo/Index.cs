using System;
using System.Collections.Generic;
using System.Windows.Forms.Integration;

namespace WinApp.Views.BanDo
{
    public class Index : BaseView
    {
        private MapControl _mapControl;
        private System.Windows.Forms.Integration.WindowsFormsHost _host;

        protected override object CreateView()
        {
            try
            {

                _mapControl = new MapControl();
                _host = new System.Windows.Forms.Integration.WindowsFormsHost();
                _host.Child = _mapControl;

                System.Diagnostics.Debug.WriteLine("BanDo.Index: View created successfully");

                return _host;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BanDo.Index CreateView Error: {ex.Message}");
                throw;
            }
        }

        protected override void RenderCore(ViewContext context)
        {
            try
            {
                base.RenderCore(context);
                context.Title = "Bản đồ phân bố công trình";

                // Load dữ liệu
                if (context.Model is List<Models.CongTrinh> data)
                {
                    System.Diagnostics.Debug.WriteLine($"BanDo.Index: Rendering {data.Count} công trình");
                    _mapControl?.LoadDuLieu(data);
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("BanDo.Index: No data or wrong model type");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BanDo.Index RenderCore Error: {ex.Message}");
                System.Windows.MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
            }
        }
    }
}