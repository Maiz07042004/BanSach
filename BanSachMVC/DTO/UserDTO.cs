namespace BanSachMVC.Models
{
	public class UserDTO
	{
		public int UserId { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		// Thêm các trường khác nếu cần, nhưng không bao gồm Password.
	}
}
