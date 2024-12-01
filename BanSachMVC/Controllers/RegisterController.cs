using Microsoft.AspNetCore.Mvc;

namespace BanSachMVC.Controllers
{
    public class RegisterController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string API_URL = "https://localhost:7059/api/"; //URL API

        public RegisterController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(API_URL);
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Name)
        {
            return RedirectToAction("Index");
        }
    }
}
