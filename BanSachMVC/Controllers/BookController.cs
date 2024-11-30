using BanSachMVC.DTO;
using BanSachMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace BanSachMVC.Controllers
{
	public class BookController : Controller
	{
		private readonly HttpClient _httpClient;
		private readonly string API_URL = "https://localhost:7059/api/"; // Thay đổi URL API của bạn

		public BookController(HttpClient httpClient)
		{
			_httpClient = httpClient;
			_httpClient.BaseAddress = new Uri(API_URL);
			_httpClient.Timeout = TimeSpan.FromSeconds(30);
		}

		//Lấy danh sách sách theo pagination
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

				// Kiểm tra session
				if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) &&
					!string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
				{
					var userId = HttpContext.Session.GetString("UserId");
					var userName = HttpContext.Session.GetString("UserName");
					ViewBag.UserId = userId;
					ViewBag.UserName = userName;
				}
				else
				{
					// Xử lý khi không có session
					ViewBag.UserId = "Unknown";
					ViewBag.UserName = "Guest";
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

		[HttpGet("Book/Category/{CategoryId}")]
		public async Task<IActionResult> GetBooksByCategory(int CategoryId,int page=1,int pageSize=12)
		{
			try
			{
				var viewModel = new BookViewModel();

				// Gọi API lấy danh sách sách
				var response = await _httpClient.GetAsync($"Books/Category/{CategoryId}");
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					var booksData = JsonConvert.DeserializeObject<List<Book>>(content);

					if (booksData != null)
					{
						// Thực hiện phân trang trên bộ dữ liệu
						var pagedBooks = booksData
							.Skip((page - 1) * pageSize)  // Bỏ qua số sách của các trang trước đó
							.Take(pageSize)              // Lấy số sách trong trang hiện tại
							.ToList();                   // Chuyển đổi sang danh sách

						viewModel.Books = pagedBooks;
						viewModel.TotalBooks = booksData.Count;
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


				// Kiểm tra session
				if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")) &&
					!string.IsNullOrEmpty(HttpContext.Session.GetString("UserName")))
				{
					var userId = HttpContext.Session.GetString("UserId");
					var userName = HttpContext.Session.GetString("UserName");
					ViewBag.UserId = userId;
					ViewBag.UserName = userName;
				}
				else
				{
					// Xử lý khi không có session
					ViewBag.UserId = "Unknown";
					ViewBag.UserName = "Guest";
				}
				ViewBag.CategoryId=CategoryId;

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
		[HttpGet("Book/Detail/{BookId}")]
		public async Task<IActionResult> BookDetail(int BookId)
		{ 
			var response = await _httpClient.GetAsync($"Books/{BookId}");
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var booksData = JsonConvert.DeserializeObject<Book>(content);
				return View(booksData);
			}
			else
			{
				return Redirect("/");
			}
		}

	}
}
