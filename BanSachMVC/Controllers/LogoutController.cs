using Microsoft.AspNetCore.Mvc;

namespace BanSachMVC.Controllers
{
	public class LogoutController : Controller
	{
		public IActionResult Index()
		{
			HttpContext.Session.Remove("UserId");
			HttpContext.Session.Remove("UserName");
			return RedirectToAction("Index", "Home");
		}
	}
}
