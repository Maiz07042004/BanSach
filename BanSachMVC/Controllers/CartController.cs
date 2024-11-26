using BanSachMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BanSachMVC.Controllers
{
	public class CartController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly string API_URL = "https://localhost:7059/api/"; // Thay đổi URL API của bạn

		public CartController(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(API_URL);
		}
		public async Task<IActionResult> Index()
		{
			try
			{
				var viewModel = new CartDTO();

				// Lấy userId từ session
				var userId = HttpContext.Session.GetString("UserId");

				if (string.IsNullOrEmpty(userId))
				{
					// Nếu không có cookie, có thể chuyển hướng đến trang đăng nhập
					return RedirectToAction("Index", "Login");
				}

				// Gọi API lấy giỏ hàng của người dùng
				var response = await _httpClient.GetAsync($"Carts/{userId}");  // Sử dụng userId từ session

				if (response.IsSuccessStatusCode)
				{
					// Đọc dữ liệu JSON từ phản hồi
					var content = await response.Content.ReadAsStringAsync();

					// Giải mã JSON thành đối tượng giỏ hàng
					var cart = JsonConvert.DeserializeObject<CartDTO>(content);
					viewModel = cart;  // Giả sử ViewModel có thuộc tính Cart để lưu thông tin giỏ hàng
				}
				else
				{
					// Nếu API không thành công, có thể log lỗi hoặc xử lý như thế nào đó
					viewModel = null;
				}

				// Trả về view với dữ liệu đã lấy được từ API
				return View(viewModel);
			}
			catch (Exception ex)
			{
				// Log lỗi nếu có
				// Logger.LogError(ex, "Error occurred while fetching cart data");
				return View("Error");
			}
		}


	}
}
