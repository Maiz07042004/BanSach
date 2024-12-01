using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BanSach.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using BanSach.DTO;

namespace BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

		// Lấy tất cả sách
		[HttpGet("all")]
		public async Task<ActionResult<List<Book>>> GetBooks()
		{
			var books = await _context.Books.Include(b => b.Category).ToListAsync();
			return Ok(books);
		}

		// Lấy sách theo pagination
		[HttpGet]
		public async Task<IActionResult> GetBooksPagination(int page = 1, int pageSize = 12)
		{
			// Lấy tổng số sách trong cơ sở dữ liệu
			var totalBooks = await _context.Books.CountAsync();

			// Lấy danh sách sách theo phân trang
			var books = await _context.Books.Include(b => b.Category)
				.Skip((page - 1) * pageSize)  // Bỏ qua số sách của các trang trước đó
				.Take(pageSize)              // Lấy số sách trong trang hiện tại
				.ToListAsync();

			// Tạo đối tượng DTO để trả về kết quả
			var pageResult = new PageResultDTO
			{
				Items = books,                  // Các sách trong trang
				TotalCount = totalBooks,        // Tổng số sách trong cơ sở dữ liệu
			};

			return Ok(pageResult);
		}


		// Lấy danh sách sách theo category
		[HttpGet("category/{categoryId}")]
		public async Task<IActionResult> GetBooksByCategory(int categoryId)
		{
			// Truy vấn các sách thuộc thể loại có CategoryId tương ứng
			var books = await _context.Books
				.Where(b => b.CategoryId == categoryId)
				.Include(b => b.Category)
				.ToListAsync();

			if (books == null || books.Count == 0)
			{
				return NotFound(); // Nếu không có sách nào, trả về 404 Not Found
			}

			return Ok(books); // Trả về danh sách sách dưới dạng JSON
		}

		// Lấy sách theo id
		[HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.Include(b => b.Category).FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

		//Thêm sách
		// POST: api/books
		[HttpPost]
		public async Task<ActionResult<Book>> PostBook(Book book)
		{
			_context.Books.Add(book);
			await _context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetBook), new { id = book.BookId }, book);
		}

		// PUT: api/books/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutBook(int id, Book book)
		{
			if (id != book.BookId)
			{
				return BadRequest();
			}

			_context.Entry(book).State = EntityState.Modified;
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// DELETE: api/books/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteBook(int id)
		{
			var book = await _context.Books.FindAsync(id);
			if (book == null)
			{
				return NotFound();
			}

			_context.Books.Remove(book);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		// Tìm kiếm sách
		[HttpGet("Search")]
		public async Task<IActionResult> SearchBooks(string query, int page = 1, int pageSize = 12)
		{
			if (string.IsNullOrEmpty(query))
			{
				return BadRequest("Từ khóa tìm kiếm không hợp lệ.");
			}

			// Lọc sách dựa trên từ khóa tìm kiếm
			var booksQuery = _context.Books.Include(b => b.Category).AsQueryable();

			// Tìm sách theo tên (Title), tác giả (Author) hoặc mô tả (Description)
			booksQuery = booksQuery.Where(b =>
				b.Title.Contains(query) ||
				b.Author.Contains(query) ||
				b.Category.CategoryName.Contains(query)||
				b.Description.Contains(query));

			// Lấy tổng số sách tìm được
			var totalBooks = await booksQuery.CountAsync();

			// Lấy danh sách sách theo phân trang
			var books = await booksQuery
				.Skip((page - 1) * pageSize)  // Bỏ qua số sách của các trang trước đó
				.Take(pageSize)              // Lấy số sách trong trang hiện tại
				.ToListAsync();

			// Tạo đối tượng DTO để trả về kết quả
			var pageResult = new PageResultDTO
			{
				Items = books,              // Các sách tìm được trong trang
				TotalCount = totalBooks,    // Tổng số sách tìm được
			};

			return Ok(pageResult);
		}

	}
}
