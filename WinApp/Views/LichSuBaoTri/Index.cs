

using System;
namespace WinApp.Views.LichSuBaoTri
{
    using Models;
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Lịch sử Bảo trì & Sửa chữa";
            context.TableColumns = new object[] {
 
        new TableColumn { Name = "TenCongTrinh", Caption = "Công trình", Width = 200, },
        new TableColumn { Name = "NoiDung", Caption = "Nội dung", Width = 300, },
        new TableColumn { Name = "NgayBatDauStr", Caption = "Bắt đầu", Width = 100},
        new TableColumn { Name = "NgayKetThucStr", Caption = "Kết thúc", Width = 100},
        new TableColumn { Name = "DonViThucHien", Caption = "Đơn vị thực hiện", Width = 150, },
        new TableColumn { Name = "KinhPhi", Caption = "Kinh phí", Width = 120, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
        new TableColumn { Name = "KetQua", Caption = "Kết quả", Width = 150, },
      };
            context.Search = (e, s) => {
                var x = (ViewLichSuBaoTri)e;
                return x.TenCongTrinh.ToLower().Contains(s) || x.NoiDung.ToLower().Contains(s);
            };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Ghi nhận bảo trì";
            context.Editors = new object[] {
        
        new EditorInfo { Name = "CongTrinhId", Caption = "Công trình", Layout = 12, Type = "select", ValueName = "Id", DisplayName = "TenCongTrinh", Options = Provider.Select<CongTrinh>(), },

        new EditorInfo { Name = "NoiDung", Caption = "Nội dung bảo trì", Layout = 12, },

        new EditorInfo { Name = "NgayBatDau", Caption = "Ngày bắt đầu (dd/MM/yyyy)", Layout = 6 },
                new EditorInfo { Name = "NgayKetThuc", Caption = "Ngày kết thúc (dd/MM/yyyy)", Layout = 6 },

        new EditorInfo { Name = "DonViThucHien", Caption = "Đơn vị thực hiện", Layout = 6, },
        new EditorInfo { Name = "KinhPhi", Caption = "Kinh phí (VNĐ)", Layout = 6, },

        new EditorInfo { Name = "KetQua", Caption = "Kết quả nghiệm thu", Layout = 12, },
      };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("NoiDung");
        }
    }
}
