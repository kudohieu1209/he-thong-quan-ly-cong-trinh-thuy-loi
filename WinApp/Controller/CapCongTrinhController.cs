using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WinApp.Controllers
{
    partial class CapCongTrinhController : DataController<Models.CapCongTrinh>
    {
        public override object Index() => View(new WinApp.Views.CapCongTrinh.Index(), DataEngine.ToList<Models.CapCongTrinh>(null, null));
        public override object Add() => View(new WinApp.Views.CapCongTrinh.Add(), new EditContext(CreateEntity(), EditActions.Insert));
        public override object Edit(Models.CapCongTrinh e) => View(new WinApp.Views.CapCongTrinh.Edit(), new EditContext(e));
    }
}
