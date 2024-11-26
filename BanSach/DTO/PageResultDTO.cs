using BanSach.Models;

namespace BanSach.DTO
{
	public class PageResultDTO
	{
		public List<Book> Items { get; set; } // Danh sách sách
		public int TotalCount { get; set; } // Tổng số sách
	}
}
