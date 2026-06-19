using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Views.ChiTietTramBom
{
    using Vst.Controls;
    using Models;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Chi tiết công trình Trạm bơm";
            context.TableColumns = new object[] {
                new TableColumn { Name = "TenCongTrinh", Caption = "Tên công trình", Width = 250 },
                new TableColumn { Name = "SoMayBom", Caption = "Số máy bơm", Width = 100, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "CongSuatMay", Caption = "Công suất (kW)", Width = 120, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "LuuLuongThietKe", Caption = "Lưu lượng TK (m³/s)", Width = 150, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "CotNuocThietKe", Caption = "Cột nước TK (m)", Width = 130, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
            };
            context.Search = null;
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin chi tiết Trạm bơm";
            context.Editors = new object[] {
                new EditorInfo { Name = "CongTrinhId", Caption = "Công trình", Layout = 12, Required = true, Type = "select", ValueName = "Id", DisplayName = "TenCongTrinh", Options = Provider.Select<CongTrinh>() },
                new EditorInfo { Name = "SoMayBom", Caption = "Số máy bơm", Layout = 3 },
                new EditorInfo { Name = "CongSuatMay", Caption = "Công suất máy (kW)", Layout = 3 },
                new EditorInfo { Name = "LuuLuongThietKe", Caption = "Lưu lượng thiết kế (m³/s)", Layout = 3 },
                new EditorInfo { Name = "CotNuocThietKe", Caption = "Cột nước thiết kế (m)", Layout = 3 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("SoMayBom");
            Find("CongTrinhId", c => c.IsEnabled = false);
        }
    }
}
