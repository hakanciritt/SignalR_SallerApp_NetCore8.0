using Microsoft.AspNetCore.Mvc;

namespace SignalR_App.WebUI.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
