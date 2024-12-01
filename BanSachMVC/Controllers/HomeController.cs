using BanSachMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BanSachMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string API_URL = "https://localhost:7059/api/"; // Thay đổi URL API của bạn

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(API_URL);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModel = new HomeViewModel();

                // Gọi API lấy danh sách sách
                var response = await _httpClient.GetAsync("Books/all");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    viewModel.Books = JsonConvert.DeserializeObject<List<Book>>(content);
                }

                // Gọi API lấy danh sách danh mục
                var categoryResponse = await _httpClient.GetAsync("Categories");
                if (categoryResponse.IsSuccessStatusCode)
                {
                    var content = await categoryResponse.Content.ReadAsStringAsync();
                    viewModel.Categories = JsonConvert.DeserializeObject<List<Category>>(content);
                }
				if ((HttpContext.Session.GetInt32("UserId")) != null &&
					!string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
				{
					var userId = HttpContext.Session.GetInt32("UserId");
					var userName = HttpContext.Session.GetString("UserName");
					ViewBag.UserId = userId;
					ViewBag.UserName = userName;

					// Tiếp tục xử lý với userId và userName
				}


				return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log error
                return View("Error");
            }
        }
		
	}
}
