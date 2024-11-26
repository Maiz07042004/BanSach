using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BanSach.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _context.Books.Include(b => b.Category).ToListAsync();
            return Ok(books);
        }

		// Lấy danh sách sách theo category
		[HttpGet("category/{categoryId}")]
		public async Task<IActionResult> GetBooksByCategory(int categoryId)
		{
			// Truy vấn các sách thuộc thể loại có CategoryId tương ứng
			var books = await _context.Books
				.Where(b => b.CategoryId == categoryId)
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
	}
}
