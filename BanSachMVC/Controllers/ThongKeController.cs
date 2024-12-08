using Microsoft.AspNetCore.Mvc;

namespace BanSachMVC.Controllers
{
    public class ThongKeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
