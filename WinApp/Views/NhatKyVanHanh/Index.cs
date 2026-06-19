using System;
namespace WinApp.Views.NhatKyVanHanh
{
    using Models;
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Nhật ký Vận hành";

            context.TableColumns = new object[] {
                new TableColumn { Name = "TenCongTrinh", Caption = "Tên công trình", Width = 250 },

                new TableColumn { Name = "NgayBatDauStr", Caption = "Ngày thực hiện", Width = 100 },
                new TableColumn { Name = "NgayKetThucStr", Caption = "Ngày kết thúc", Width = 100 },

                new TableColumn { Name = "NguoiThucHien", Caption = "Người thực hiện", Width = 150 },
                new TableColumn { Name = "NoiDung", Caption = "Nội dung vận hành", Width = 300 },
                new TableColumn { Name = "KetQua", Caption = "Kết quả", Width = 150 },
                new TableColumn { Name = "ChiPhi", Caption = "Chi phí", Width = 120, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
            };

            // Tìm kiếm theo Tên công trình hoặc Nội dung hoặc Người thực hiện
            context.Search = (e, s) => {
                var x = (NhatKyVanHanh)e;
                return (x.TenCongTrinh != null && x.TenCongTrinh.ToLower().Contains(s))
                    || (x.NoiDung != null && x.NoiDung.ToLower().Contains(s))
                    || (x.NguoiThucHien != null && x.NguoiThucHien.ToLower().Contains(s));
            };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Ghi nhận vận hành";

            context.Editors = new object[] {
                new EditorInfo { Name = "CongTrinhId", Caption = "Công trình", Layout = 12, Type = "select", ValueName = "Id", DisplayName = "TenCongTrinh", Options = Provider.Select<CongTrinh>() },

                new EditorInfo { Name = "NgayBatDau", Caption = "Ngày bắt đầu (dd/MM/yyyy)", Layout = 6 },
                new EditorInfo { Name = "NgayKetThuc", Caption = "Ngày kết thúc (dd/MM/yyyy)", Layout = 6 },

                new EditorInfo { Name = "NguoiThucHien", Caption = "Người thực hiện", Layout = 12 },
                new EditorInfo { Name = "NoiDung", Caption = "Nội dung công việc", Layout = 12 },

                new EditorInfo { Name = "KetQua", Caption = "Kết quả", Layout = 6 },
                new EditorInfo { Name = "ChiPhi", Caption = "Chi phí (VNĐ)", Layout = 6 },
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