using System;
using System.Collections.Generic;
using WinApp.Controllers;
using Models;

namespace WinApp.Controllers
{
    partial class NhatKyVanHanhController : DataController<Models.NhatKyVanHanh>
    {
        public override object Index()
        {
            var data = DataEngine.ToList<Models.NhatKyVanHanh>(null, null);
            var listCongTrinh = System.Provider.GetTable<Models.CongTrinh>().ToList<Models.CongTrinh>(null, null);

            foreach (var item in data)
            {
                var ct = listCongTrinh.Find(c => c.Id == item.CongTrinhId);
                item.TenCongTrinh = (ct != null) ? ct.TenCongTrinh : "---";
            }
            return View(new WinApp.Views.NhatKyVanHanh.Index(), data);
        }
        public override object Add() => View(new WinApp.Views.NhatKyVanHanh.Add(), new EditContext(CreateEntity(), EditActions.Insert));
        public override object Edit(Models.NhatKyVanHanh e) => View(new WinApp.Views.NhatKyVanHanh.Edit(), new EditContext(e));
    }
}
