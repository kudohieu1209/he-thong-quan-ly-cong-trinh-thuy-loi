using System;
using System.Collections.Generic;
using System.Linq;
using WinApp.Views;
using Models;

namespace WinApp.Controllers
{
    partial class ThongKeCongTrinhController : DataController<Models.CongTrinh>
    {
        public override object Index() => null;
        public override object Add() => null;
        public override object Edit(Models.CongTrinh e) => null;

        // 1. Thống kê theo Loại công trình
        public object TheoLoai()
        {
            var dataCongTrinh = DataEngine.ToList<Models.CongTrinh>(null, null);
            var listLoai = System.Provider.GetTable<Models.LoaiCongTrinh>().ToList<Models.LoaiCongTrinh>(null, null);

            var result = from ct in dataCongTrinh
                         group ct by ct.LoaiCongTrinhId into g
                         let loai = listLoai.FirstOrDefault(x => x.Id == g.Key)
                         select new Models.ThongKeCongTrinh
                         {
                             TenNhom = loai?.TenLoai ?? "Chưa phân loại",
                             SoLuong = g.Count()
                         };

            return RenderView(result.ToList(), "Thống kê số lượng theo Loại công trình");
        }

        // 2. Thống kê theo Cấp công trình
        public object TheoCap()
        {
            var dataCongTrinh = DataEngine.ToList<Models.CongTrinh>(null, null);
            var listCap = System.Provider.GetTable<Models.CapCongTrinh>().ToList<Models.CapCongTrinh>(null, null);

            var result = from ct in dataCongTrinh
                         group ct by ct.CapCongTrinhId into g
                         let cap = listCap.FirstOrDefault(x => x.Id == g.Key)
                         select new Models.ThongKeCongTrinh
                         {
                             TenNhom = cap?.TenCap ?? "Chưa phân cấp",
                             SoLuong = g.Count()
                         };

            return RenderView(result.ToList(), "Thống kê số lượng theo Cấp công trình");
        }

        // 3. Thống kê theo Địa bàn (Xã, Huyện, Tỉnh)
        public object TheoHuyen()
        {
            return GroupByHanhChinhLevel(2, "Thống kê công trình theo Huyện");
        }

        public object TheoXa()
        {
            return GroupByHanhChinhLevel(3, "Thống kê công trình theo Xã");
        }

        public object ToanTinh()
        {
            return GroupByHanhChinhLevel(1, "Tổng hợp số lượng toàn Tỉnh");
        }

        // ================== HÀM HỖ TRỢ (PRIVATE) ==================

        private object GroupByHanhChinhLevel(int targetLevel, string reportTitle)
        {
            var dataCongTrinh = DataEngine.ToList<Models.CongTrinh>(null, null);
            var listDonVi = System.Provider.Select<Models.ViewDonVi>();

            var result = from ct in dataCongTrinh
                         group ct by GetMappedUnitId(ct.DonViHanhChinhId, targetLevel, listDonVi) into g
                         let donVi = listDonVi.Find(dv => dv.Id == g.Key)
                         where donVi != null
                         select new Models.ThongKeCongTrinh
                         {
                             // Hiển thị Tên đơn vị + Cấp (ví dụ: Huyện Thái Thụy)
                             TenNhom = $"{donVi.TenHanhChinh} {donVi.Ten}", 
                             SoLuong = g.Count()
                         };

            return RenderView(result.ToList(), reportTitle);
        }

        private int? GetMappedUnitId(int? currentId, int targetLevel, List<Models.ViewDonVi> allUnits)
        {
            if (currentId == null) return null;
            var unit = allUnits.Find(u => u.Id == currentId);

            if (unit == null) return null;

            while (unit != null && unit.HanhChinhId > targetLevel)
            {
                unit = allUnits.Find(p => p.Id == unit.TrucThuocId);
            }

            return (unit != null && unit.HanhChinhId == targetLevel) ? unit.Id : (int?)null;
        }

        private object RenderView(List<Models.ThongKeSoLuong> data, string title)
        {
            double total = data.Sum(x => x.SoLuong);

            foreach (var item in data)
            {
                if (total > 0)
                    item.TyLe = Math.Round((item.SoLuong / total) * 100, 2);
                else
                    item.TyLe = 0;
            }

            data = data.OrderByDescending(x => x.SoLuong).ToList();

            var view = new WinApp.Views.ThongKeCongTrinh.Index();
            view.SetTitle(title);

            // Trả về View kèm data
            return View(view, data);
        }
    }
}
