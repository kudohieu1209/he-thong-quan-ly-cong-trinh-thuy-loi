using System;
using System.Collections.Generic;
using WinApp.Controllers;
using Models;

namespace WinApp.Controllers
{
    partial class VuMuaController : DataController<Models.VuMua>
    {
        public override object Index()
        {
            return View(new WinApp.Views.VuMua.Index(), DataEngine.ToList<Models.VuMua>(null, null));
        }
        public override object Add() => View(new WinApp.Views.VuMua.Add(), new EditContext(CreateEntity(), EditActions.Insert));
        public override object Edit(Models.VuMua e) => View(new WinApp.Views.VuMua.Edit(), new EditContext(e));
    }
}

