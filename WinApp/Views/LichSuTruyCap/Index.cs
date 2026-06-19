using System;
namespace WinApp.Views.LichSuTruyCap
{
    using Models;
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Nhật ký truy cập hệ thống";

            context.TableColumns = new object[] {
                new TableColumn {
                    Name = "ThoiGian",
                    Caption = "Thời gian",
                    Width = 150,
                },
                
                new TableColumn { Name = "TenNguoiDung", Caption = "Người thực hiện", Width = 200 },

                new TableColumn { Name = "HanhDong", Caption = "Hành động", Width = 150 },
                new TableColumn { Name = "DoiTuongThaoTac", Caption = "Đối tượng thao tác", Width = 250 },
                new TableColumn { Name = "IP", Caption = "Địa chỉ IP", Width = 120 },
            };

            context.Search = (e, s) => {
                var x = (LichSuTruyCap)e;
                return (x.TenNguoiDung != null && x.TenNguoiDung.ToLower().Contains(s))
                    || (x.HanhDong != null && x.HanhDong.ToLower().Contains(s));
            };
        }
    }

    // Form thêm mới (Thường ít dùng thủ công, nhưng vẫn tạo cho đủ bộ)
    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Ghi nhận lịch sử";

            context.Editors = new object[] {
                // Chọn người dùng từ danh sách Hồ Sơ
                new EditorInfo { Name = "HoSoId", Caption = "Người dùng", Layout = 6, Type = "select", ValueName = "Id", DisplayName = "Ten", Options = Provider.Select<HoSo>() },
                
                // Nhập tay ngày giờ (Vì control date bị lỗi, dùng text tạm hoặc để trống tự lấy giờ hiện tại)
                new EditorInfo { Name = "ThoiGian", Caption = "Thời gian", Layout = 6 }, // Nhập định dạng yyyy-MM-dd HH:mm:ss

                new EditorInfo { Name = "HanhDong", Caption = "Hành động (Đăng nhập/Thêm/Sửa)", Layout = 6 },
                new EditorInfo { Name = "IP", Caption = "Địa chỉ IP", Layout = 6 },

                new EditorInfo { Name = "DoiTuongThaoTac", Caption = "Chi tiết đối tượng", Layout = 12 },
            };
        }
    }

    // Form xem chi tiết (Khóa không cho sửa)
    class Edit : Add
    {
        protected override void OnReady()
        {
            // Không cho phép xóa lịch sử để bảo đảm an toàn dữ liệu
            // Nếu muốn cho xóa thì thêm dòng: ShowDeleteAction("HanhDong");

            // Khóa tất cả các ô lại (Chỉ xem)
            Find("HoSoId", c => c.IsEnabled = false);
            Find("ThoiGian", c => c.IsEnabled = false);
            Find("HanhDong", c => c.IsEnabled = false);
            Find("IP", c => c.IsEnabled = false);
            Find("DoiTuongThaoTac", c => c.IsEnabled = false);
        }
    }
}