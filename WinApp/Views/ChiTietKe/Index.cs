using System;
using WinApp.Views;
using Models;

namespace WinApp.Views.ChiTietKe
{
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Chi tiết công trình Kè";

            context.TableColumns = new object[] {
                new TableColumn { Name = "TenCongTrinh", Caption = "Tên công trình", Width = 250 },

                new TableColumn { Name = "ChieuDai", Caption = "Chiều dài (m)", Width = 150, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "CaoTrinhDinhKe", Caption = "Cao trình đỉnh kè (m)", Width = 150, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "KetCau", Caption = "Kết cấu", Width = 200 },
            };

            context.Search = (e, s) => { return true; };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin chi tiết Kè";
            context.Editors = new object[] {
                new EditorInfo { Name = "CongTrinhId", Caption = "Công trình", Layout = 12, Required = true, Type = "select", ValueName = "Id", DisplayName = "TenCongTrinh", Options = Provider.Select<Models.CongTrinh>() },

                new EditorInfo { Name = "ChieuDai", Caption = "Chiều dài (m)", Layout = 6 },
                new EditorInfo { Name = "CaoTrinhDinhKe", Caption = "Cao trình đỉnh kè (m)", Layout = 6 },
                new EditorInfo { Name = "KetCau", Caption = "Kết cấu kè", Layout = 12 },
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