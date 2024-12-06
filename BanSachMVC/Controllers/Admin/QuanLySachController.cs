using BanSachMVC.DTO;
using BanSachMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BanSachMVC.Controllers.Admin
{
    public class QuanLySachController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string API_URL = "https://localhost:7059/api/"; // Thay đổi URL API của bạn

        public QuanLySachController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(API_URL);
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }
        public async Task<IActionResult> Index(int page = 1, int pageSize = 12)
        {
            try
            {
                var viewModel = new BookViewModel();

                // Gọi API lấy danh sách sách
                var response = await _httpClient.GetAsync($"Books?page={page}&pageSize={pageSize}");
                Console.WriteLine(response);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var booksData = JsonConvert.DeserializeObject<PageResultDTO>(content);

                    if (booksData != null)
                    {
                        viewModel.Books = booksData.Items ?? new List<Book>();  // Nếu Items là null, gán danh sách rỗng
                        viewModel.TotalBooks = booksData.TotalCount;
                        viewModel.CurrentPage = page;
                        viewModel.PageSize = pageSize;
                        viewModel.TotalPages = (int)Math.Ceiling((double)viewModel.TotalBooks / pageSize);  // Tính số trang
                    }
                    else
                    {
                        // Xử lý khi booksData là null
                        viewModel.Books = new List<Book>();
                        viewModel.TotalBooks = 0;
                    }
                }

                // Gọi API lấy danh sách danh mục
                var categoryResponse = await _httpClient.GetAsync("Categories");
                if (categoryResponse.IsSuccessStatusCode)
                {
                    var content = await categoryResponse.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        viewModel.Categories = JsonConvert.DeserializeObject<List<Category>>(content) ?? new List<Category>();
                    }
                    else
                    {
                        viewModel.Categories = new List<Category>();  // Xử lý trường hợp không có danh mục
                    }
                }
                //else
                //{
                //	viewModel.Categories = new List<Category>();  // Xử lý khi API trả về lỗi
                //}

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
                // Bạn có thể log chi tiết lỗi tại đây để biết chính xác lỗi xảy ra ở đâu
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }
    }
}
