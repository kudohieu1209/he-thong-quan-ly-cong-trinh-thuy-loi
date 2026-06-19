using System;
namespace WinApp.Views.KetQuaTuoi
{
    using System.Linq;
    using Models;
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Kết quả Tưới tiêu";
            context.TableColumns = new object[] {
                new TableColumn { Name = "TenVuMua", Caption = "Vụ mùa", Width = 150 },
                new TableColumn { Name = "TenHanhChinh", Caption = "Địa bàn", Width = 150 },
                new TableColumn { Name = "TenCongTrinh", Caption = "Công trình phục vụ", Width = 200 },
                new TableColumn { Name = "DienTichKeHoach", Caption = "DT Kế hoạch (ha)", Width = 120, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "DienTichThucTe", Caption = "DT Thực tế (ha)", Width = 120, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "NangSuat", Caption = "Năng suất (tạ/ha)", Width = 120, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
                new TableColumn { Name = "SanLuong", Caption = "Sản lượng (tấn)", Width = 120, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
            };
            context.Search = (e, s) => {
                var x = (KetQuaTuoi)e;
                return (x.TenCongTrinh != null && x.TenCongTrinh.ToLower().Contains(s))
                    || (x.TenVuMua != null && x.TenVuMua.ToLower().Contains(s))
                    || (x.TenHanhChinh != null && x.TenHanhChinh.ToLower().Contains(s));
            };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Cập nhật kết quả tưới";

            context.Editors = new object[] {
            new EditorInfo { Name = "VuMuaId", Caption = "Vụ mùa", Layout = 6, Type = "select", ValueName = "Id", DisplayName = "TenHienThi", Options = Provider.Select<VuMua>(), },

            new EditorInfo {
                Name = "DonViHanhChinhId",
                Caption = "Địa bàn",
                Layout = 6,
                Type = "select",
                ValueName = "Id",
                DisplayName = "TenDayDu", 
                Options = Provider.Select<Models.ViewDonVi>() 
            },

            new EditorInfo { Name = "CongTrinhId", Caption = "Công trình", Layout = 12, Type = "select", ValueName = "Id", DisplayName = "TenCongTrinh", Options = Provider.Select<CongTrinh>(), },
            new EditorInfo { Name = "DienTichKeHoach", Caption = "Diện tích kế hoạch (ha)", Layout = 6, },
            new EditorInfo { Name = "DienTichThucTe", Caption = "Diện tích thực tế (ha)", Layout = 6, },
            new EditorInfo { Name = "NangSuat", Caption = "Năng suất (tạ/ha)", Layout = 6, },
            new EditorInfo { Name = "SanLuong", Caption = "Sản lượng (tấn)", Layout = 6, },
        };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("DienTichThucTe");
        }
    }
}