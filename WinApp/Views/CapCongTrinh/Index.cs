using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Views.CapCongTrinh
{
    using Vst.Controls;
    using Models;

    class Index : BaseView<DataListViewLayout>
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Danh sách Cấp công trình";
            context.TableColumns = new object[] {
                new TableColumn { Name = "TenCap", Caption = "Tên cấp", Width = 150 },
                new TableColumn { Name = "MoTa", Caption = "Mô tả", Width = 350 },
            };
            context.Search = (e, s) => {
                var x = (CapCongTrinh)e;
                return x.TenCap.ToLower().Contains(s);
            };
        }
    }

    class Add : EditView
    {
        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            context.Title = "Thông tin Cấp công trình";
            context.Editors = new object[] {
                new EditorInfo { Name = "TenCap", Caption = "Tên cấp", Layout = 6, Required = true },
                new EditorInfo { Name = "MoTa", Caption = "Mô tả", Layout = 12 },
            };
        }
    }

    class Edit : Add
    {
        protected override void OnReady()
        {
            ShowDeleteAction("TenCap");
        }
    }
}