using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WinApp.Controllers
{
    partial class KetQuaTuoiController : DataController<Models.KetQuaTuoi>
    {
        public override object Index()
        {
            var data = DataEngine.ToList<Models.KetQuaTuoi>(null, null);

            var listCongTrinh = System.Provider.GetTable<Models.CongTrinh>().ToList<Models.CongTrinh>(null, null);
            var listVuMua = System.Provider.GetTable<Models.VuMua>().ToList<Models.VuMua>(null, null);
            var listDonVi = System.Provider.Select<Models.ViewDonVi>();

            foreach (var item in data)
            {
                var ct = listCongTrinh.Find(c => c.Id == item.CongTrinhId);
                item.TenCongTrinh = (ct != null) ? ct.TenCongTrinh : "---";

                var vm = listVuMua.Find(v => v.Id == item.VuMuaId);
                item.TenVuMua = (vm != null) ? $"{vm.TenVu} - {vm.Nam}" : "---";

                var hc = listDonVi.Find(h => h.Id == item.DonViHanhChinhId);
                item.TenHanhChinh = (hc != null) ? hc.TenDayDu : "---";
            }
            return View(new WinApp.Views.KetQuaTuoi.Index(), data);
        }

        public override object Add() => View(new WinApp.Views.KetQuaTuoi.Add(), new EditContext(CreateEntity(), EditActions.Insert));
        public override object Edit(Models.KetQuaTuoi e) => View(new WinApp.Views.KetQuaTuoi.Edit(), new EditContext(e));
    }

}
