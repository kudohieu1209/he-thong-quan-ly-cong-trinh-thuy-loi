using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Views.Admin
{
    class Test : Home.Missing
    {
        protected override void BuildContent(string roleName)
        {
        }

        protected override void RenderCore(ViewContext context)
        {
            base.RenderCore(context);
            PrepareSurface();
            AddHero("TEST " + ViewContext.Model, "Màn hình kiểm thử nội bộ.");
        }
    }
}
