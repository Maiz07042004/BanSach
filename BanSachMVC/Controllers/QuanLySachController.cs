using BanSachMVC.DTO;
using BanSachMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

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
            var Role = HttpContext.Session.GetInt32("Role");
            if (Role == 1)
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
            else
            {
                return RedirectToAction("Index", "Home");
            }
                
        }

        // GET: /Admin/QuanLySach/Create
        public async Task<IActionResult> Create()
        {
            // Bạn có thể truyền các danh mục vào ViewBag hoặc ViewData nếu cần
            var categoryResponse = await _httpClient.GetAsync("Categories");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var content = await categoryResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    var categories = JsonConvert.DeserializeObject<List<Category>>(content);
                    ViewBag.Categories = categories;
                }
                else
                {
                    ViewBag.Categories = new List<Category>();
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create( string Title, string Author, decimal Price, int Discount, string Description, int CategoryId, IFormFile imageFile)
        {
            var book = new ThemBookRequest
            {
                Title = Title,
                Author = Author,
                Price = Price,
                Discount = Discount,
                Description = Description,
                CategoryId = CategoryId // Chỉ cần truyền CategoryId, không cần truyền BookId
            };

            try
            {
                // Kiểm tra dữ liệu hợp lệ
                if (ModelState.IsValid)
                {
                    // Xử lý file hình ảnh
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        var fileName = Path.GetFileName(imageFile.FileName);
                        var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                        using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                        {
                            await imageFile.CopyToAsync(fileStream);
                        }

                        book.Image = fileName;  // Lưu tên file hình ảnh vào model
                    }

                    var jsonContent = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");

                    // Gửi yêu cầu POST đến API để thêm sách
                    var response = await _httpClient.PostAsync("Books", jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index)); 
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Lỗi khi thêm sách.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Dữ liệu không hợp lệ.");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi: {ex.Message}");
            }

            // Nếu có lỗi, hiển thị lại form thêm sách với các danh mục
            var categoryResponse = await _httpClient.GetAsync("Categories");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var content = await categoryResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(content))
                {
                    var categories = JsonConvert.DeserializeObject<List<Category>>(content);
                    ViewBag.Categories = categories;
                }
                else
                {
                    ViewBag.Categories = new List<Category>();
                }
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Gọi API để lấy thông tin sách theo ID
                var response = await _httpClient.GetAsync($"Books/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var book = JsonConvert.DeserializeObject<Book>(content);

                        // Lấy danh mục sách từ API
                        var categoryResponse = await _httpClient.GetAsync("Categories");
                        List<Category> categories = new List<Category>();

                        if (categoryResponse.IsSuccessStatusCode)
                        {
                            var categoryContent = await categoryResponse.Content.ReadAsStringAsync();
                            categories = JsonConvert.DeserializeObject<List<Category>>(categoryContent);
                        }

                        ViewBag.Categories = categories;
                        return View(book);
                    }
                }

                // Nếu không tìm thấy thông tin hoặc có lỗi, chuyển hướng về danh sách sách
                TempData["Error"] = "Không tìm thấy thông tin sách hoặc có lỗi xảy ra.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Lỗi: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int BookId, string Title, string Author, decimal Price, int Discount, string Description, int CategoryId, IFormFile imageFile)
        {
            var book = new ThemBookRequest
            {
                Title = Title,
                Author = Author,
                Price = Price,
                Discount = Discount,
                Description = Description,
                CategoryId = CategoryId
            };

            try
            {
                // Gọi API để lấy thông tin sách theo ID
                var responseBook = await _httpClient.GetAsync($"Books/{BookId}");

                if (responseBook.IsSuccessStatusCode)
                {
                    var content = await responseBook.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(content))
                    {
                        var bookdetail = JsonConvert.DeserializeObject<Book>(content);
                        if (imageFile != null && imageFile.Length > 0)
                        {
                            var fileName = Path.GetFileName(imageFile.FileName);
                            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                            using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                            {
                                await imageFile.CopyToAsync(fileStream);
                            }

                            book.Image = fileName;
                        }
                        else
                        {
                            book.Image = bookdetail.Image;
                        }
                    }
                }
                


                var jsonContent = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"Books/{BookId}", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, "Lỗi khi cập nhật sách.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Lỗi: {ex.Message}");
            }

            // Nếu có lỗi, hiển thị lại form
            var categoriesResponse = await _httpClient.GetAsync("Categories");
            if (categoriesResponse.IsSuccessStatusCode)
            {
                var content = await categoriesResponse.Content.ReadAsStringAsync();
                ViewBag.Categories = JsonConvert.DeserializeObject<List<Category>>(content);
            }
            else
            {
                ViewBag.Categories = new List<Category>();
            }

            return View(book);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                // Gọi API để xoá sách thông qua HTTP DELETE
                var response = await _httpClient.DeleteAsync($"Books/{id}");

                if (response.IsSuccessStatusCode)
                {
                    // Nếu xoá thành công, điều hướng về danh sách sách
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, "Không thể xoá sách.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Có lỗi xảy ra: {ex.Message}");
            }

            // Nếu có lỗi, vẫn giữ tại danh sách sách
            return RedirectToAction(nameof(Index));
        }


    }
}
