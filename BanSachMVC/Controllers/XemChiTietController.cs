using Microsoft.AspNetCore.Mvc;

namespace BanSachMVC.Controllers
{
    public class XemChiTietController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string API_URL = "https://localhost:7059/api/"; // Thay đổi URL API của bạn
        public IActionResult Index()
        {
            return View();
        }
    }
}
