using System;
namespace WinApp.Views.VanBanPhapLy
{
    using Models;
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Văn bản pháp lý & Tài liệu";

            context.TableColumns = new object[] {
                new TableColumn { Name = "TenCongTrinh", Caption = "Tên công trình", Width = 250 },

                new TableColumn { Name = "SoKyHieu", Caption = "Số ký hiệu", Width = 150 },
                new TableColumn { Name = "NgayBanHanh", Caption = "Ngày ban hành", Width = 120 },
                new TableColumn { Name = "LoaiVanBan", Caption = "Loại văn bản", Width = 150 },
                new TableColumn { Name = "TrichYeu", Caption = "Trích yếu nội dung", Width = 350 },
            };

            // Tìm kiếm theo Tên công trình, Số ký hiệu hoặc Trích yếu
            context.Search = (e, s) => {
                var x = (VanBanPhapLy)e;
                return (x.TenCongTrinh != null && x.TenCongTrinh.ToLower().Contains(s))
                    || (x.SoKyHieu != null && x.SoKyHieu.ToLower().Contains(s))
                    || (x.TrichYeu != null && x.TrichYeu.ToLower().Contains(s));
            };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin văn bản";

            context.Editors = new object[] {
                new EditorInfo { Name = "CongTrinhId", Caption = "Công trình liên quan", Layout = 12, Type = "select", ValueName = "Id", DisplayName = "TenCongTrinh", Options = Provider.Select<CongTrinh>() },

                new EditorInfo { Name = "SoKyHieu", Caption = "Số ký hiệu", Layout = 6 },
                new EditorInfo { Name = "NgayBanHanh", Caption = "Ngày ban hành (dd/mm/yyyy)", Layout = 6 },

                new EditorInfo { Name = "LoaiVanBan", Caption = "Loại văn bản", Layout = 6 },
                // Nếu muốn upload file thì dùng Type = "file", còn nhập tên file thì để mặc định
                new EditorInfo { Name = "TepDinhKem", Caption = "Tệp đính kèm", Layout = 6 },

                new EditorInfo { Name = "TrichYeu", Caption = "Trích yếu nội dung", Layout = 12 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            // Hỏi xóa dựa trên số ký hiệu
            ShowDeleteAction("SoKyHieu");
        }
    }
}