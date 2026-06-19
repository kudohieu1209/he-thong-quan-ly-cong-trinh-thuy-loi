using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Views.KyQuyHoach
{
    using Vst.Controls;
    using Models;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Kỳ quy hoạch";
            context.TableColumns = new object[] {
                new TableColumn { Name = "TenKyQuyHoach", Caption = "Tên kỳ quy hoạch", Width = 250 },
                new TableColumn { Name = "NamBatDau", Caption = "Năm bắt đầu", Width = 100, HorizontalAlignment = System.Windows.HorizontalAlignment.Center },
                new TableColumn { Name = "NamKetThuc", Caption = "Năm kết thúc", Width = 100, HorizontalAlignment = System.Windows.HorizontalAlignment.Center },
                new TableColumn { Name = "TrangThai", Caption = "Trạng thái", Width = 120 },
                new TableColumn { Name = "MoTa", Caption = "Mô tả", Width = 300 }, // Đã sửa: Layout=12 -> Width=300
            };

            context.Search = (e, s) => {
                var x = (KyQuyHoach)e;
                return x.TenKyQuyHoach.ToLower().Contains(s);
            };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin Kỳ quy hoạch";
            context.Editors = new object[] {
                new EditorInfo { Name = "TenKyQuyHoach", Caption = "Tên kỳ quy hoạch", Layout = 12, Required = true },
                new EditorInfo { Name = "NamBatDau", Caption = "Năm bắt đầu", Layout = 6, Type = "number" },
                new EditorInfo { Name = "NamKetThuc", Caption = "Năm kết thúc", Layout = 6, Type = "number" },
                new EditorInfo { Name = "TrangThai", Caption = "Trạng thái", Layout = 12 },
                new EditorInfo { Name = "MoTa", Caption = "Mô tả", Layout = 12 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("TenKyQuyHoach");
        }
    }
}