﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BanSach.Models;
using BanSach.DTO;

namespace BanSach.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CartsController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public CartsController(ApplicationDbContext context)
		{
			_context = context;
		}

		// API Lấy giỏ hàng của người dùng
		[HttpGet("{userId}")]
		public async Task<ActionResult<CartDTO>> GetUserCart(int userId)
		{
			var cart = await _context.Carts
				.Include(c => c.CartItems)
					.ThenInclude(ci => ci.Book)
				.FirstOrDefaultAsync(c => c.UserId == userId);

			if (cart == null)
			{
				return NotFound(new { message = "Cart not found." });
			}

			// Map từ entity Cart sang CartDTO
			var cartDTO = new CartDTO
			{
				CartId = cart.CartId,
				Items = cart.CartItems.Select(ci => new CartItemDTO
				{
					BookId = ci.BookId,
					Title = ci.Book.Title,
					Author = ci.Book.Author,
					Price = Math.Floor(ci.Book.Price * ((100 - ci.Book.Discount) / 100)),
					Image = ci.Book.Image,
					Quantity = ci.Quantity
				}).ToList(),
				TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * Math.Floor(ci.Book.Price * ((100 - ci.Book.Discount) / 100))) // Tính tổng số tiền giỏ hàng
			};

			return Ok(cartDTO); // Trả về DTO của giỏ hàng
		}

		// API Thêm sách vào giỏ hàng
		[HttpPost("add")]
		public async Task<ActionResult> AddToCart(int userId, int bookId, int quantity = 1)
		{
			// Kiểm tra dữ liệu vào hợp lệ
			if (quantity <= 0)
			{
				return BadRequest(new { message = "Quantity must be greater than 0." });
			}

			// Tìm giỏ hàng của người dùng
			var cart = await _context.Carts
				.Include(c => c.CartItems)
				.FirstOrDefaultAsync(c => c.UserId == userId);

			// Nếu giỏ hàng không tồn tại, tạo mới
			if (cart == null)
			{
				cart = new Cart
				{
					UserId = userId,
					CreatedDate = DateTime.Now,
					UpdatedDate = DateTime.Now
				};
				_context.Carts.Add(cart);
				await _context.SaveChangesAsync();
			}

			// Tìm sách theo bookId
			var book = await _context.Books.FindAsync(bookId);
			if (book == null)
			{
				return NotFound(new { message = "Book not found." });
			}

			// Tìm item trong giỏ hàng, nếu chưa có thì thêm mới
			var cartItem = cart.CartItems?.FirstOrDefault(ci => ci.BookId == bookId);
			if (cartItem == null)
			{


				cartItem = new CartItem
				{
					CartId=cart.CartId,
					BookId = bookId,
					Quantity = quantity,
					UnitPrice = book.Price
				};
				cart.CartItems.Add(cartItem);
			}
			else
			{
				// Nếu sách đã có trong giỏ, cập nhật số lượng
				cartItem.Quantity += quantity;
			}

			// Cập nhật thời gian sửa giỏ hàng
			cart.UpdatedDate = DateTime.Now;
			await _context.SaveChangesAsync();

			return Ok(new { message = "Item added to cart successfully.", cartItem = cartItem });
		}
		
		//Xoá Item khỏi giỏ hàng
		[HttpDelete("remove/{userId}/{cartItemId}")]
		public async Task<ActionResult> RemoveItemFromCart(int userId, int cartItemId)
		{
			var cart = await _context.Carts
				.Include(c => c.CartItems)
				.FirstOrDefaultAsync(c => c.UserId == userId);

			if (cart == null)
			{
				return NotFound(new { message = "Giỏ hàng không tìm thấy." });
			}

			// Tìm sản phẩm trong giỏ hàng
			var cartItem = cart.CartItems?.FirstOrDefault(ci => ci.BookId == cartItemId);
			if (cartItem == null)
			{
				return NotFound(new { message = "Không tìm thấy sách trong giỏ hàng." });
			}

			// Xoá sản phẩm khỏi giỏ hàng
			_context.CartItems.Remove(cartItem);
			cart.UpdatedDate = DateTime.Now;  // Cập nhật lại thời gian sửa giỏ hàng
			await _context.SaveChangesAsync();

			return Ok(new { message = "Sản phẩm đã được xóa khỏi giỏ hàng thành công." });
		}

		// API Cập nhật số lượng item trong giỏ hàng
		[HttpPut("update/{userId}")]
		public async Task<ActionResult> UpdateCartItem(int userId, [FromBody] UpdateQuantityBook request)
		{
			if (request.quantity <= 0)
			{
				return BadRequest(new { message = "Quantity must be greater than 0." });
			}

			var cart = await _context.Carts
				.Include(c => c.CartItems)
				.FirstOrDefaultAsync(c => c.UserId == userId);

			if (cart == null)
			{
				return NotFound(new { message = "Cart not found." });
			}

			var cartItem = cart.CartItems?.FirstOrDefault(ci => ci.BookId == request.idBook);
			if (cartItem == null)
			{
				return NotFound(new { message = "Item not found in cart." });
			}

			// Cập nhật số lượng
			cartItem.Quantity = request.quantity;

			// Cập nhật thời gian sửa giỏ hàng
			cart.UpdatedDate = DateTime.Now;
			await _context.SaveChangesAsync();

			return Ok(new { message = "Cart item updated successfully." });
		}
	}
}
