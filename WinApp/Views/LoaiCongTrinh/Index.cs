using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Views.LoaiCongTrinh
{
    using Vst.Controls;
    using Models;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Danh sách Loại công trình";
            context.TableColumns = new object[] {
                new TableColumn { Name = "TenLoai", Caption = "Tên loại", Width = 150 },
                new TableColumn { Name = "MoTa", Caption = "Mô tả", Width = 350 },
            };
            context.Search = (e, s) => {
                var x = (LoaiCongTrinh)e;
                return x.TenLoai.ToLower().Contains(s);
            };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin Loại công trình";
            context.Editors = new object[] {
                new EditorInfo { Name = "TenLoai", Caption = "Tên loại", Layout = 6, Required = true },
                new EditorInfo { Name = "MoTa", Caption = "Mô tả", Layout = 12 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("TenLoai");
        }
    }
}
