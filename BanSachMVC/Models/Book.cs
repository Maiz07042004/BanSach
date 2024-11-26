using System.Text.Json.Serialization;
namespace BanSachMVC.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public decimal Price { get; set; }
		public string Image { get; set; }
		public decimal Discount { get; set; }
		public int CategoryId { get; set; }

        // Navigation property
        public virtual Category Category { get; set; }
    }
}
