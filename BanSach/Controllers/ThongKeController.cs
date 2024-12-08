using BanSach.DTO;
using BanSach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ThongKeController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Thống kê số lượng sách theo danh mục.
        /// </summary>
        [HttpGet("books-by-category")]
        public async Task<ActionResult<IEnumerable<BookCategoryStatisticsDTO>>> GetBooksByCategory()
        {
            var stats = await _context.Books
                .Include(b => b.Category) // Dùng Include để lấy thông tin Category
                .GroupBy(b => b.Category.CategoryName)
                .Select(group => new BookCategoryStatisticsDTO
                {
                    CategoryName = group.Key,
                    BookCount = group.Count()
                })
                .ToListAsync();

            return Ok(stats);
        }


        [HttpGet("revenue-by-date")]
        public async Task<ActionResult<IEnumerable<RevenueStatisticsDTO>>> GetRevenueByDate()
        {
            var stats = await _context.Orders
                .Join(
                    _context.OrderItems,
                    o => o.OrderId,
                    oi => oi.OrderId,
                    (o, oi) => new { o, oi }
                )
                .GroupBy(x => x.o.OrderDate.Date)
                .Select(group => new RevenueStatisticsDTO
                {
                    OrderDate = group.Key,
                    TotalRevenue = group.Sum(x => x.oi.Quantity * x.oi.UnitPrice)
                })
                .DefaultIfEmpty()
                .ToListAsync();

            return Ok(stats);
        }


        /// <summary>
        /// Thống kê số lượng đơn hàng theo trạng thái.
        /// </summary>
        [HttpGet("orders-by-status")]
        public async Task<ActionResult<IEnumerable<OrderStatisticsDTO>>> GetOrdersByStatus()
        {
            var stats = await _context.Orders
                .GroupBy(o => o.Status)
                .Select(group => new OrderStatisticsDTO
                {
                    Status = group.Key,
                    Count = group.Count()
                })
                .ToListAsync();

            return Ok(stats);
        }
    }
}
