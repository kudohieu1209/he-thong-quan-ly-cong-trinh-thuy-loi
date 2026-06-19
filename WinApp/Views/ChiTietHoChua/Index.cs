using System;
using WinApp.Views;
using Models;

namespace WinApp.Views.ChiTietHoChua
{
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Chi tiết công trình Hồ chứa";

            context.TableColumns = new object[] {
                new TableColumn { Name = "TenCongTrinh", Caption = "Tên công trình", Width = 250 },

                new TableColumn { Name = "TongDungTich", Caption = "Tổng dung tích (m³)", Width = 150, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "DungTichHuuIch", Caption = "Dung tích hữu ích (m³)", Width = 150, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "MucNuocDangBinhThuong", Caption = "MNDBT (m)", Width = 100, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "DienTichMatNuoc", Caption = "DT mặt nước (m²)", Width = 150, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
            };

            context.Search = (e, s) => {
                return true;
            };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin chi tiết Hồ chứa";
            context.Editors = new object[] {
                new EditorInfo { Name = "CongTrinhId", Caption = "Công trình", Layout = 12, Required = true, Type = "select", ValueName = "Id", DisplayName = "TenCongTrinh", Options = Provider.Select<Models.CongTrinh>() },

                new EditorInfo { Name = "TongDungTich", Caption = "Tổng dung tích (m³)", Layout = 6 },
                new EditorInfo { Name = "DungTichHuuIch", Caption = "Dung tích hữu ích (m³)", Layout = 6 },
                new EditorInfo { Name = "DungTichChet", Caption = "Dung tích chết (m³)", Layout = 4 },
                new EditorInfo { Name = "MucNuocDangBinhThuong", Caption = "Mực nước dâng BT (m)", Layout = 4 },
                new EditorInfo { Name = "MucNuocLuThietKe", Caption = "Mực nước lũ TK (m)", Layout = 4 },
                new EditorInfo { Name = "DienTichMatNuoc", Caption = "Diện tích mặt nước (m²)", Layout = 12 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("TongDungTich"); 
            Find("CongTrinhId", c => c.IsEnabled = false); 
        }
    }
}