using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using WinApp.Controllers;

partial class LichSuTruyCapController : DataController<Models.LichSuTruyCap>
{
    public override object Index()
    {
        var listLog = DataEngine.ToList<Models.LichSuTruyCap>(null, null);
        var listHoSo = System.Provider.GetTable<Models.HoSo>().ToList<Models.HoSo>(null, null);

        foreach (var log in listLog)
        {
            var user = listHoSo.Find(h => h.Id == log.HoSoId);
            log.TenNguoiDung = (user != null) ? user.Ten : "Unknown";

            if (log.ThoiGian != null)
            { log.ThoiGianHienThi = log.ThoiGian.Value.ToString("dd/MM/yyyy HH:mm:ss"); }
        }

        return View(new WinApp.Views.LichSuTruyCap.Index(), listLog);
    }
    public override object Add() => View(new WinApp.Views.LichSuTruyCap.Add(), new EditContext(CreateEntity(), EditActions.Insert));
    public override object Edit(Models.LichSuTruyCap e) => View(new WinApp.Views.LichSuTruyCap.Edit(), new EditContext(e));
}

