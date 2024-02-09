using Microsoft.AspNetCore.Mvc;

namespace SignalR_App.WebUI.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
