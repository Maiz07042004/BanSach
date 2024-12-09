using BanSachMVC.DTO;
using BanSachMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace BanSachMVC.Controllers.Admin
{
    public class QuanLyDonHangController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string API_URL = "https://localhost:7059/api/"; // Thay đổi URL API của bạn

        public QuanLyDonHangController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(API_URL);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var Role = HttpContext.Session.GetInt32("Role");
            if (Role == 1)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"Orders/All?page={page}&pageSize={pageSize}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<OrdersResponseDTO>(content);

                        return View(result);
                    }

                    TempData["Error"] = "Không thể lấy danh sách đơn hàng.";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["Error"] = "Có lỗi xảy ra khi kết nối tới API.";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
               
        }

        // POST: Admin/Orders/UpdateStatus
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int orderId, string status)
        {
            try
            {
                var response = await _httpClient.PutAsync($"Orders/{orderId}/status", new StringContent($"\"{status}\"", Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = $"Đã cập nhật trạng thái đơn hàng thành '{status}'.";
                }
                else
                {
                    TempData["Error"] = "Không thể cập nhật trạng thái đơn hàng.";
                }
            }
            catch
            {
                TempData["Error"] = "Có lỗi xảy ra khi cập nhật trạng thái.";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                // Gọi API để lấy thông tin chi tiết đơn hàng
                var response = await _httpClient.GetAsync($"Orders/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var order = JsonConvert.DeserializeObject<Order>(content);

                    if (order != null)
                    {
                        return View(order);
                    }
                }

                TempData["Error"] = "Không tìm thấy thông tin đơn hàng hoặc xảy ra lỗi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Lỗi: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}
