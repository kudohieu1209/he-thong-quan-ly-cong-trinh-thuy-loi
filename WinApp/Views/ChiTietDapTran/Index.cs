using System;
using WinApp.Views;
using Models;

namespace WinApp.Views.ChiTietDapTran
{
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Chi tiết công trình Đập tràn";

            context.TableColumns = new object[] {
                new TableColumn { Name = "TenCongTrinh", Caption = "Tên công trình", Width = 250 },

                new TableColumn { Name = "ChieuDaiDap", Caption = "Chiều dài đập (m)", Width = 120, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "ChieuCaoDap", Caption = "Chiều cao đập (m)", Width = 120, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "CaoTrinhNguongTran", Caption = "Cao trình ngưỡng (m)", Width = 150, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "KetCauDap", Caption = "Kết cấu đập", Width = 150 },
            };

            context.Search = (e, s) => { return true; };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin chi tiết Đập tràn";
            context.Editors = new object[] {
                new EditorInfo { Name = "CongTrinhId", Caption = "Công trình", Layout = 12, Required = true, Type = "select", ValueName = "Id", DisplayName = "TenCongTrinh", Options = Provider.Select<Models.CongTrinh>() },

                new EditorInfo { Name = "ChieuDaiDap", Caption = "Chiều dài đập (m)", Layout = 6 },
                new EditorInfo { Name = "ChieuCaoDap", Caption = "Chiều cao đập (m)", Layout = 6 },
                new EditorInfo { Name = "CaoTrinhNguongTran", Caption = "Cao trình ngưỡng tràn (m)", Layout = 6 },
                new EditorInfo { Name = "HinhThucTieuNang", Caption = "Hình thức tiêu năng", Layout = 6 },
                new EditorInfo { Name = "KetCauDap", Caption = "Kết cấu đập", Layout = 12 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("KetCauDap");
            Find("CongTrinhId", c => c.IsEnabled = false);
        }
    }
}