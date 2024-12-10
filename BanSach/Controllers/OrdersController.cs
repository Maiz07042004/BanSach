using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BanSach.Models;
using BanSach.DTO;

namespace BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrders), new { id = order.OrderId }, order);
        }

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDTO request)
        {
            try
            {
                // 1. Kiểm tra giỏ hàng của người dùng
                var cart = await _context.Carts
                    .FirstOrDefaultAsync(c => c.UserId == request.UserId);

                if (cart == null)
                {
                    return BadRequest("Giỏ hàng không tồn tại.");
                }

                // Lấy các mục trong giỏ hàng
                var cartItems = await _context.CartItems
                    .Where(ci => ci.CartId == cart.CartId)
                    .Include(ci => ci.Book)
                    .ToListAsync();

                if (cartItems.Count() == 0)
                {
                    return BadRequest("Giỏ hàng của bạn trống.");
                }

                // 2. Tính toán tổng giá trị đơn hàng
                decimal totalAmount = 0;
                foreach (var item in cartItems)
                {
                    decimal priceAfterDiscount = item.UnitPrice - (item.UnitPrice * (item.Book.Discount / 100));
                    totalAmount += priceAfterDiscount * item.Quantity;
                }

                // 3. Tạo đơn hàng mới
                var order = new Order
                {
                    UserId = request.UserId,
                    CustomerName = request.CustomerName,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount,
                    Address = request.Address,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    OrtherNotes = request.OrtherNotes,
                    Status = "Pending"
                };

                // Thêm đơn hàng vào cơ sở dữ liệu và lưu
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();  // Đảm bảo OrderId được gán sau khi lưu

                // 4. Thêm các mục trong đơn hàng
                foreach (var item in cartItems)
                {
                    var orderItem = new OrderItem
                    {
                        OrderId = order.OrderId,
                        BookId = item.BookId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice - (item.UnitPrice * (item.Book.Discount / 100))  // Giá sau khi giảm
                    };
                    _context.OrderItems.Add(orderItem);
                }

                await _context.SaveChangesAsync();

                // 5. Xóa tất cả các mục trong giỏ hàng
                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync();

                // Trả về OrderId sau khi đơn hàng được tạo thành công
                return Ok(new { Message = "Đặt hàng thành công", OrderId = order.OrderId });
            }
            catch (Exception ex)
            {
                // Log lỗi chi tiết
                return StatusCode(500, new { Message = "Đã xảy ra lỗi khi đặt hàng", Error = ex.Message });
            }
        }

		[HttpPost("BuyNow")]
		public async Task<IActionResult> BuyNow([FromBody] BuyNowRequestDTO request)
		{
			try
			{
				// 1. Lấy thông tin sách từ cơ sở dữ liệu
				var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == request.BookId);
				if (book == null)
				{
					return BadRequest("Sách không tồn tại.");
				}

				// 2. Tính toán giá sau khi áp dụng giảm giá
				var unitPriceAfterDiscount = book.Price - (book.Price * (book.Discount / 100));

				decimal totalAmount = unitPriceAfterDiscount * request.Quantity;

				// 3. Tạo đơn hàng mới
				var order = new Order
				{
					UserId = request.UserId,
					CustomerName = request.CustomerName,
					OrderDate = DateTime.Now,
					TotalAmount = totalAmount,
					Address = request.Address,
					PhoneNumber = request.PhoneNumber,
					Email = request.Email,
					OrtherNotes = request.OrtherNotes,
					Status = "Pending"
				};

				_context.Orders.Add(order);
				await _context.SaveChangesAsync(); // Lưu đơn hàng và lấy OrderId

				// 4. Thêm thông tin sách vào bảng OrderItems
				var orderItem = new OrderItem
				{
					OrderId = order.OrderId,
					BookId = request.BookId,
					Quantity = request.Quantity,
					UnitPrice = unitPriceAfterDiscount
				};

				_context.OrderItems.Add(orderItem);
				await _context.SaveChangesAsync();

				// 5. Trả về thông tin thành công
				return Ok(new { Message = "Đặt hàng thành công", OrderId = order.OrderId });
			}
			catch (Exception ex)
			{
				// Log lỗi chi tiết
				return StatusCode(500, new { Message = "Đã xảy ra lỗi khi đặt hàng", Error = ex.Message });
			}
		}



		[HttpGet("All")]
        public async Task<IActionResult> GetAllOrders(int page = 1, int pageSize = 10)
        {
            var totalOrders = await _context.Orders.CountAsync();
            var orders = await _context.Orders
               .Include(o => o.OrderItems)
               .OrderByDescending(o => o.Status == "Pending") // Sắp xếp ưu tiên Pending lên trước
               .ThenByDescending(o => o.OrderDate) 
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();
            return Ok(new
            {
                TotalOrders = totalOrders,
                Orders = orders
            });
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] string status)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = status;
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Đã cập nhật trạng thái đơn hàng thành '{status}'" });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetails(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Book)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

    }
}
