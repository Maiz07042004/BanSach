using Microsoft.AspNetCore.Mvc;

namespace BanSachMVC.Controllers
{
	public class BookController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
