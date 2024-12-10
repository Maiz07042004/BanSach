namespace BanSach.DTO
{
	public class BuyNowRequestDTO
	{
		public int UserId { get; set; }                  // ID người dùng
		public int BookId { get; set; }                 // ID sách được mua
		public int Quantity { get; set; }               // Số lượng sách
		public string CustomerName { get; set; }        // Tên khách hàng
		public string Address { get; set; }             // Địa chỉ giao hàng
		public string PhoneNumber { get; set; }         // Số điện thoại
		public string Email { get; set; }               // Email
		public string OrtherNotes { get; set; }         // Ghi chú nếu có
	}
}
