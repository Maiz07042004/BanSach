namespace BanSachMVC.Models
{
	public class BookViewModel
	{
		public List<Book> Books { get; set; } = [];
		public List<Category> Categories { get; set; } = [];
		public int TotalBooks { get; set; } = 0; //tổng số sách
		public int CurrentPage { get; set; } //trang hiện tại
		public int PageSize { get; set; }
		public int TotalPages { get; set; }  //tổng số page
	}
}
