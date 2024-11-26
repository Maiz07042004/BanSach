namespace BanSach.DTO
{
	public class CartItemDTO
	{
		public int BookId { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
		public decimal Price { get; set; }
		public string Image { get; set; }
		public int Quantity { get; set; }
	}
}
