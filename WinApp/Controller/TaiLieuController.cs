using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace WinApp.Controllers
{
    partial class TaiLieuController : DataController<Models.TaiLieuDinhKem>
    {
        public override object Index() => View(new WinApp.Views.TaiLieu.Index(), DataEngine.ToList<Models.TaiLieuDinhKem>(null, null));
        public override object Add() => View(new WinApp.Views.TaiLieu.Add(), new EditContext(CreateEntity(), EditActions.Insert));
        public override object Edit(Models.TaiLieuDinhKem e) => View(new WinApp.Views.TaiLieu.Edit(), new EditContext(e));
    }

}
