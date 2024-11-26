using BanSach.DTO;
using BanSach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BanSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> PostLogin([FromBody] LoginDTO loginDTO)
        {
            if (loginDTO == null)
            {
                return BadRequest("Invalid login request");
            }

            // Kiểm tra xem người dùng có tồn tại không
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

            if (user == null)
            {
                return Unauthorized(new { message = "Sai email" });
            }

            // Kiểm tra mật khẩu 
            if (user.Password != loginDTO.Password)
            {
                return Unauthorized(new { message = "Sai mk" });
            }
            //if (user.Cart == null)
            //{
            //    var cart = new Cart
            //    {
            //        UserId = user.UserId,
            //        CreatedDate = DateTime.Now,
            //        UpdatedDate = DateTime.Now
            //    };
            //    _context.Carts.Add(cart);
            //    await _context.SaveChangesAsync();

            //    // Lưu giỏ hàng mới vào đối tượng người dùng
            //    user.Cart = cart;
            //}

            //// Kiểm tra nếu người dùng chưa có đơn hàng, trả về thông tin người dùng
            //if (user.Orders == null || !user.Orders.Any())
            //{
            //    user.Orders = new List<Order>(); // Trả về danh sách rỗng thay vì null
            //}
            var userDTO=new UserDTO
            {
                Email=user.Email,
                UserId=user.UserId,
                Name=user.Name
            };
            return Ok(user);
        }

    }
}
