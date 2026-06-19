using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinApp.Views;
using Models;
using WinApp.Controllers;
namespace WinApp.Controller
{
        partial class ChiTietDuongOngController : DataController<Models.ChiTietDuongOng>
        {
            public override object Index()
            {
                var data = DataEngine.ToList<Models.ChiTietDuongOng>(null, null);
                var listCongTrinh = System.Provider.GetTable<Models.CongTrinh>().ToList<Models.CongTrinh>(null, null);

                foreach (var item in data)
                {
                    var ct = listCongTrinh.Find(c => c.Id == item.CongTrinhId);
                    item.TenCongTrinh = (ct != null) ? ct.TenCongTrinh : "---";
                }
                return View(new WinApp.Views.ChiTietDuongOng.Index(), data);
            }
            public override object Add() => View(new WinApp.Views.ChiTietDuongOng.Add(), new EditContext(CreateEntity(), EditActions.Insert));
            public override object Edit(Models.ChiTietDuongOng e) => View(new WinApp.Views.ChiTietDuongOng.Edit(), new EditContext(e));
        }

        partial class ChiTietHoChuaController : DataController<Models.ChiTietHoChua>
        {
            public override object Index()
            {
                var data = DataEngine.ToList<Models.ChiTietHoChua>(null, null);
                var listCongTrinh = System.Provider.GetTable<Models.CongTrinh>().ToList<Models.CongTrinh>(null, null);

                foreach (var item in data)
                {
                    var ct = listCongTrinh.Find(c => c.Id == item.CongTrinhId);
                    item.TenCongTrinh = (ct != null) ? ct.TenCongTrinh : "---";
                }
                return View(new WinApp.Views.ChiTietHoChua.Index(), data);
            }
            public override object Add() => View(new WinApp.Views.ChiTietHoChua.Add(), new EditContext(CreateEntity(), EditActions.Insert));
            public override object Edit(Models.ChiTietHoChua e) => View(new WinApp.Views.ChiTietHoChua.Edit(), new EditContext(e));
        }

        partial class ChiTietKeController : DataController<Models.ChiTietKe>
        {
            public override object Index()
            {
                var data = DataEngine.ToList<Models.ChiTietKe>(null, null);
                var listCongTrinh = System.Provider.GetTable<Models.CongTrinh>().ToList<Models.CongTrinh>(null, null);

                foreach (var item in data)
                {
                    var ct = listCongTrinh.Find(c => c.Id == item.CongTrinhId);
                    item.TenCongTrinh = (ct != null) ? ct.TenCongTrinh : "---";
                }
                return View(new WinApp.Views.ChiTietKe.Index(), data);
            }
            public override object Add() => View(new WinApp.Views.ChiTietKe.Add(), new EditContext(CreateEntity(), EditActions.Insert));
            public override object Edit(Models.ChiTietKe e) => View(new WinApp.Views.ChiTietKe.Edit(), new EditContext(e));
        }

    partial class ChiTietDapTranController : DataController<Models.ChiTietDapTran>
    {
        public override object Index()
        {
            var data = DataEngine.ToList<Models.ChiTietDapTran>(null, null);
            var listCongTrinh = System.Provider.GetTable<Models.CongTrinh>().ToList<Models.CongTrinh>(null, null);

            foreach (var item in data)
            {
                var ct = listCongTrinh.Find(c => c.Id == item.CongTrinhId);
                item.TenCongTrinh = (ct != null) ? ct.TenCongTrinh : "---";
            }
            return View(new WinApp.Views.ChiTietDapTran.Index(), data);
        }
        public override object Add() => View(new WinApp.Views.ChiTietDapTran.Add(), new EditContext(CreateEntity(), EditActions.Insert));
        public override object Edit(Models.ChiTietDapTran e) => View(new WinApp.Views.ChiTietDapTran.Edit(), new EditContext(e));
    }


    partial class ChiTietKenhMuongController : DataController<Models.ChiTietKenhMuong>
        {
            public override object Index()
            {
                var data = DataEngine.ToList<Models.ChiTietKenhMuong>(null, null);
                var listCongTrinh = System.Provider.GetTable<Models.CongTrinh>().ToList<Models.CongTrinh>(null, null);

                foreach (var item in data)
                {
                    var ct = listCongTrinh.Find(c => c.Id == item.CongTrinhId);
                    item.TenCongTrinh = (ct != null) ? ct.TenCongTrinh : "---";
                }
                return View(new WinApp.Views.ChiTietKenhMuong.Index(), data);
            }
            public override object Add() => View(new WinApp.Views.ChiTietKenhMuong.Add(), new EditContext(CreateEntity(), EditActions.Insert));
            public override object Edit(Models.ChiTietKenhMuong e) => View(new WinApp.Views.ChiTietKenhMuong.Edit(), new EditContext(e));
        }

        partial class ChiTietTramBomController : DataController<Models.ChiTietTramBom>
        {
            public override object Index()
            {
                var data = DataEngine.ToList<Models.ChiTietTramBom>(null, null);
                var listCongTrinh = System.Provider.GetTable<Models.CongTrinh>().ToList<Models.CongTrinh>(null, null);

                foreach (var item in data)
                {
                    var ct = listCongTrinh.Find(c => c.Id == item.CongTrinhId);
                    item.TenCongTrinh = (ct != null) ? ct.TenCongTrinh : "---";
                }
                return View(new WinApp.Views.ChiTietTramBom.Index(), data);
            }
            public override object Add() => View(new WinApp.Views.ChiTietTramBom.Add(), new EditContext(CreateEntity(), EditActions.Insert));
            public override object Edit(Models.ChiTietTramBom e) => View(new WinApp.Views.ChiTietTramBom.Edit(), new EditContext(e));
        }
    }

    namespace Models
    {
        public partial class ChiTietDuongOng { public string TenCongTrinh { get; set; } }
        public partial class ChiTietDapTran { public string TenCongTrinh { get; set; } }

        public partial class ChiTietHoChua { public string TenCongTrinh { get; set; } }
        public partial class ChiTietKe { public string TenCongTrinh { get; set; } }
        public partial class ChiTietKenhMuong { public string TenCongTrinh { get; set; } }
        public partial class ChiTietTramBom { public string TenCongTrinh { get; set; } }
    }

