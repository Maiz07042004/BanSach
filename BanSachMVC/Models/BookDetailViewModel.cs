namespace BanSachMVC.Models
{
	public class BookDetailViewModel
	{
		public Book Book { get; set; }
		public List<Book> ListBooks { get; set; } = [];
		public List<Category> Categories { get; set; }
	}
}
