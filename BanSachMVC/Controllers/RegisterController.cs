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
        public async Task<IActionResult> Index(string Name, string Email,string Password,string Address, string PasswordConfirm)
        {
			if (Password != PasswordConfirm)
			{
				TempData["ErrorMessage"] = "Mật khẩu không khớp.";
				return RedirectToAction("Index");
			}

            var userData = new
            {
                Name = Name,
                Email = Email,
                Password = Password,
                Address = Address
            };

            try
            {
             

                var response = await _httpClient.PostAsJsonAsync("Auth/register", userData);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Đăng ký thành công!";
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = $"Error: {errorContent}";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An exception occurred: {ex.Message}";
            }

            return RedirectToAction("Index","Login");
        }
    }
}
