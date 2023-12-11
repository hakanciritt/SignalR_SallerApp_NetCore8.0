using Microsoft.AspNetCore.Mvc;

namespace SignalR_App.WebUI.Areas.Admin.Views.Shared.Components.Sidebar
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
