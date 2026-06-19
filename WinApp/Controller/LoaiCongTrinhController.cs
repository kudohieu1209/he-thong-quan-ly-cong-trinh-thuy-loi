using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WinApp.Controllers
{
    partial class LoaiCongTrinhController : DataController<Models.LoaiCongTrinh>
    {
        public override object Index() => View(new WinApp.Views.LoaiCongTrinh.Index(), DataEngine.ToList<Models.LoaiCongTrinh>(null, null));
        public override object Add() => View(new WinApp.Views.LoaiCongTrinh.Add(), new EditContext(CreateEntity(), EditActions.Insert));
        public override object Edit(Models.LoaiCongTrinh e) => View(new WinApp.Views.LoaiCongTrinh.Edit(), new EditContext(e));
    }
}
