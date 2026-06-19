using System;
using WinApp.Views;
using Models;

namespace WinApp.Controllers
{
    partial class BanDoController : DataController<Models.CongTrinh>
    {
        public override object Index()
        {
            try
            {
                var data = DataEngine.ToList<Models.CongTrinh>(null, null);

                System.Diagnostics.Debug.WriteLine($"BanDoController: Loaded {data?.Count ?? 0} công trình");

                return View(new WinApp.Views.BanDo.Index(), data);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"BanDoController Error: {ex.Message}");
                throw;
            }
        }
    }
}