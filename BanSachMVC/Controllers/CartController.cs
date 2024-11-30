using BanSachMVC.DTO;
using BanSachMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

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
				var viewModel = new CartViewModel();

				// Lấy userId từ session
				var userId = HttpContext.Session.GetInt32("UserId");

				if (userId==null)
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
					viewModel.Cart = cart;  // Giả sử ViewModel có thuộc tính Cart để lưu thông tin giỏ hàng
				}
				else
				{
					// Nếu API không thành công, có thể log lỗi hoặc xử lý như thế nào đó
					viewModel.Cart = null;
				}
				// Gọi API lấy danh sách danh mục
				var categoryResponse = await _httpClient.GetAsync("Categories");
				if (categoryResponse.IsSuccessStatusCode)
				{
					var content = await categoryResponse.Content.ReadAsStringAsync();
					viewModel.Categories = JsonConvert.DeserializeObject<List<Category>>(content);
				}
				if ((HttpContext.Session.GetInt32("UserId"))!=null &&
					!string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
				{
					var userId2 = HttpContext.Session.GetInt32("UserId");
					var userName = HttpContext.Session.GetString("UserName");
					ViewBag.UserId = userId2;
					ViewBag.UserName = userName;

					// Tiếp tục xử lý với userId và userName
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

		[HttpPut]
		public async Task<IActionResult> UpdateQuantity([FromBody] UpdateQuantityBook request)
		{
			try
			{
				var userId = HttpContext.Session.GetInt32("UserId");

				var data = new { idBook = request.idBook, quantity = request.quantity };
				var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
				var response = await _httpClient.PutAsync($"Carts/update/{userId}", jsonContent);
				if (response.IsSuccessStatusCode)
				{

					return Json(new { success = true });
				}
				else
				{
					// Xử lý lỗi nếu có
					return Json(new { success = false });
				}
				// Trả về kết quả thành công

			}
			catch (Exception ex)
			{
				// Xử lý lỗi nếu có
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpGet]
		public async Task<IActionResult> RemoveItem(int BookId)
		{
			
			try
			{
				var userId = HttpContext.Session.GetInt32("UserId");
				// Gửi yêu cầu đến API
				var response = await _httpClient.DeleteAsync($"Carts/remove/{userId}/{BookId}");

				// Kiểm tra xem phản hồi từ API có thành công không
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction("Index");
				}
				else
				{
					return Json(new { success = false });
				}
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}
		[HttpGet]
		public async Task<IActionResult> AddToCart(int bookId, int quantity = 1)
		{
			try
			{
				// Lấy userId từ session
				var userId = HttpContext.Session.GetInt32("UserId");

				if (userId == null)
				{
					// Nếu không có userId trong session, chuyển hướng đến trang đăng nhập
					return RedirectToAction("Index", "Login");
				}

				// Tạo đối tượng dữ liệu cần gửi cho API
				var data = new
				{
					userId = userId,
					bookId = bookId,
					quantity = quantity
				};

				// Chuyển đối tượng dữ liệu thành JSON
				var jsonContent = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

				// Gọi API để thêm sách vào giỏ hàng
				var response = await _httpClient.PostAsync($"Carts/add?userId={userId}&bookId={bookId}&quantity={quantity}",null);

				// Kiểm tra phản hồi từ API
				if (response.IsSuccessStatusCode)
				{
					// Quay lại trang trước đó
					var refererUrl = Request.Headers["Referer"].ToString();
					if (!string.IsNullOrEmpty(refererUrl))
					{
						return Redirect(refererUrl); // Quay lại trang trước đó
					}
				}
				else
				{
					// Nếu API không thành công, xử lý lỗi (có thể log lỗi hoặc hiển thị thông báo lỗi)
					return Json(new { success = false, message = "Failed to add item to cart." });
				}
			}
			catch (Exception ex)
			{
				// Xử lý lỗi nếu có
				return Json(new { success = false, message = ex.Message });
			}
			return RedirectToAction("Index", "Home");
		}

	}
}
