namespace BanSach.DTO
{
	public class UpdateCartItemDTO
	{
		public int Quantity { get; set; }  // Số lượng mới của item trong giỏ
		public decimal UnitPrice { get; set; } // Giá của sản phẩm, có thể thay đổi nếu muốn cập nhật giá
	}

}
