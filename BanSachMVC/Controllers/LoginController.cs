using BanSachMVC.DTO;
using BanSachMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BanSachMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string API_URL = "https://localhost:7059/api/"; //URL API

        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(API_URL);
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            // Kiểm tra giá trị email và password nhận được từ form
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
				// Xử lý nếu không có email hoặc mật khẩu
				ViewBag.ErrorMessage = "Email hoặc mật khẩu không được để trống";
				return View();
			}

			// Tạo đối tượng dữ liệu đăng nhập
			var loginData = new LoginDTO { Email=email,Password=password};
            var jsonContent = new StringContent(JsonConvert.SerializeObject(loginData), Encoding.UTF8, "application/json");
            
            try
            {
                // Gửi yêu cầu đăng nhập đến API
                var response = await _httpClient.PostAsync("https://localhost:7059/api/Auth/login", jsonContent);

                // Kiểm tra xem phản hồi từ API có thành công không
                if (response.IsSuccessStatusCode)
                {
                    // Đăng nhập thành công, lấy dữ liệu trả về từ API
                    var content = await response.Content.ReadAsStringAsync();
                    var user = JsonConvert.DeserializeObject<User>(content);

                    // Lưu thông tin người dùng vào Session
                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    HttpContext.Session.SetString("UserName", user.Name);
                    HttpContext.Session.SetInt32("Role", user.Role);

                    // Chuyển hướng đến trang chủ sau khi đăng nhập thành công
                    if (user.Role == 0)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Index", "ThongKe");
                    }

                }
                else
                {
                    // Nếu phản hồi từ API không thành công, hiển thị lỗi từ API
                    var errorContent = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<dynamic>(errorContent);
                    ViewBag.ErrorMessage = error?.message ?? "Invalid login attempt."; // Hiển thị lỗi từ API
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Bắt tất cả các lỗi khác và hiển thị thông báo lỗi
                ViewBag.ErrorMessage = $"An unexpected error occurred: {ex.Message}";
                return View();
            }
        }
    }
}
