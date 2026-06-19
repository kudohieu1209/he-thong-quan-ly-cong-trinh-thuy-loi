using System;
using WinApp.Views;
using Models;

namespace WinApp.Views.ChiTietDuongOng
{
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Chi tiết công trình Đường ống";

            context.TableColumns = new object[] {
                new TableColumn { Name = "TenCongTrinh", Caption = "Tên công trình", Width = 250 },

                new TableColumn { Name = "ChieuDai", Caption = "Chiều dài (m)", Width = 150, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "DuongKinh", Caption = "Đường kính (mm)", Width = 150, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "VatLieu", Caption = "Vật liệu", Width = 200 },
            };

            context.Search = (e, s) => { return true; };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin chi tiết Đường ống";
            context.Editors = new object[] {
                new EditorInfo { Name = "CongTrinhId", Caption = "Công trình", Layout = 12, Required = true, Type = "select", ValueName = "Id", DisplayName = "TenCongTrinh", Options = Provider.Select<Models.CongTrinh>() },

                new EditorInfo { Name = "ChieuDai", Caption = "Chiều dài (m)", Layout = 6 },
                new EditorInfo { Name = "DuongKinh", Caption = "Đường kính (mm)", Layout = 6 },
                new EditorInfo { Name = "VatLieu", Caption = "Vật liệu", Layout = 12 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("VatLieu");
            Find("CongTrinhId", c => c.IsEnabled = false);
        }
    }
}