using BanSachMVC.Models;

namespace BanSachMVC.DTO
{
	public class PageResultDTO
	{
		public List<Book> Items { get; set; }
		public int TotalCount { get; set; }
	}
}
