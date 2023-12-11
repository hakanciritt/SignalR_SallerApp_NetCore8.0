using Microsoft.AspNetCore.Mvc;

namespace SignalR_App.WebUI.Areas.Admin.Views.Shared.Components.Header
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
