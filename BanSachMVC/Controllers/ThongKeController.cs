using BanSachMVC.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BanSachMVC.Controllers
{
    public class ThongKeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string API_URL = "https://localhost:7059/api/"; //URL API

        public ThongKeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(API_URL);
        }
        public async Task<IActionResult> Index()
        {
            var Role = HttpContext.Session.GetInt32("Role");
            if (Role == 1)
            {
                // Thống kê sách theo danh mục
                var booksResponse = await _httpClient.GetAsync("ThongKe/books-by-category");
                var booksJson = await booksResponse.Content.ReadAsStringAsync();
                var booksData = JsonConvert.DeserializeObject<List<BookCategoryStatisticsDTO>>(booksJson);

                // Thống kê doanh thu theo ngày
                var revenueResponse = await _httpClient.GetAsync("ThongKe/revenue-by-date");
                var revenueJson = await revenueResponse.Content.ReadAsStringAsync();
                var revenueData = JsonConvert.DeserializeObject<List<RevenueStatisticsDTO>>(revenueJson);

                // Thống kê đơn hàng theo trạng thái
                var orderStatusResponse = await _httpClient.GetAsync("ThongKe/orders-by-status");
                var orderStatusJson = await orderStatusResponse.Content.ReadAsStringAsync();
                var orderStatusData = JsonConvert.DeserializeObject<List<OrderStatisticsDTO>>(orderStatusJson);

                // Gửi dữ liệu JSON về ViewBag
                ViewBag.BookStatistics = JsonConvert.SerializeObject(booksData);
                ViewBag.RevenueStatistics = JsonConvert.SerializeObject(revenueData);
                ViewBag.OrderStatistics = JsonConvert.SerializeObject(orderStatusData);

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
           
        }
    }
}
