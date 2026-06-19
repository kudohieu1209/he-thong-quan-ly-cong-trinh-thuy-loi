using System;
namespace WinApp.Views.VuMua
{
    using Models;
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Danh sách Vụ mùa";

            context.TableColumns = new object[] {
                new TableColumn { Name = "TenVu", Caption = "Tên vụ", Width = 150 },
                new TableColumn { Name = "Nam", Caption = "Năm", Width = 80, HorizontalAlignment = System.Windows.HorizontalAlignment.Center },
                new TableColumn { Name = "NgayBatDauStr", Caption = "Bắt đầu", Width = 120 },
                new TableColumn { Name = "NgayKetThucStr", Caption = "Kết thúc", Width = 120 },
            };

            context.Search = (e, s) => {
                var x = (VuMua)e;
                return x.TenVu.ToLower().Contains(s);
            };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin Vụ mùa";

            context.Editors = new object[] {
                new EditorInfo { Name = "TenVu", Caption = "Tên vụ (Xuân, Hè Thu...)", Layout = 8, },
                new EditorInfo { Name = "Nam", Caption = "Năm", Layout = 4, Type="Number"},
                new EditorInfo { Name = "ThoiGianBatDau", Caption = "Thời gian bắt đầu", Layout = 6  },
                new EditorInfo { Name = "ThoiGianKetThuc", Caption = "Thời gian kết thúc", Layout = 6 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("TenVu");
        }
    }
}