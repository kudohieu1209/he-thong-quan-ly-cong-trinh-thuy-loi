using System;
using WinApp.Views;
using Models;

namespace WinApp.Controllers
{
    partial class CongTrinhController : DataController<Models.CongTrinh>
    {
        public override object Index()
        {
            var data = DataEngine.ToList<Models.CongTrinh>(null, null);

            var listLoai = System.Provider.GetTable<Models.LoaiCongTrinh>().ToList<Models.LoaiCongTrinh>(null, null);
            var listCap = System.Provider.GetTable<Models.CapCongTrinh>().ToList<Models.CapCongTrinh>(null, null);
            var listDonVi = System.Provider.GetTable<Models.DonVi>().ToList<Models.DonVi>(null, null);

            foreach (var item in data)
            {
                var loai = listLoai.Find(x => x.Id == item.LoaiCongTrinhId);
                item.LoaiCongTrinh = (loai != null) ? loai.TenLoai : "---";

                var cap = listCap.Find(x => x.Id == item.CapCongTrinhId);
                item.CapCongTrinh = (cap != null) ? cap.TenCap : "---";

                var dv = listDonVi.Find(x => x.Id == item.DonViQuanLyId);
                item.DonViQuanLy = (dv != null) ? dv.Ten : "---";
            }

            return View(new WinApp.Views.CongTrinh.Index(), data);
        }

        public override object Add()
        {
            return View(new WinApp.Views.CongTrinh.Add(), new EditContext(CreateEntity(), EditActions.Insert));
        }

        public override object Edit(Models.CongTrinh entity)
        {
            return View(new WinApp.Views.CongTrinh.Edit(), new EditContext(entity));
        }
        public object ChiTiet()
        {
            return View();
        }
    }
}

