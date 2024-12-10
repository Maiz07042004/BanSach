using BanSach.DTO;
using BanSach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            //var userDTO=new UserDTO
            //{
            //    Email=user.Email,
            //    UserId=user.UserId,
            //    Name=user.Name,
            //    Role=user.Role
            //};
            return Ok(user);
        }
        [HttpPost("register")]
        public async Task<ActionResult<User>> PostRegister([FromBody] RegisterDTO registerDTO)
        {
            if (registerDTO == null)
            {
                return BadRequest(new { message = "Invalid registration request" });
            }

            try
            {
                // Kiểm tra xem email đã tồn tại chưa
                var emailCheckSql = "SELECT TOP 1 * FROM Users WHERE Email = @Email";
                var existingUser = await _context.Users
                    .FromSqlRaw(emailCheckSql, new SqlParameter("@Email", registerDTO.Email))
                    .FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    return Conflict(new { message = "Email đã được sử dụng" });
                }

                // Insert user mới
                var insertUserSql = @"
            INSERT INTO Users (Name, Email, Password, Address) 
            VALUES (@Name, @Email, @Password, @Address);
        ";

                var parameters = new[]
                {
            new SqlParameter("@Name", registerDTO.Name),
            new SqlParameter("@Email", registerDTO.Email),
            new SqlParameter("@Password", registerDTO.Password),
            new SqlParameter("@Address", registerDTO.Address)
        };

                await _context.Database.ExecuteSqlRawAsync(insertUserSql, parameters);

                return Ok(new { message = "User created successfully" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}"); // Debugging thông tin lỗi
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }


    }
}
