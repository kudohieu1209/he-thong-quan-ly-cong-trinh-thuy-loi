using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using WinApp.Controllers;

namespace WinApp.Controller
{
    partial class LichSuBaoTriController : DataController<Models.LichSuBaoTri>
    {
        public override object Index()
        {
            var data = DataEngine.ToList<Models.LichSuBaoTri>(null, null);
            var listCongTrinh = System.Provider.GetTable<Models.CongTrinh>().ToList<Models.CongTrinh>(null, null);

            foreach (var item in data)
            {
                var ct = listCongTrinh.Find(c => c.Id == item.CongTrinhId);
                item.TenCongTrinh = (ct != null) ? ct.TenCongTrinh : "---";
            }
            return View(new WinApp.Views.LichSuBaoTri.Index(), data);
        }
        public override object Add() => View(new WinApp.Views.LichSuBaoTri.Add(), new EditContext(CreateEntity(), EditActions.Insert));
        public override object Edit(Models.LichSuBaoTri e) => View(new WinApp.Views.LichSuBaoTri.Edit(), new EditContext(e));
    }

}
