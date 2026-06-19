using System;
namespace WinApp.Views.TaiLieu
{
    using Models;
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Kho Tài liệu & Văn bản số hóa";

            context.TableColumns = new object[] {
                new TableColumn { Name = "TenFile", Caption = "Tên tệp tin", Width = 250 },
                
                new TableColumn { Name = "LoaiDoiTuong", Caption = "Phân loại", Width = 150 },

                new TableColumn { Name = "MoTa", Caption = "Mô tả chi tiết", Width = 350 },
                
                new TableColumn { Name = "DuongDan", Caption = "Đường dẫn lưu trữ", Width = 200 },
            };

            context.Search = (e, s) => {
                var x = (TaiLieuDinhKem)e;
                return (x.TenFile != null && x.TenFile.ToLower().Contains(s))
                    || (x.MoTa != null && x.MoTa.ToLower().Contains(s))
                    || (x.LoaiDoiTuong != null && x.LoaiDoiTuong.ToLower().Contains(s));
            };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thêm mới tài liệu";

            context.Editors = new object[] {
                new EditorInfo { Name = "TenFile", Caption = "Tên tệp tin (VD: Ban_ve_thiet_ke.pdf)", Layout = 12, Required = true },
                
                new EditorInfo { Name = "LoaiDoiTuong", Caption = "Phân loại (CongTrinh/VanBan...)", Layout = 6 },
                
                new EditorInfo { Name = "DoiTuongId", Caption = "ID Đối tượng liên quan", Layout = 6, Type = "number" },

                new EditorInfo { Name = "DuongDan", Caption = "Đường dẫn file (/uploads/...)", Layout = 12 },

                new EditorInfo { Name = "MoTa", Caption = "Mô tả nội dung", Layout = 12 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("TenFile");
        }
    }
}