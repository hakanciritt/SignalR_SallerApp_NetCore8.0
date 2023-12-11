using Microsoft.AspNetCore.Mvc;

namespace SignalR_App.WebUI.Areas.Admin.Views.Shared.Components.Footer
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
