using System;
using WinApp.Views;
using Models;

namespace WinApp.Views.ThongKe
{
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        private string _customTitle = "Báo cáo thống kê";
        public void SetTitle(string title)
        {
            _customTitle = title;
        }

        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);

            context.Title = _customTitle;

            context.TableColumns = new object[] {
                new TableColumn { Name = "TenDoiTuong", Caption = "Đối tượng", Width = 250 },

                new TableColumn { Name = "DienTichKeHoach", Caption = "DT Kế hoạch (ha)", Width = 150, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },

                new TableColumn { Name = "DienTichThucTe", Caption = "DT Thực tế (ha)", Width = 150, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },

                new TableColumn { Name = "TyLeDat", Caption = "Tỷ lệ đạt (%)", Width = 120, HorizontalAlignment = System.Windows.HorizontalAlignment.Right },
            };

            //context.MakeReadOnly();
        }
    }
}