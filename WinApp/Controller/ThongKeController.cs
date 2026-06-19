using System;
using System.Collections.Generic;
using System.Linq;
using WinApp.Controllers;
using Models;

namespace WinApp.Controllers
{
    partial class ThongKeController : DataController<Models.KetQuaTuoi>
    {
        public override object Index() => null;
        public override object Add() => null;
        public override object Edit(Models.KetQuaTuoi e) => null;
        public object SoSanhTheoVu()
        {
            var dataTuoi = DataEngine.ToList<Models.KetQuaTuoi>(null, null);
            var listVu = System.Provider.GetTable<Models.VuMua>().ToList<Models.VuMua>(null, null);

            var result = from t in dataTuoi
                         group t by t.VuMuaId into g
                         select new Models.ThongKe
                         {
                             TenDoiTuong = listVu.FirstOrDefault(v => v.Id == g.Key)?.TenVu ?? "Vụ chưa xác định",
                             DienTichKeHoach = g.Sum(x => x.DienTichKeHoach ?? 0),
                             DienTichThucTe = g.Sum(x => x.DienTichThucTe ?? 0)
                         };

            return RenderView(result.ToList(), "So sánh diện tích tưới theo Vụ mùa");
        }

        public object SoSanhTheoHuyen()
        {
            return GroupByHanhChinhLevel(2, "So sánh diện tích tưới giữa các Huyện");
        }
        public object SoSanhTheoXa()
        {
            return GroupByHanhChinhLevel(3, "So sánh diện tích tưới giữa các Xã");
        }

        public object TongHopToanTinh()
        {

            return GroupByHanhChinhLevel(1, "Tổng hợp số liệu toàn Tỉnh");
        }

        private object GroupByHanhChinhLevel(int targetLevel, string reportTitle)
        {
            var dataTuoi = DataEngine.ToList<Models.KetQuaTuoi>(null, null);
            var listDonVi = System.Provider.Select<Models.ViewDonVi>();

            var result = from t in dataTuoi
                         group t by GetMappedUnitId(t.DonViHanhChinhId, targetLevel, listDonVi) into g
                         let donVi = listDonVi.Find(dv => dv.Id == g.Key)
                         where donVi != null 
                         select new Models.ThongKe
                         {
                             TenDoiTuong = donVi.TenDayDu, 
                             DienTichKeHoach = g.Sum(x => x.DienTichKeHoach ?? 0),
                             DienTichThucTe = g.Sum(x => x.DienTichThucTe ?? 0)
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

        private object RenderView(List<Models.ThongKe> data, string title)
        {
            foreach (var item in data)
            {
                if (item.DienTichKeHoach > 0)
                    item.TyLeDat = Math.Round((item.DienTichThucTe / item.DienTichKeHoach) * 100, 2);
                else
                    item.TyLeDat = 0;
            }

            data = data.OrderByDescending(x => x.DienTichThucTe).ToList();

            var view = new WinApp.Views.ThongKe.Index();
            view.SetTitle(title);

            return View(view, data);
        }
    }
}