using System;
using WinApp.Views;
using Models;

namespace WinApp.Views.ThongKeCongTrinh
{
    using Vst.Controls;

    class Index : BaseView<DataListViewLayout>
    {
        private string _customTitle = "Báo cáo thống kê công trình";

        public void SetTitle(string title)
        {
            _customTitle = title;
        }

        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);

            context.Title = _customTitle;

            context.TableColumns = new object[] {
                new TableColumn { 
                    Name = "TenNhom", 
                    Caption = "Nhóm phân loại", 
                    Width = 300 
                },

                new TableColumn { 
                    Name = "SoLuong", 
                    Caption = "Số lượng", 
                    Width = 150, 
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                },

                new TableColumn { 
                    Name = "TyLe", 
                    Caption = "Tỷ lệ (%)", 
                    Width = 120, 
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                },
                
                new TableColumn { 
                    Name = "GhiChu", 
                    Caption = "Ghi chú", 
                    Width = 200 
                }
            };
            
            // Tắt chức năng edit trên lưới thống kê
            // context.MakeReadOnly(); 
        }
    }
}
