using System;
using WinApp.Views;
using Models;

namespace WinApp.Views.ChiTietKenhMuong
{
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Chi tiết công trình Kênh mương";

            context.TableColumns = new object[] {
                new TableColumn { Name = "TenCongTrinh", Caption = "Tên công trình", Width = 250 },

                new TableColumn { Name = "ChieuDai", Caption = "Chiều dài (m)", Width = 100, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "ChieuRong", Caption = "Chiều rộng (m)", Width = 100, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "ChieuCao", Caption = "Chiều cao (m)", Width = 100, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "LuuLuong", Caption = "Lưu lượng (m³/s)", Width = 120, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "KetCau", Caption = "Kết cấu", Width = 150 },
            };

            context.Search = (e, s) => { return true; };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin chi tiết Kênh mương";
            context.Editors = new object[] {
                new EditorInfo { Name = "CongTrinhId", Caption = "Công trình", Layout = 12, Required = true, Type = "select", ValueName = "Id", DisplayName = "TenCongTrinh", Options = Provider.Select<Models.CongTrinh>() },

                new EditorInfo { Name = "ChieuDai", Caption = "Chiều dài (m)", Layout = 4 },
                new EditorInfo { Name = "ChieuRong", Caption = "Chiều rộng (m)", Layout = 4 },
                new EditorInfo { Name = "ChieuCao", Caption = "Chiều cao (m)", Layout = 4 },
                new EditorInfo { Name = "LuuLuong", Caption = "Lưu lượng thiết kế (m³/s)", Layout = 6 },
                new EditorInfo { Name = "KetCau", Caption = "Kết cấu kênh", Layout = 6 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("KetCau");
            Find("CongTrinhId", c => c.IsEnabled = false);
        }
    }
}