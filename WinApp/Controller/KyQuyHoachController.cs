using System;
using WinApp.Controllers;
using Models;

namespace WinApp.Controllers
{
    partial class KyQuyHoachController : DataController<Models.KyQuyHoach>
    {
        public override object Index() => View(new WinApp.Views.KyQuyHoach.Index(), DataEngine.ToList<Models.KyQuyHoach>(null, null));
        public override object Add() => View(new WinApp.Views.KyQuyHoach.Add(), new EditContext(CreateEntity(), EditActions.Insert));
        public override object Edit(Models.KyQuyHoach e) => View(new WinApp.Views.KyQuyHoach.Edit(), new EditContext(e));
    }

}
