using System;
namespace WinApp.Views.CongTrinh
{
    using Vst.Controls;
    using Models;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Danh sách Công trình thủy lợi";
            context.TableColumns = new object[] {
                new TableColumn { Name = "TenCongTrinh", Caption = "Tên công trình", Width = 200 },
                new TableColumn { Name = "MaHieu", Caption = "Mã hiệu", Width = 100 },
                new TableColumn { Name = "LoaiCongTrinh", Caption = "Loại công trình", Width = 120 },
                new TableColumn { Name = "CapCongTrinh", Caption = "Cấp", Width = 80 },
                new TableColumn { Name = "DiaDiem", Caption = "Địa điểm", Width = 180 },
                new TableColumn { Name = "NamXayDung", Caption = "Năm xây dựng", Width = 100 },
                new TableColumn { Name = "TrangThai", Caption = "Trạng thái", Width = 120 },
                new TableColumn { Name = "DonViQuanLy", Caption = "Đơn vị quản lý", Width = 150 },
            };
            context.Search = (e, s) => {
                var x = (CongTrinh)e;
                return x.TenCongTrinh.ToLower().Contains(s) ||
                       (x.MaHieu != null && x.MaHieu.ToLower().Contains(s));
            };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin Công trình";
            context.Editors = new object[] {
                new EditorInfo { Name = "TenCongTrinh", Caption = "Tên công trình", Layout = 8, Required = true },
                new EditorInfo { Name = "MaHieu", Caption = "Mã hiệu", Layout = 4, Required = true },
                new EditorInfo { Name = "CapCongTrinhId", Caption = "Cấp công trình", Layout = 6, Required = true, Type = "select", ValueName = "Id", DisplayName = "TenCap", Options = Provider.Select<CapCongTrinh>() },
                new EditorInfo { Name = "LoaiCongTrinhId", Caption = "Loại công trình", Layout = 6, Required = true, Type = "select", ValueName = "Id", DisplayName = "TenLoai", Options = Provider.Select<LoaiCongTrinh>() },
                new EditorInfo { Name = "DonViQuanLyId", Caption = "Đơn vị quản lý", Layout = 6, Required = true, Type = "select", ValueName = "Id", DisplayName = "Ten", Options = Provider.Select<DonVi>() },
                new EditorInfo { Name = "DonViHanhChinhId", Caption = "Đơn vị hành chính", Layout = 6, Required = true, Type = "select", ValueName = "Id", DisplayName = "Ten", Options = Provider.Select<DonVi>() },
                new EditorInfo { Name = "DiaDiem", Caption = "Địa điểm", Layout = 8 },
                new EditorInfo { Name = "NamXayDung", Caption = "Năm xây dựng", Layout = 4 },
                new EditorInfo { Name = "TrangThai", Caption = "Trạng thái", Layout = 6 },
                new EditorInfo { Name = "DuLieuGIS", Caption = "Dữ liệu GIS", Layout = 6 },
                new EditorInfo { Name = "MoTa", Caption = "Mô tả", Layout = 12 },
                new EditorInfo { Name = "HinhAnh", Caption = "Hình ảnh", Layout = 12 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("TenCongTrinh");

            // Nếu 0 có trường nào tên là "EditorName" cần ẩn thì comment dòng này đi để tránh lỗi
            // Find("EditorName", c => c.IsEnabled = false); 
        }
    }
}